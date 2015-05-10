using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ColossalFramework.Plugins;
using UnityEngine;

namespace SmoothSlopes
{
    
    public class SlopeSetter : MonoBehaviour
    {

        public SlopeSetter()
        {
            config = Config.Deserialize(ConfigPath);
            mode = config.DefaultMode;
            Apply();
        }

        private readonly Config config;
        private SlopeMode mode;

        void Update()
        {
            if(config.HoldKey != KeyCode.None)
            {
                UpdateHold(config.HoldKey);
            }
            if(config.ToggleKey != KeyCode.None)
            {
                UpdateToggle(config.HoldKey);
            }

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
            foreach(var collection in 
                        UnityEngine
                        .Object
                        .FindObjectsOfType<NetCollection>())
            {
                foreach (var info in collection.m_prefabs)
                {
                    var type = GetType(info);
                    var network = config.GetNetwork(type);
                    if(network != null)
                    {
                        info.m_maxSlope = network.GetLimit(mode);
                    }
                }
            }
        }

        private static string GetType(NetInfo info)
        {
            if(info.m_class.name == "Highway")
            {
                if(info.m_lanes != null && info.m_lanes.Length == 3)
                {
                    return "Ramp";
                }
            }
            return info.m_class.name;
        }

    }
}
