// Copyright (c) 2019-2023 Five Squared Interactive. All rights reserved.

using UnityEngine;
using FiveSQD.WebVerse.WorldEngine.Utilities;

namespace FiveSQD.WebVerse.WorldEngine.Camera
{
    /// <summary>
    /// Class that manages the world's camera.
    /// </summary>
    public class CameraManager : BaseManager
    {
        /// <summary>
        /// Camera that is being controlled.
        /// </summary>
        [Tooltip("Camera that is being controlled.")]
        public UnityEngine.Camera cam { get; private set; }

        /// <summary>
        /// Default parent for the camera (i.e. when null is provided).
        /// </summary>
        [Tooltip("Default parent for the camera (i.e. when null is provided).")]
        public GameObject defaultCameraParent { get; private set; }

        /// <summary>
        /// Initialize the camera manager.
        /// </summary>
        /// <param name="cam">Camera to initialize with.</param>
        /// <param name="defaultCameraParent">Default camera parent to initialize with.</param>
        public void Initialize(UnityEngine.Camera cam, GameObject defaultCameraParent)
        {
            base.Initialize();
            this.cam = cam;
            this.defaultCameraParent = defaultCameraParent;
        }

        /// <summary>
        /// Terminate the camera manager.
        /// </summary>
        public override void Terminate()
        {
            base.Terminate();
        }

        /// <summary>
        /// Set parent for the camera manager. If null is provided, will use the default camera parent.
        /// </summary>
        /// <param name="parent">Parent GameObject to apply to the camera.</param>
        public void SetParent(GameObject parent)
        {
            if (parent == null)
            {
                cam.transform.SetParent(defaultCameraParent.transform);
            }
            else
            {
                cam.transform.SetParent(parent.transform);
            }
        }

        /// <summary>
        /// Set the position of the camera.
        /// </summary>
        /// <param name="position">Position to apply.</param>
        /// <param name="local">Whether or not that position is local.</param>
        public void SetPosition(Vector3 position, bool local)
        {
            if (local)
            {
                cam.transform.localPosition = position;
            }
            else
            {
                cam.transform.position = position;
            }
        }

        /// <summary>
        /// Set the rotation of the camera.
        /// </summary>
        /// <param name="rotation">Rotation to apply.</param>
        /// <param name="local">Whether or not the rotation is local.</param>
        public void SetRotation(Quaternion rotation, bool local)
        {
            if (local)
            {
                cam.transform.localRotation = rotation;
            }
            else
            {
                cam.transform.rotation = rotation;
            }
        }

        /// <summary>
        /// Set the euler rotation of the camera.
        /// </summary>
        /// <param name="rotation">Euler rotation to apply.</param>
        /// <param name="local">Whether or not the euler rotation is local.</param>
        public void SetEulerRotation(Vector3 rotation, bool local)
        {
            if (local)
            {
                cam.transform.localEulerAngles = rotation;
            }
            else
            {
                cam.transform.eulerAngles = rotation;
            }
        }

        /// <summary>
        /// Set the scale of the camera.
        /// </summary>
        /// <param name="scale">Scale to apply.</param>
        public void SetScale(Vector3 scale)
        {
            cam.transform.localScale = scale;
        }
    }
}