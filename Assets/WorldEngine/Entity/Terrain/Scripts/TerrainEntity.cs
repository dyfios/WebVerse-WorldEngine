// Copyright (c) 2019-2023 Five Squared Interactive. All rights reserved.

using System;
using UnityEngine;
using FiveSQD.WebVerse.WorldEngine.Materials;
using FiveSQD.WebVerse.WorldEngine.Utilities;

namespace FiveSQD.WebVerse.WorldEngine.Entity
{
    /// <summary>
    /// WIP: Class for a terrain entity.
    /// </summary>
    public class TerrainEntity : BaseEntity
    {
        /// <summary>
        /// Terrain object for the terrain entity.
        /// </summary>
        public Terrain terrain;

        /// <summary>
        /// Terrain collider for the terrain entity.
        /// </summary>
        public TerrainCollider terrainCollider;

        /// <summary>
        /// Highlight cube for the terrain entity.
        /// </summary>
        private GameObject highlightCube;

        /// <summary>
        /// Create a terrain entity.
        /// </summary>
        /// <param name="length">Length of the terrain.</param>
        /// <param name="width">Width of the terrain.</param>
        /// <param name="height">Height of the terrain.</param>
        /// <param name="heights">2D array of heights for the terrain.</param>
        /// <param name="id">ID for the terrain.</param>
        /// <returns>The requested terrain entity.</returns>
        public static TerrainEntity Create(float length, float width, float height,
            float[,] heights, Guid id)
        {
            GameObject terrainGO = new GameObject("TerrainEntity-" + id.ToString());
            TerrainEntity terrainEntity = terrainGO.AddComponent<TerrainEntity>();
            TerrainData terrainData = new TerrainData();
            for (int i = 0; i < heights.GetLength(0); i++)
            {
                for (int j = 0; j < heights.GetLength(1); j++)
                {
                    heights[i, j] = heights[i, j] / height;
                }
            }
            terrainData.heightmapResolution = GetTerrainSize(Math.Max(length, width));
            terrainData.SetHeights(0, 0, heights);
            terrainData.size = new Vector3(length, height, width);
            GameObject terrainObject = Terrain.CreateTerrainGameObject(terrainData);
            terrainObject.transform.SetParent(terrainGO.transform);
            terrainEntity.terrain = terrainObject.GetComponent<Terrain>();
            terrainEntity.terrainCollider = terrainObject.GetComponent<TerrainCollider>();
            return terrainEntity;
        }

        /// <summary>
        /// Delete the terrain entity.
        /// </summary>
        /// <returns></returns>
        public override bool Delete()
        {
            return base.Delete();
        }

        /// <summary>
        /// Get the motion state for this entity.
        /// </summary>
        /// <returns>The motion state for this entity.</returns>
        public override EntityMotion? GetMotion()
        {
            return new EntityMotion
            {
                angularVelocity = Vector3.zero,
                stationary = true,
                velocity = Vector3.zero
            };
        }

        /// <summary>
        /// Get the physical properties for the entity.
        /// </summary>
        /// <returns>The physical properties for this entity.</returns>
        public override EntityPhysicalProperties? GetPhysicalProperties()
        {
            return new EntityPhysicalProperties()
            {
                angularDrag = float.PositiveInfinity,
                centerOfMass = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity),
                drag = float.PositiveInfinity,
                gravitational = false,
                mass = float.PositiveInfinity
            };
        }

        /// <summary>
        /// Get the size of the entity.
        /// </summary>
        /// <returns>The size of the entity.</returns>
        public override Vector3 GetSize()
        {
            return terrain.terrainData.size;
        }

        /// <summary>
        /// Set the highlight state of the entity.
        /// </summary>
        /// <param name="highlight">Whether or not to enable highlighting.</param>
        /// <returns>Whether or not the operation was successful.</returns>
        public override bool SetHighlight(bool highlight)
        {
            highlightCube.SetActive(highlight);

            return true;
        }

        /// <summary>
        /// Set the interaction state for the entity.
        /// </summary>
        /// <param name="stateToSet">Interaction state to set.</param>
        /// <returns>Whether or not the setting was successful.</returns>
        public override bool SetInteractionState(InteractionState stateToSet)
        {
            switch (stateToSet)
            {
                case InteractionState.Physical:
                    MakePhysical();
                    return true;

                case InteractionState.Placing:
                    LogSystem.LogWarning("[TerrainEntity->SetInteractionState] Placing not valid for terrain.");
                    return false;

                case InteractionState.Static:
                    MakeStatic();
                    return true;

                case InteractionState.Hidden:
                    MakeHidden();
                    return true;

                default:
                    LogSystem.LogWarning("[TerrainEntity->SetInteractionState] Interaction state invalid.");
                    return false;
            }
        }

