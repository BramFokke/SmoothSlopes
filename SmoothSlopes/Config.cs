﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

namespace SmoothSlopes
{


    [Serializable()]
    public class Config
    {
        public Config()
        {
            DefaultMode = SlopeMode.Strict;
            Networks = new List<NetworkType>()
                           {
                               new NetworkType() {Name = "Small Road", StrictLimit = 0.15f, RelaxedLimit = 0.30f },
                               new NetworkType() {Name = "Medium Road", StrictLimit = 0.10f, RelaxedLimit = 0.20f },
                               new NetworkType() {Name = "Large Road", StrictLimit = 0.10f, RelaxedLimit = 0.20f },
                               new NetworkType() {Name = "Ramp", StrictLimit = 0.15f, RelaxedLimit = 0.30f },
                               new NetworkType() {Name = "Highway", StrictLimit = 0.07f, RelaxedLimit = 0.14f },
                               new NetworkType() {Name = "Train Track", StrictLimit = 0.05f, RelaxedLimit = 0.10f },
                           };
            HoldKey = KeyCode.LeftShift;
            ToggleKey = KeyCode.None;
        }

        [XmlAttribute()]
        public SlopeMode DefaultMode { get; set; }

        [XmlAttribute(), DefaultValue(KeyCode.None)]
        public KeyCode HoldKey { get; set; }

        [XmlAttribute(), DefaultValue(KeyCode.None)]
        public KeyCode ToggleKey { get; set; }


        public List<NetworkType> Networks { get; set; }

        public SlopeMode GetNonDefaultMode()
        {
            return GetOtherMode(DefaultMode);
        }

        public SlopeMode GetOtherMode(SlopeMode mode)
        {
            if (mode == SlopeMode.Relaxed)
            {
                return SlopeMode.Strict;
            }
            return SlopeMode.Relaxed;
        }


        /// <summary>
        /// Returns the network config with the specified name
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public NetworkType GetNetwork(string type)
        {
            return Networks.FirstOrDefault(n => n.Name == type);
        }

        public static Config Deserialize(string path)
        {
            try
            {
                if(!File.Exists(path))
                {
                    var config = new Config();
                    config.Serialize(path);
                    return config;
                }
                using(var reader = new StreamReader(path))
                {
                    return (Config)new XmlSerializer(typeof (Config)).Deserialize(reader);
                }
            }
            catch(Exception ex)
            {
                return new Config();
            }
        }

        private void Serialize(string path)
        {
            using(var stream = new StreamWriter(path))
            {
                new XmlSerializer(GetType()).Serialize(stream, this);
            }
        }
    }

    [Serializable()]
    public class NetworkType
    {
        [XmlAttribute()]
        public string Name { get; set; }

        [XmlAttribute()]
        public float StrictLimit { get; set; }

        [XmlAttribute()]
        public float RelaxedLimit { get; set; }

        public float GetLimit(SlopeMode mode)
        {
            switch(mode)
            {
                case SlopeMode.Relaxed:
                    return RelaxedLimit;
                case SlopeMode.Strict:
                    return StrictLimit;
                default:
                    throw new ArgumentException("Unexpected slope mode: " + mode);
            }
        }

    }

    public enum SlopeMode
    {
        Relaxed,
        Strict
    }

}
