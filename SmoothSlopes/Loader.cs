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
            #if DEBUG
            LogSlopes("PrefabTypes.md", false);
            LogSlopes("PrefabTypesverbose.md", true);
            #endif
        }

        private void LogSlopes(string path, bool verbose)
        {
            using (var writer = new StreamWriter(path))
            {
                foreach (var collection in
                            UnityEngine
                            .Object
                            .FindObjectsOfType<NetCollection>())
                {
                    writer.WriteLine("Collection: {0}", collection.name);
                    writer.WriteLine("=======================");
                    writer.WriteLine();
                    writer.WriteLine("   * **enabled**: {0}", collection.enabled);
                    writer.WriteLine("   * **tag**: {0}", collection.tag);
                    writer.WriteLine("   * **m_replacedNames**: {0}", string.Join(", ", collection.m_replacedNames));
                    writer.WriteLine("   * **useGUILayout**: {0}", collection.useGUILayout);
                    writer.WriteLine();
                    foreach (var info in collection.m_prefabs)
                    {
                        writer.WriteLine("Collection: {0}", info.name);
                        writer.WriteLine("-----------------------");
                        writer.WriteLine();
                        writer.WriteLine("   * **enabled**: {0}", info.enabled);
                        writer.WriteLine("   * **tag**: {0}", info.tag);
                        writer.WriteLine("   * **category**: {0}", info.category);
                        writer.WriteLine("   * **m_class**: {0}", info.m_class.name);
                        writer.WriteLine("   * **m_lanes**: {0}", info.m_lanes.Length);

                        if(verbose)
                        {
                            writer.WriteLine("   * **editorCategory**: {0}", info.editorCategory);
                            writer.WriteLine("   * **hideFlags**: {0}", info.hideFlags);
                            writer.WriteLine("   * **m_Thumbnail**: {0}", info.m_Thumbnail);
                            writer.WriteLine("   * **m_InfoTooltipThumbnail**: {0}", info.m_InfoTooltipThumbnail);
                            writer.WriteLine("   * **m_UIPriority**: {0}", info.m_UIPriority);
                            writer.WriteLine("   * **m_averageVehicleLaneSpeed**: {0}", info.m_averageVehicleLaneSpeed);
                            writer.WriteLine("   * **m_blockWater**: {0}", info.m_blockWater);
                            writer.WriteLine("   * **m_buildHeight**: {0}", info.m_buildHeight);
                            writer.WriteLine("   * **m_canCollide**: {0}", info.m_canCollide);
                            writer.WriteLine("   * **m_canCrossLanes**: {0}", info.m_canCrossLanes);
                            writer.WriteLine("   * **m_canDisable**: {0}", info.m_canDisable);
                            writer.WriteLine("   * **m_lowerTerrain**: {0}", info.m_lowerTerrain);
                            writer.WriteLine("   * **m_maxBuildAngle**: {0}", info.m_maxBuildAngle);
                            writer.WriteLine("   * **m_maxHeight**: {0}", info.m_maxHeight);
                            writer.WriteLine("   * **m_maxPropDistance**: {0}", info.m_maxPropDistance);
                            writer.WriteLine("   * **m_maxSlope**: {0}", info.m_maxSlope);
                            writer.WriteLine("   * **m_maxTurnAngle**: {0}", info.m_maxTurnAngle);
                            writer.WriteLine("   * **m_minCornerOffset**: {0}", info.m_minCornerOffset);
                            writer.WriteLine("   * **m_minHeight**: {0}", info.m_minHeight);
                            writer.WriteLine("   * **m_netLayers**: {0}", info.m_netLayers);
                            writer.WriteLine("   * **m_nodes**: {0}", info.m_nodes.Length);
                            writer.WriteLine("   * **m_overlayVisible**: {0}", info.m_overlayVisible);
                            writer.WriteLine("   * **m_pavementWidth**: {0}", info.m_pavementWidth);
                            writer.WriteLine("   * **m_prefabDataIndex**: {0}", info.m_prefabDataIndex);
                            writer.WriteLine("   * **m_prefabDataLayer**: {0}", info.m_prefabDataLayer);
                            writer.WriteLine("   * **m_prefabInitialized**: {0}", info.m_prefabInitialized);
                            writer.WriteLine("   * **m_requireContinuous**: {0}", info.m_requireContinuous);
                            writer.WriteLine("   * **m_requireDirectRenderers**: {0}", info.m_requireDirectRenderers);
                            writer.WriteLine("   * **m_requireSegmentRenderers**: {0}", info.m_requireSegmentRenderers);
                            writer.WriteLine("   * **m_requireSurfaceMaps**: {0}", info.m_requireSurfaceMaps);
                            writer.WriteLine("   * **m_segmentLength**: {0}", info.m_segmentLength);
                            writer.WriteLine("   * **m_segments**: {0}", info.m_segments.Length);
                            writer.WriteLine("   * **m_setVehicleFlags**: {0}", info.m_setVehicleFlags);
                            writer.WriteLine("   * **m_snapBuildingNodes**: {0}", info.m_snapBuildingNodes);
                            writer.WriteLine("   * **m_sortedLanes**: {0}", string.Join(", ", info.m_sortedLanes.Select(l => l.ToString()).ToArray()));
                            writer.WriteLine("   * **m_straightSegmentEnds**: {0}", info.m_straightSegmentEnds);
                            writer.WriteLine("   * **m_surfaceLevel**: {0}", info.m_surfaceLevel);
                            writer.WriteLine("   * **m_treeLayers**: {0}", info.m_treeLayers);
                            writer.WriteLine("   * **m_twistSegmentEnds**: {0}", info.m_twistSegmentEnds);
                            writer.WriteLine("   * **m_useFixedHeight**: {0}", info.m_useFixedHeight);
                            writer.WriteLine("   * **m_clipTerrain**: {0}", info.m_clipTerrain);
                            writer.WriteLine("   * **m_color**: {0}", info.m_color);
                            writer.WriteLine("   * **m_createGravel**: {0}", info.m_createGravel);
                            writer.WriteLine("   * **m_createPavement**: {0}", info.m_createPavement);
                            writer.WriteLine("   * **useGUILayout**: {0}", info.m_createRuining);
                            writer.WriteLine("   * **m_flatJunctions**: {0}", info.m_flatJunctions);
                            writer.WriteLine("   * **m_flattenTerrain**: {0}", info.m_flattenTerrain);
                            writer.WriteLine("   * **m_followTerrain**: {0}", info.m_followTerrain);
                            writer.WriteLine("   * **m_halfWidth**: {0}", info.m_halfWidth);
                            writer.WriteLine("   * **m_hasForwardVehicleLanes**: {0}", info.m_hasForwardVehicleLanes);
                            writer.WriteLine("   * **m_hasBackwardVehicleLanes**: {0}", info.m_hasBackwardVehicleLanes);
                            writer.WriteLine("   * **m_hasParkingSpaces**: {0}", info.m_hasParkingSpaces);
                        }                        
                        
                        writer.WriteLine();
                    }
                    writer.WriteLine();
                    writer.WriteLine();
                }
            }
        }

        public override void OnLevelUnloading()
        {
            GameObject.Destroy(mod);
        }

    }

}
