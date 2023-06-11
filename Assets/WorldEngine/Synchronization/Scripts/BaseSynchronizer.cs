// Copyright (c) 2019-2023 Five Squared Interactive. All rights reserved.

using UnityEngine;
using FiveSQD.WebVerse.WorldEngine.Entity;

namespace FiveSQD.WebVerse.WorldEngine.Synchronization
{
    /// <summary>
    /// Base class for a Synchronizer. The World Engine interfaces with a Synchronizer following
    /// this structure.
    /// </summary>
    public class BaseSynchronizer : MonoBehaviour
    {
        /// <summary>
        /// Status Code Enumeration for Synchronizer Calls.
        /// </summary>
        public enum StatusCode { SUCCESS = 0, FAILED = 1, UNSUPPORTED = 99 }

        /// <summary>
        /// Method to set the visibility of a synchronized entity.
        /// </summary>
        /// <param name="entityToSet">The entity to set the visibility of.</param>
        /// <param name="visible">Visibility value.</param>
        /// <returns><see cref="StatusCode"/>StatusCode</returns>
        public virtual StatusCode SetVisibility(BaseEntity entityToSet, bool visible)
        {
            return StatusCode.UNSUPPORTED;
        }

        /// <summary>
        /// Method to delete a synchronized entity.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        /// <returns><see cref="StatusCode"/>StatusCode</returns>
        public virtual StatusCode DeleteSynchronizedEntity(BaseEntity entityToDelete)
        {
            return StatusCode.UNSUPPORTED;
        }

        /// <summary>
        /// Method to set the highlight of a synchronized entity.
        /// </summary>
        /// <param name="entityToSet">The entity to set the highlight of.</param>
        /// <param name="highlight">Highlight value.</param>
        /// <returns><see cref="StatusCode"/>StatusCode</returns>
        public virtual StatusCode SetHighlight(BaseEntity entityToSet, bool highlight)
        {
            return StatusCode.UNSUPPORTED;
        }

        /// <summary>
        /// Method to set the parent of a synchronized entity.
        /// </summary>
        /// <param name="entityToSet">The entity to set the parent of.</param>
        /// <param name="parentToSet">Parent to set.</param>
        /// <returns><see cref="StatusCode"/>StatusCode</returns>
        public virtual StatusCode SetParent(BaseEntity entityToSet, BaseEntity parentToSet)
        {
            return StatusCode.UNSUPPORTED;
        }

        /// <summary>
        /// Method to set the position of a synchronized entity.
        /// </summary>
        /// <param name="entityToSet">The entity to set the position of.</param>
        /// <param name="position">Position value.</param>
        /// <returns><see cref="StatusCode"/>StatusCode</returns>
        public virtual StatusCode SetPosition(BaseEntity entityToSet, Vector3 position)
        {
            return StatusCode.UNSUPPORTED;
        }

        /// <summary>
        /// Method to set the rotation of a synchronized entity.
        /// </summary>
        /// <param name="entityToSet">The entity to set the rotation of.</param>
        /// <param name="rotation">Rotation value.</param>
        /// <returns><see cref="StatusCode"/>StatusCode</returns>
        public virtual StatusCode SetRotation(BaseEntity entityToSet, Quaternion rotation)
        {
            return StatusCode.UNSUPPORTED;
        }

        /// <summary>
        /// Method to set the scale of a synchronized entity.
        /// </summary>
        /// <param name="entityToSet">The entity to set the scale of.</param>
        /// <param name="scale">Scale value.</param>
        /// <returns><see cref="StatusCode"/>StatusCode</returns>
        public virtual StatusCode SetScale(BaseEntity entityToSet, Vector3 scale)
        {
            return StatusCode.UNSUPPORTED;
        }

        /// <summary>
        /// Method to set the size of a synchronized entity.
        /// </summary>
        /// <param name="entityToSet">The entity to set the size of.</param>
        /// <param name="size">Size value.</param>
        /// <returns><see cref="StatusCode"/>StatusCode</returns>
        public virtual StatusCode SetSize(BaseEntity entityToSet, Vector3 size)
        {
            return StatusCode.UNSUPPORTED;
        }

        /// <summary>
        /// Method to set the physical properties of a synchronized entity.
        /// </summary>
        /// <param name="entityToSet">The entity to set the physical properties of.</param>
        /// <param name="properties">Properties value.</param>
        /// <returns><see cref="StatusCode"/>StatusCode</returns>
        public virtual StatusCode SetPhysicalProperties(BaseEntity entityToSet,
            BaseEntity.EntityPhysicalProperties? properties)
        {
            return StatusCode.UNSUPPORTED;
        }

        /// <summary>
        /// Method to set the interaction state of a synchronized entity.
        /// </summary>
        /// <param name="entityToSet">The entity to set the interaction state of.</param>
        /// <param name="state">Interaction State value.</param>
        /// <returns><see cref="StatusCode"/>StatusCode</returns>
        public virtual StatusCode SetInteractionState(BaseEntity entityToSet,
            BaseEntity.InteractionState? state)
        {
            return StatusCode.UNSUPPORTED;
        }

        /// <summary>
        /// Method to set the motion of a synchronized entity.
        /// </summary>
        /// <param name="entityToSet">The entity to set the motion of.</param>
        /// <param name="motion">Motion value.</param>
        /// <returns><see cref="StatusCode"/>StatusCode</returns>
        public virtual StatusCode SetMotion(BaseEntity entityToSet,
            BaseEntity.EntityMotion? motion)
        {
            return StatusCode.UNSUPPORTED;
        }

        /// <summary>
        /// Method to make a synchronized canvas entity a world canvas.
        /// </summary>
        /// <param name="entityToSet">The entity to make a world canvas.</param>
        /// <returns><see cref="StatusCode">StatusCode</returns>
        public virtual StatusCode MakeWorldCanvas(CanvasEntity entityToSet)
        {
            return StatusCode.UNSUPPORTED;
        }

        /// <summary>
        /// Method to make a synchronized canvas entity a screen canvas.
        /// </summary>
        /// <param name="entityToSet">The entity to make a screen canvas.</param>
        /// <returns><see cref="StatusCode">StatusCode</returns>
        public virtual StatusCode MakeScreenCanvas(CanvasEntity entityToSet)
        {
            return StatusCode.UNSUPPORTED;
        }
    }
}