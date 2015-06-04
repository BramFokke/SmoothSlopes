using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ICities;
using UnityEngine;

namespace SmoothSlopes
{
    public class Loader : LoadingExtensionBase
    {

        private SlopeSetter mod;

        public override void OnLevelLoaded(LoadMode mode)
        {
            var cameraController = GameObject.FindObjectOfType<CameraController>();
            mod = cameraController.gameObject.AddComponent<SlopeSetter>();
        }

        public override void OnLevelUnloading()
        {
            GameObject.Destroy(mod);
        }

    }

}