        /// <summary>
        /// Set the motion state for this entity.
        /// </summary>
        /// <param name="motionToSet">Motion state to set.</param>
        /// <returns>Whether or not the setting was successful.</returns>
        public override bool SetMotion(EntityMotion? motionToSet)
        {
            LogSystem.LogWarning("[TerrainEntity->SetMotion] Motion not settable for light.");

            return false;
        }

        /// <summary>
        /// Set the physical properties of the entity.
        /// </summary>
        /// <param name="propertiesToSet">Properties to apply.</param>
        /// <returns>Whether or not the setting was successful.</returns>
        public override bool SetPhysicalProperties(EntityPhysicalProperties? propertiesToSet)
        {
            return base.SetPhysicalProperties(propertiesToSet);
        }

        /// <summary>
        /// Set the size of the entity.
        /// </summary>
        /// <param name="size">Size to set.</param>
        /// <param name="synchronize">Whether or not to synchronize the setting.</param>
        /// <returns>Whether or not the setting was successful.</returns>
        public override bool SetSize(Vector3 size, bool synchronize = true)
        {
            terrain.terrainData.size = size;

            return true;
        }

        /// <summary>
        /// Set the visibility state of the entity.
        /// </summary>
        /// <param name="visible">Whether or not to set the entity to visible.</param>
        /// <returns>Whether or not the setting was successful.</returns>
        public override bool SetVisibility(bool visible)
        {
            terrain.drawHeightmap = visible;

            return true;
        }

        /// <summary>
        /// Initialize this entity. This should only be called once.
        /// </summary>
        /// <param name="idToSet">ID to apply to the entity.</param>
        public override void Initialize(Guid idToSet)
        {
            base.Initialize(idToSet);

            SetUpHighlightVolume();

            MakeHidden();
        }

        /// <summary>
        /// Tear down the entity.
        /// </summary>
        public override void TearDown()
        {
            base.TearDown();
        }

        /// <summary>
        /// Make the entity hidden.
        /// </summary>
        private void MakeHidden()
        {
            switch (interactionState)
            {
                case InteractionState.Physical:
                    break;

                case InteractionState.Placing:
                    break;

                case InteractionState.Static:
                    break;

                case InteractionState.Hidden:
                default:
                    break;
            }

            terrainCollider.enabled = false;
            terrain.enabled = false;
            gameObject.SetActive(false);
            interactionState = InteractionState.Hidden;
        }

        /// <summary>
        /// Make the entity static.
        /// </summary>
        private void MakeStatic()
        {
            switch(interactionState)
            {
                case InteractionState.Hidden:
                    break;

                case InteractionState.Physical:
                    break;

                case InteractionState.Placing:
                    break;

                case InteractionState.Static:
                default:
                    break;
            }

            gameObject.SetActive(true);
            terrain.enabled = true;
            terrainCollider.enabled = false;
            interactionState = InteractionState.Static;
        }

        /// <summary>
        /// Make the entity physical.
        /// </summary>
        private void MakePhysical()
        {
            switch (interactionState)
            {
                case InteractionState.Hidden:
                    break;

                case InteractionState.Placing:
                    break;

                case InteractionState.Static:
                    break;

                case InteractionState.Physical:
                default:
                    break;
            }

            gameObject.SetActive(true);
            terrain.enabled = true;
            terrainCollider.enabled = true;
            interactionState = InteractionState.Physical;
        }

        /// <summary>
        /// Set up the highlight volume for the entity.
        /// </summary>
        private void SetUpHighlightVolume()
        {
            highlightCube = new GameObject("HighlightVolume");

            Vector3[] vertices =
            {
                    new Vector3(terrain.terrainData.bounds.min.x, terrain.terrainData.bounds.min.y, terrain.terrainData.bounds.min.z),
                    new Vector3 (terrain.terrainData.bounds.max.x, terrain.terrainData.bounds.min.y, terrain.terrainData.bounds.min.z),
                    new Vector3 (terrain.terrainData.bounds.max.x, terrain.terrainData.bounds.max.y, terrain.terrainData.bounds.min.z),
                    new Vector3 (terrain.terrainData.bounds.min.x, terrain.terrainData.bounds.min.y, terrain.terrainData.bounds.min.z),
                    new Vector3 (terrain.terrainData.bounds.min.x, terrain.terrainData.bounds.max.y, terrain.terrainData.bounds.max.z),
                    new Vector3 (terrain.terrainData.bounds.max.x, terrain.terrainData.bounds.max.y, terrain.terrainData.bounds.max.z),
                    new Vector3 (terrain.terrainData.bounds.max.x, terrain.terrainData.bounds.min.y, terrain.terrainData.bounds.max.z),
                    new Vector3 (terrain.terrainData.bounds.min.x, terrain.terrainData.bounds.min.y, terrain.terrainData.bounds.max.z),
                };

            int[] triangles =
            {
                    0, 2, 1, //face front
			        0, 3, 2,
                    2, 3, 4, //face top
			        2, 4, 5,
                    1, 2, 5, //face right
			        1, 5, 6,
                    0, 7, 4, //face left
			        0, 4, 3,
                    5, 4, 7, //face back
			        5, 7, 6,
                    0, 6, 7, //face bottom
			        0, 1, 6
                };

            Mesh mesh = highlightCube.AddComponent<MeshFilter>().mesh;
            MeshRenderer hRend = highlightCube.AddComponent<MeshRenderer>();
            hRend.material = MaterialManager.HighlightMaterial;
            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.Optimize();
            mesh.RecalculateNormals();

            highlightCube.transform.SetParent(transform);
            highlightCube.transform.localPosition = Vector3.zero;
            highlightCube.transform.localRotation = Quaternion.identity;
            highlightCube.transform.localScale = Vector3.one;
            highlightCube.SetActive(false);
        }

