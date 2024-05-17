// Copyright (c) 2019-2024 Five Squared Interactive. All rights reserved.

using System;
using UnityEngine;
using FiveSQD.WebVerse.WorldEngine.Utilities;

namespace FiveSQD.WebVerse.WorldEngine.Entity
{
    /// <summary>
    /// Base class for a UI element entity.
    /// </summary>
    public class UIElementEntity : UIEntity
    {
        protected RectTransform uiElementRectTransform;

        /// <summary>
        /// Set the parent of the entity. This method is not valid for UI element entities.
        /// </summary>
        /// <param name="parent">Parent to set.</param>
        /// <returns>Whether or not the setting was successful.</returns>
        public override bool SetParent(BaseEntity parent)
        {
            LogSystem.LogError("[UIElementEntity->SetParent] Parent on a UI element entity must be set using a UI entity.");
            return false;
        }

        /// <summary>
        /// Set the parent of the entity.
        /// </summary>
        /// <param name="parent">Parent to set.</param>
        /// <returns>Whether or not the setting was successful.</returns>
        public bool SetParent(UIEntity parent)
        {
            if (parent == null)
            {
                LogSystem.LogError("[UIElementEntity->SetParent] UI Element cannot have a null parent.");
                return false;
            }

            transform.SetParent(parent.transform);
            if (synchronizer != null)
            {
                synchronizer.SetParent(this, parent);
            }

            return true;
        }

        /// <summary>
        /// Get the size of the UI element entity as a percentage of its canvas.
        /// </summary>
        /// <returns>The size of the UI element entity as a percentage of its canvas.</returns>
        public Vector2 GetSizePercent()
        {
            RectTransform rt = uiElementRectTransform;
            if (rt == null)
            {
                LogSystem.LogWarning("[UIElementEntity->GetSizePercent] No rect transform.");
                return Vector3.zero;
            }

            CanvasEntity parentCanvasEntity = GetParentCanvasEntity();
            if (parentCanvasEntity == null)
            {
                LogSystem.LogError("[UIElementEntity->GetSizePercent] No parent canvas entity.");
                return Vector3.zero;
            }

            RectTransform parentRT = parentCanvasEntity.GetComponent<RectTransform>();
            if (parentRT == null)
            {
                LogSystem.LogError("[UIElementEntity->GetSizePercent] No parent canvas entity rect transform.");
                return Vector3.zero;
            }

            return new Vector2(rt.sizeDelta.x / parentRT.sizeDelta.x, rt.sizeDelta.y / parentRT.sizeDelta.y);
        }

        /// <summary>
        /// Get the position of the entity. A UI element entity cannot have an absolute position.
        /// </summary>
        /// <param name="local">Whether or not to provide the local position.</param>
        /// <returns>The position of the entity.</returns>
        public override Vector3 GetPosition(bool local)
        {
            LogSystem.LogWarning("[UIElementEntity->GetPosition] Cannot get position of a UI element entity.");

            return Vector3.zero;
        }

        /// <summary>
        /// Get the position of the entity as a percentage of its canvas.
        /// </summary>
        /// <param name="local">Whether or not to provide the local position.</param>
        /// <returns>The position of the entity as a percentage of its canvas.</returns>
        public Vector2 GetPositionPercent()
        {
            RectTransform rt = uiElementRectTransform;
            if (rt == null)
            {
                LogSystem.LogWarning("[UIElementEntity->GetPositionPercent] No rect transform.");
                return Vector3.zero;
            }

            CanvasEntity parentCanvasEntity = GetParentCanvasEntity();
            if (parentCanvasEntity == null)
            {
                LogSystem.LogError("[UIElementEntity->GetPositionPercent] No parent canvas entity.");
                return Vector3.zero;
            }

            RectTransform parentRT = parentCanvasEntity.GetComponent<RectTransform>();
            if (parentRT == null)
            {
                LogSystem.LogError("[UIElementEntity->GetPositionPercent] No parent canvas entity rect transform.");
                return Vector3.zero;
            }

            return new Vector2((rt.anchoredPosition.x - rt.sizeDelta.x / 2) / parentRT.sizeDelta.x,
                ((-1 * rt.anchoredPosition.y) - rt.sizeDelta.y / 2) / parentRT.sizeDelta.y);
        }

        /// <summary>
        /// Set the size of the entity as a percentage of its canvas.
        /// </summary>
        /// <param name="percent">Percent to set.</param>
        /// <param name="synchronize">Whether or not to synchronize the setting.</param>
        /// <returns>Whether or not the setting was successful.</returns>
        public virtual bool SetSizePercent(Vector2 percent, bool synchronize = true)
        {
            RectTransform rt = uiElementRectTransform;
            if (rt == null)
            {
                LogSystem.LogWarning("[UIElementEntity->SetSizePercent] No rect transform.");
                return false;
            }

            CanvasEntity parentCanvasEntity = GetParentCanvasEntity();
            if (parentCanvasEntity == null)
            {
                LogSystem.LogError("[UIElementEntity->SetSizePercent] No parent canvas entity.");
                return false;
            }

            RectTransform parentRT = parentCanvasEntity.GetComponent<RectTransform>();
            if (parentRT == null)
            {
                LogSystem.LogError("[UIElementEntity->SetSizePercent] No parent canvas entity rect transform.");
                return false;
            }

            Vector2 originalPosition = GetPositionPercent();

            Vector2 worldSize = new Vector2(parentRT.sizeDelta.x * percent.x, parentRT.sizeDelta.y * percent.y);

            rt.sizeDelta = worldSize;
            SetPositionPercent(originalPosition, false);

            return true;
        }

