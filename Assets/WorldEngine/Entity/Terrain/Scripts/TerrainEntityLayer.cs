// Copyright (c) 2019-2024 Five Squared Interactive. All rights reserved.

using UnityEngine;

namespace FiveSQD.WebVerse.WorldEngine.Entity.Terrain
{
    /// <summary>
    /// Struct for a terrain entity layer.
    /// </summary>
    public struct TerrainEntityLayer
    {
        /// <summary>
        /// Diffuse texture.
        /// </summary>
        public Texture2D diffuse;

        /// <summary>
        /// Normal texture.
        /// </summary>
        public Texture2D normal;

        /// <summary>
        /// Mask texture.
        /// </summary>
        public Texture2D mask;

        /// <summary>
        /// Specularity.
        /// </summary>
        public Color specular;

        /// <summary>
        /// Metallic factor. Must be between 0 and 1.
        /// </summary>
        [Range(0, 1)]
        public float metallic;

        /// <summary>
        /// Smoothness factor. Must be between 0 and 1.
        /// </summary>
        [Range(0, 1)]
        public float smoothness;
    }
}