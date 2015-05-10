using System;
using System.IO;
using ColossalFramework.Plugins;
using ICities;

namespace SmoothSlopes
{
    public class SmoothSlopesMod : IUserMod
    {
        public string Description
        {
            get { return "Limts allowed slope on roads - configurable"; }
        }

        public string Name
        {
            get { return "Flexible Slope Limiter"; }
        }

    }

}