        /// <summary>
        /// Set the position of the entity. A UI element entity cannot have an absolute position.
        /// </summary>
        /// <param name="position">Position to set.</param>
        /// <param name="local">Whether or not the position is local.</param>
        /// <param name="synchronize">Whether or not to synchronize the setting.</param>
        /// <returns>Whether or not the setting was successful.</returns>
        public override bool SetPosition(Vector3 position, bool local, bool synchronize = true)
        {
            LogSystem.LogWarning("[UIElementEntity->SetPosition] Cannot set position of a UI element entity.");

            return false;
        }

        /// <summary>
        /// Set the position of the entity as a percentage of its canvas.
        /// </summary>
        /// <param name="percent">Percentage to set.</param>
        /// <param name="synchronize">Whether or not to synchronize the setting.</param>
        /// <returns>Whether or not the setting was successful.</returns>
        public virtual bool SetPositionPercent(Vector2 percent, bool synchronize = true)
        {
            RectTransform rt = uiElementRectTransform;
            if (rt == null)
            {
                LogSystem.LogWarning("[UIElementEntity->SetPositionPercent] No rect transform.");
                return false;
            }

            CanvasEntity parentCanvasEntity = GetParentCanvasEntity();
            if (parentCanvasEntity == null)
            {
                LogSystem.LogError("[UIElementEntity->SetPositionPercent] No parent canvas entity.");
                return false;
            }

            RectTransform parentRT = parentCanvasEntity.GetComponent<RectTransform>();
            if (parentRT == null)
            {
                LogSystem.LogError("[UIElementEntity->SetPositionPercent] No parent canvas entity rect transform.");
                return false;
            }

            Vector3 worldPos = new Vector3(parentRT.sizeDelta.x * percent.x + rt.sizeDelta.x / 2,
                -1 * parentRT.sizeDelta.y * percent.y - rt.sizeDelta.y / 2);

            rt.anchorMin = rt.anchorMax = new Vector2(0, 1);
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.localPosition = Vector3.zero;
            rt.anchoredPosition = worldPos;

            return true;
        }

        /// <summary>
        /// Set the rotation of the entity. Not a valid operation for UI element entities.
        /// </summary>
        /// <param name="rotation">Rotation to set.</param>
        /// <param name="local">Whether or not the rotation is local.</param>
        /// <param name="synchronize">Whether or not to synchronize the setting.</param>
        /// <returns>Whether or not the setting was successful.</returns>
        public override bool SetRotation(Quaternion rotation, bool local, bool synchronize = true)
        {
            LogSystem.LogError("[UIElementEntity->SetRotation] Cannot set rotation of a UI element entity.");

            return false;
        }

        /// <summary>
        /// Set the Euler rotation of the entity. Not valid for UI element entities.
        /// </summary>
        /// <param name="rotation">Rotation to set.</param>
        /// <param name="local">Whether or not the rotation is local.</param>
        /// <param name="synchronize">Whether or not to synchronize the setting.</param>
        /// <returns>Whether or not the setting was successful.</returns>
        public override bool SetEulerRotation(Vector3 rotation, bool local, bool synchronize = true)
        {
            LogSystem.LogError("[UIElementEntity->SetEulerRotation] Cannot set Euler rotation of a UI element entity.");

            return false;
        }

        /// <summary>
        /// Set the scale of the entity. Not a valid operation for UI element entities.
        /// </summary>
        /// <param name="scale">Scale to set.</param>
        /// <param name="synchronize">Whether or not to synchronize the setting.</param>
        /// <returns>Whether or not the setting was successful.</returns>
        public override bool SetScale(Vector3 scale, bool synchronize = true)
        {
            LogSystem.LogError("[UIElementEntity->SetScale] Cannot set scale of a UI element entity.");

            return false;
        }

        /// <summary>
        /// Initialize this entity. This method cannot be used for a UI element entity; it must be called with a parent canvas.
        /// </summary>
        /// <param name="idToSet">ID to apply to the entity.</param>
        public override void Initialize(Guid idToSet)
        {
            LogSystem.LogError("[UIElementEntity->Initialize] UI element entity must be initialized with a parent canvas.");

            return;
        }
        /// <summary>
        /// Initialize this entity. This should only be called once.
        /// </summary>
        /// <param name="idToSet">ID to apply to the entity.</param>
        public virtual void Initialize(Guid idToSet, CanvasEntity parentCanvas)
        {
            base.Initialize(idToSet);
        }

        /// <summary>
        /// Get the parent canvas entity of this entity.
        /// </summary>
        /// <returns>The parent canvas entity.</returns>
        protected CanvasEntity GetParentCanvasEntity()
        {
            return GetComponentInParent<CanvasEntity>(true);
        }
    }
}