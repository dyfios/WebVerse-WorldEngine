// Copyright (c) 2019-2023 Five Squared Interactive. All rights reserved.

using UnityEngine;

namespace FiveSQD.WebVerse.WorldEngine
{
    /// <summary>
    /// Class for the World Engine.
    /// </summary>
    public class WorldEngine : MonoBehaviour
    {
        /// <summary>
        /// Material to use for object highlighting.
        /// </summary>
        [Tooltip("Material to use for object highlighting.")]
        public Material highlightMaterial;

        /// <summary>
        /// Material to use for the environment sky.
        /// </summary>
        [Tooltip("Material to use for the environment sky.")]
        public Material skyMaterial;

        /// <summary>
        /// Prefab for an input entity.
        /// </summary>
        [Tooltip("Prefab for an input entity.")]
        public GameObject inputEntityPrefab;

        /// <summary>
        /// Prefab for a character controller.
        /// </summary>
        [Tooltip("Prefab for a character controller.")]
        public GameObject characterControllerPrefab;

        /// <summary>
        /// Prefab for a voxel block.
        /// </summary>
        [Tooltip("Prefab for a voxel block.")]
        public GameObject voxelPrefab;

        /// <summary>
        /// The active world loaded by the world engine.
        /// </summary>
        public static World.World ActiveWorld
        {
            get
            {
                return instance.currentWorld;
            }
        }

        /// <summary>
        /// The instance of the world engine.
        /// </summary>
        private static WorldEngine instance;

        /// <summary>
        /// The current world in the world engine.
        /// </summary>
        private World.World currentWorld = null;

        /// <summary>
        /// The GameObject for the current world.
        /// </summary>
        private GameObject currentWorldGO;

        /// <summary>
        /// Load a world.
        /// </summary>
        /// <param name="worldName">Name for the world.</param>
        /// <returns>Whether or not the operation was successful.</returns>
        public static bool LoadWorld(string worldName)
        {
            if (instance.currentWorld != null)
            {
                Utilities.LogSystem.LogError("[WorldEngine->LoadWorld] Cannot load world. A world is loaded.");
                return false;
            }

            World.World.WorldInfo wInfo = new World.World.WorldInfo()
            {
                highlightMaterial = instance.highlightMaterial,
                skyMaterial = instance.skyMaterial,
                inputEntityPrefab = instance.inputEntityPrefab,
                characterControllerPrefab = instance.characterControllerPrefab,
                voxelPrefab = instance.voxelPrefab,
                maxStorageEntries = 2048,
                maxEntryLength = 2048,
                maxKeyLength = 128
            };

            instance.currentWorldGO = new GameObject(worldName);
            instance.currentWorldGO.transform.parent = instance.transform;
            instance.currentWorld = instance.currentWorldGO.AddComponent<World.World>();
            instance.currentWorld.Initialize(wInfo);

            return true;
        }

        /// <summary>
        /// Unload the current world.
        /// </summary>
        public static void UnloadWorld()
        {
            if (instance.currentWorld != null)
            {
                instance.currentWorld.Unload();
            }
            instance.currentWorld = null;
        }

        /// <summary>
        /// Unity Awake method.
        /// </summary>
        void Awake()
        {
            instance = this;
        }
    }
}