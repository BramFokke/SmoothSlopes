using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ColossalFramework.Plugins;
using UnityEngine;

namespace SmoothSlopes
{
    
    public class SlopeSetter : MonoBehaviour
    {


        private Config config;
        private SlopeMode mode;



        void Update()
        {
            CheckInit(); // Defer loading until tick to ensure that all mods have been loaded.
            if(config.HoldKey != KeyCode.None)
            {
                UpdateHold(config.HoldKey);
            }
            if(config.ToggleKey != KeyCode.None)
            {
                UpdateToggle(config.HoldKey);
            }

        }

        private void CheckInit()
        {
            if(config == null)
            {
                return;
            }
            config = Config.Deserialize(ConfigPath);
            config.UpdateNetworkTypes();
            config.Serialize(ConfigPath);

            mode = config.DefaultMode;
            Apply();

        }

        private void UpdateToggle(KeyCode keyCode)
        {
            if (Input.GetKeyDown(keyCode) || Input.GetKeyUp(keyCode))
            {
                mode = config.GetOtherMode(mode);
                Apply();
            }
        }

        private void UpdateHold(KeyCode keyCode)
        {
            if (Input.GetKeyDown(keyCode))
            {
                mode = config.GetNonDefaultMode();
                Apply();
            }
            if (Input.GetKeyUp(keyCode))
            {
                mode = config.DefaultMode;
                Apply();
            }
        }

        public static string ConfigPath
        {
            get
            {
                return "SmoothSlopesConfig.xml";
            }
        }


        /// <summary>
        /// Globally sets the maximum slopes for all prefabs according to a specified configuration
        /// </summary>
        public void Apply()
        {
            foreach(var info in EnumeratePrefabs())
            {
                var type = GetType(info);
                var network = config.GetNetwork(type);
                if (network != null && network.AreLimitsSet)
                {
                    info.m_maxSlope = network.GetLimit(mode);
                }
            }
        }

        private IEnumerable<NetInfo> EnumeratePrefabs()
        {
            for (uint c = 0; c < PrefabCollection<NetInfo>.PrefabCount(); c++)
            {
                yield return PrefabCollection<NetInfo>.GetPrefab(c);
            }
        }

        private static string GetType(NetInfo info)
        {
            if (info.m_class.name == "Highway")
            {
                if(info.m_lanes != null && info.m_lanes.Length == 3)
                {
                    return "Ramp";
                }
            }
            if(info.name.Contains("Bus"))
            {
                if (info.m_class.name == "Small Road")
                {
                    return "Small Busway";
                }
                return string.Format("{0} With Bus Lanes", info.m_class.name);
            }
            if(info.m_class.name == "Gravel Road" && info.name.Contains("Zonable"))
            {
                return info.name;
            }
            return info.m_class.name;
        }

    }
}
