// Copyright (c) 2019-2025 Five Squared Interactive. All rights reserved.

using UnityEngine;
using FiveSQD.StraightFour.Entity;

namespace FiveSQD.StraightFour.Examples
{
    /// <summary>
    /// Example demonstrating the new UI entity enhancements.
    /// </summary>
    public class UIEntityEnhancementDemo : MonoBehaviour
    {
        void Start()
        {
            DemonstrateUIEnhancements();
        }

        void DemonstrateUIEnhancements()
        {
            // Create a canvas
            GameObject canvasGO = new GameObject("DemoCanvas");
            CanvasEntity canvas = canvasGO.AddComponent<CanvasEntity>();
            canvas.Initialize(System.Guid.NewGuid());

            // Demo 1: UIElementEntity with stretch to parent
            GameObject uiElementGO = new GameObject("StretchedElement");
            UIElementEntity uiElement = uiElementGO.AddComponent<UIElementEntity>();
            uiElement.Initialize(System.Guid.NewGuid(), canvas);
            uiElement.StretchToParent();
            Debug.Log("Created UI element that stretches to parent");

            // Demo 2: UIElementEntity with center alignment
            GameObject centeredElementGO = new GameObject("CenteredElement");
            UIElementEntity centeredElement = centeredElementGO.AddComponent<UIElementEntity>();
            centeredElement.Initialize(System.Guid.NewGuid(), canvas);
            centeredElement.SetAlignment(UIElementAlignment.Center);
            centeredElement.SetSizePercent(new Vector2(0.5f, 0.3f));
            Debug.Log("Created centered UI element");

            // Demo 3: TextEntity with enhanced formatting
            GameObject textGO = new GameObject("FormattedText");
            TextEntity textEntity = textGO.AddComponent<TextEntity>();
            textEntity.Initialize(System.Guid.NewGuid(), canvas);
            
            // Apply text formatting
            textEntity.SetText("Enhanced Text Demo");
            textEntity.SetBold(true);
            textEntity.SetItalic(true);
            textEntity.SetUnderline(true);
            textEntity.SetTextAlignment(TextAlignment.Center);
            textEntity.SetTextWrapping(TextWrapping.Wrap);
            textEntity.SetColor(Color.blue);
            textEntity.SetSizePercent(new Vector2(0.8f, 0.2f));
            textEntity.SetPositionPercent(new Vector2(0.1f, 0.1f));
            Debug.Log("Created text with bold, italic, underlined formatting and center alignment");

            // Demo 4: ButtonEntity with colors (already implemented)
            GameObject buttonGO = new GameObject("ColoredButton");
            ButtonEntity buttonEntity = buttonGO.AddComponent<ButtonEntity>();
            buttonEntity.Initialize(System.Guid.NewGuid(), canvas);
            buttonEntity.SetColors(Color.green, Color.yellow, Color.red, Color.gray);
            buttonEntity.SetSizePercent(new Vector2(0.3f, 0.1f));
            buttonEntity.SetPositionPercent(new Vector2(0.35f, 0.8f));
            Debug.Log("Created button with custom colors");

            Debug.Log("UI Entity Enhancement Demo completed successfully!");
        }
    }
}