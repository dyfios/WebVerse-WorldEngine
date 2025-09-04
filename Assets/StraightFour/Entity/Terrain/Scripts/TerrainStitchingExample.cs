// Copyright (c) 2019-2025 Five Squared Interactive. All rights reserved.

using System;
using System.Collections.Generic;
using UnityEngine;
using FiveSQD.StraightFour.Entity.Terrain;

namespace FiveSQD.StraightFour.Entity
{
    /// <summary>
    /// Example script demonstrating how to use terrain stitching feature.
    /// This script shows how to create adjacent terrain entities with stitching enabled.
    /// </summary>
    public class TerrainStitchingExample : MonoBehaviour
    {
        [Header("Terrain Configuration")]
        [SerializeField] private bool enableStitching = true;
        [SerializeField] private float terrainSize = 10f;
        [SerializeField] private float terrainHeight = 5f;
        [SerializeField] private int gridSize = 2;

        [Header("Example Setup")]
        [SerializeField] private bool createExampleOnStart = false;

        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        void Start()
        {
            if (createExampleOnStart)
            {
                CreateTerrainGrid();
            }
        }

        /// <summary>
        /// Create a grid of terrains with stitching enabled.
        /// This demonstrates how to use the stitching feature.
        /// </summary>
        [ContextMenu("Create Terrain Grid")]
        public void CreateTerrainGrid()
        {
            // Set up terrain layers
            TerrainEntityLayer[] layers = new TerrainEntityLayer[1]
            {
                new TerrainEntityLayer 
                { 
                    diffuseTexture = null, 
                    normalTexture = null, 
                    maskTexture = null, 
                    tileSize = Vector2.one, 
                    tileOffset = Vector2.zero, 
                    specular = Color.white, 
                    metallic = 0.0f, 
                    smoothness = 0.5f 
                }
            };

            // Create layer masks
            float[,] layerMask = GenerateLayerMask(8, 8);
            Dictionary<int, float[,]> layerMasks = new Dictionary<int, float[,]>();
            layerMasks.Add(0, layerMask);

            List<HybridTerrainEntity> createdTerrains = new List<HybridTerrainEntity>();

            // Create a grid of terrains
            for (int x = 0; x < gridSize; x++)
            {
                for (int z = 0; z < gridSize; z++)
                {
                    // Generate heights for this terrain tile
                    float[,] heights = GenerateHeights(8, 8, x, z);
                    
                    // Create terrain with stitching enabled
                    Guid terrainId = Guid.NewGuid();
                    HybridTerrainEntity terrain = HybridTerrainEntity.Create(
                        terrainSize, terrainSize, terrainHeight, 
                        heights, layers, layerMasks, terrainId, enableStitching);

                    // Position the terrain in the grid
                    Vector3 position = new Vector3(x * terrainSize, 0, z * terrainSize);
                    terrain.SetPosition(position, false, false);

                    createdTerrains.Add(terrain);
                    
                    Debug.Log($"Created terrain {terrainId} at position {position} with stitching {(enableStitching ? "enabled" : "disabled")}");
                }
            }

            // Apply stitching to all terrains
            if (enableStitching)
            {
                foreach (HybridTerrainEntity terrain in createdTerrains)
                {
                    terrain.StitchWithAdjacentTerrains();
                }
                Debug.Log($"Applied stitching to {createdTerrains.Count} terrain entities");
            }
        }

        /// <summary>
        /// Generate height data for a terrain tile.
        /// </summary>
        /// <param name="width">Width of the height array.</param>
        /// <param name="height">Height of the height array.</param>
        /// <param name="tileX">X index of the tile in the grid.</param>
        /// <param name="tileZ">Z index of the tile in the grid.</param>
        /// <returns>2D array of height values.</returns>
        private float[,] GenerateHeights(int width, int height, int tileX, int tileZ)
        {
            float[,] heights = new float[width, height];
            
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    // Create a simple height pattern that varies across tiles
                    float normalizedX = (float)x / (width - 1);
                    float normalizedZ = (float)z / (height - 1);
                    
                    // Add tile offset for variation
                    float globalX = normalizedX + tileX;
                    float globalZ = normalizedZ + tileZ;
                    
                    // Generate height using Perlin noise or simple function
                    float heightValue = Mathf.Sin(globalX * Mathf.PI * 0.5f) * Mathf.Cos(globalZ * Mathf.PI * 0.5f);
                    heights[x, z] = Mathf.Abs(heightValue) * terrainHeight * 0.5f;
                }
            }
            
            return heights;
        }

        /// <summary>
        /// Generate layer mask data.
        /// </summary>
        /// <param name="width">Width of the mask array.</param>
        /// <param name="height">Height of the mask array.</param>
        /// <returns>2D array of mask values.</returns>
        private float[,] GenerateLayerMask(int width, int height)
        {
            float[,] mask = new float[width, height];
            
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    mask[x, z] = 1.0f; // Full coverage for simplicity
                }
            }
            
            return mask;
        }

        /// <summary>
        /// Toggle stitching for all terrains in the scene.
        /// </summary>
        [ContextMenu("Toggle Stitching")]
        public void ToggleStitching()
        {
            HybridTerrainEntity[] allTerrains = FindObjectsOfType<HybridTerrainEntity>();
            
            foreach (HybridTerrainEntity terrain in allTerrains)
            {
                bool currentState = terrain.GetStitching();
                terrain.SetStitching(!currentState);
                
                if (!currentState) // If we're enabling stitching
                {
                    terrain.StitchWithAdjacentTerrains();
                }
                
                Debug.Log($"Terrain {terrain.id} stitching: {terrain.GetStitching()}");
            }
        }

        /// <summary>
        /// Remove all terrain entities from the scene.
        /// </summary>
        [ContextMenu("Clear All Terrains")]
        public void ClearAllTerrains()
        {
            HybridTerrainEntity[] allTerrains = FindObjectsOfType<HybridTerrainEntity>();
            
            foreach (HybridTerrainEntity terrain in allTerrains)
            {
                if (terrain != null)
                {
                    terrain.Delete();
                }
            }
            
            Debug.Log($"Removed {allTerrains.Length} terrain entities");
        }
    }
}