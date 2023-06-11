// Copyright (c) 2019-2023 Five Squared Interactive. All rights reserved.

using System;
using UnityEngine;
using UnityEngine.UI;

namespace FiveSQD.WebVerse.WorldEngine.Entity
{
    /// <summary>
    /// Class for a button entity.
    /// </summary>
    public class ButtonEntity : UIElementEntity
    {
        /// <summary>
        /// Image object for the button entity.
        /// </summary>
        public Image imageObject;

        /// <summary>
        /// Button object for the button entity.
        /// </summary>
        public Button buttonObject;

        /// <summary>
        /// Set the onClick event for the button entity.
        /// </summary>
        /// <param name="onClick">Action to perform on click.</param>
        public void SetOnClick(Action onClick)
        {
            if (buttonObject == null)
            {
                Utilities.LogSystem.LogError("[ButtonEntity->SetOnClick] No button.");
                return;
            }

            buttonObject.onClick = new Button.ButtonClickedEvent();
            buttonObject.onClick.AddListener(() => {
                if (onClick != null)
                {
                    onClick.Invoke();
                }
            });
        }

        /// <summary>
        /// Initialize this entity. This should only be called once.
        /// </summary>
        /// <param name="idToSet">ID to apply to the entity.</param>
        /// <param name="parentCanvas">Canvas to place the button entity on.</param>
        public override void Initialize(Guid idToSet, CanvasEntity parentCanvas)
        {
            base.Initialize(idToSet, parentCanvas);

            imageObject = gameObject.AddComponent<Image>();

            buttonObject = gameObject.AddComponent<Button>();
            buttonObject.targetGraphic = imageObject;
            RectTransform rt = buttonObject.GetComponent<RectTransform>();
            if (rt == null)
            {
                rt = buttonObject.gameObject.AddComponent<RectTransform>();
            }
            rt.position = Vector3.zero;
            rt.anchorMin = rt.anchorMax = Vector2.zero;
            uiElementRectTransform = rt;

            GraphicRaycaster graphicRaycaster = gameObject.AddComponent<GraphicRaycaster>();

            SetParent(parentCanvas);

            MakeHidden();
        }
    }
}