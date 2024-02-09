// Copyright (c) 2019-2024 Five Squared Interactive. All rights reserved.

using FiveSQD.WebVerse.WorldEngine.Utilities;
using UnityEngine;

namespace FiveSQD.WebVerse.WorldEngine.Environment
{
    /// <summary>
    /// Manager for the environment.
    /// </summary>
    public class EnvironmentManager : BaseManager
    {
        /// <summary>
        /// Material to use for the sky.
        /// </summary>
        public Material skyMaterial;

        /// <summary>
        /// Initialize the environment manager.
        /// </summary>
        public override void Initialize()
        {
            ResetSky();
            ApplySkyMaterial();
            base.Initialize();
        }

        /// <summary>
        /// Apply the sky material.
        /// </summary>
        public void ApplySkyMaterial()
        {
            if (skyMaterial == null)
            {
                LogSystem.LogError("[EnvironmentManager->ApplySkyMaterial] No sky material.");
                return;
            }

            RenderSettings.skybox = skyMaterial;
        }

        /// <summary>
        /// Set the sky to a solid color.
        /// </summary>
        /// <param name="color">Color to set the sky to.</param>
        public void SetSolidColorSky(Color color)
        {
            skyMaterial.SetColor("_Tint", color);
        }

        /// <summary>
        /// Set the sky to a texture.
        /// </summary>
        /// <param name="texture">Texture to set the sky to.</param>
        public void SetSkyTexture(Texture2D texture)
        {
            skyMaterial.SetTexture("_MainTex", texture);
        }

        /// <summary>
        /// Reset the sky to the default.
        /// </summary>
        public void ResetSky()
        {
            skyMaterial.SetColor("_Tint", new Color((float) 128 / 255,
                (float) 128 / 255, (float) 128 / 255, (float) 128 / 255));
            skyMaterial.SetTexture("_MainTex", null);
        }
    }
}