        /// <summary>
        /// Resize the heightmap for the terrain.
        /// </summary>
        /// <param name="newResolution">New resolution to apply to the terrain.</param>
        private void ResizeHeightmap(int newResolution)
        {
            RenderTexture oldRT = RenderTexture.active;

            RenderTexture oldHeightmap = RenderTexture.GetTemporary(terrain.terrainData.heightmapTexture.descriptor);
            Graphics.Blit(terrain.terrainData.heightmapTexture, oldHeightmap);

            // TODO: Can this be optimized if there is no hole?
            //RenderTexture oldHoles = RenderTexture.GetTemporary(terrain.terrainData.holesRenderTexture.descriptor);
            //Graphics.Blit(terrain.terrainData.holesRenderTexture, oldHoles);

            int dWidth = terrain.terrainData.heightmapResolution;
            int sWidth = newResolution;

            Vector3 oldSize = terrain.terrainData.size;
            terrain.terrainData.heightmapResolution = newResolution;
            terrain.terrainData.size = oldSize;

            oldHeightmap.filterMode = FilterMode.Bilinear;

            // Make sure textures are offset correctly when resampling
            // tsuv = (suv * swidth - 0.5) / (swidth - 1)
            // duv = (tsuv(dwidth - 1) + 0.5) / dwidth
            // duv = (((suv * swidth - 0.5) / (swidth - 1)) * (dwidth - 1) + 0.5) / dwidth
            // k = (dwidth - 1) / (swidth - 1) / dwidth
            // duv = suv * (swidth * k)     + 0.5 / dwidth - 0.5 * k

            float k = (dWidth - 1.0f) / (sWidth - 1.0f) / dWidth;
            float scaleX = (sWidth * k);
            float offsetX = (float)(0.5 / dWidth - 0.5 * k);
            Vector2 scale = new Vector2(scaleX, scaleX);
            Vector2 offset = new Vector2(offsetX, offsetX);

            Graphics.Blit(oldHeightmap, terrain.terrainData.heightmapTexture, scale, offset);
            RenderTexture.ReleaseTemporary(oldHeightmap);

            //oldHoles.filterMode = FilterMode.Point;
            //Graphics.Blit(oldHoles, (RenderTexture) terrain.terrainData.holesRenderTexture);
            //RenderTexture.ReleaseTemporary(oldHoles);

            RenderTexture.active = oldRT;

            terrain.terrainData.DirtyHeightmapRegion(new RectInt(0, 0, terrain.terrainData.heightmapTexture.width, terrain.terrainData.heightmapTexture.height), TerrainHeightmapSyncControl.HeightAndLod);
            //terrain.terrainData.DirtyTextureRegion(TerrainData.HolesTextureName, new RectInt(0, 0, terrain.terrainData.holesRenderTexture.width, terrain.terrainData.holesRenderTexture.height), false);
        }

        /// <summary>
        /// Get the size of the terrain.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private static int GetTerrainSize(float size)
        {
            if (size < 1)
            {
                LogSystem.LogWarning("[TerrainEntity->GetTerrainSize] Size too small.");
                return 33;
            }
            else if (size <= 33)
            {
                return 33;
            }
            else if (size <= 65)
            {
                return 65;
            }
            else if (size <= 129)
            {
                return 129;
            }
            else if (size <= 257)
            {
                return 257;
            }
            else if (size <= 513)
            {
                return 513;
            }
            else if (size <= 1025)
            {
                return 1025;
            }
            else if (size <= 2049)
            {
                return 2049;
            }
            else if (size <= 4097)
            {
                return 4097;
            }
            else
            {
                LogSystem.LogWarning("[TerrainEntity->GetTerrainSize] Size too large.");
                return 4097;
            }
        }
    }
}
