// Copyright (c) 2019-2025 Five Squared Interactive. All rights reserved.

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using FiveSQD.StraightFour.Entity;
using TMPro;

public class UIEntityEnhancementTests
{
    [UnityTest]
    public IEnumerator UIElementEntity_StretchToParent_Test()
    {
        // Create parent canvas
        GameObject canvasGO = new GameObject("TestCanvas");
        CanvasEntity canvasEntity = canvasGO.AddComponent<CanvasEntity>();
        canvasEntity.Initialize(System.Guid.NewGuid());

        // Create UI element
        GameObject uiElementGO = new GameObject("TestUIElement");
        UIElementEntity uiElement = uiElementGO.AddComponent<UIElementEntity>();
        uiElement.Initialize(System.Guid.NewGuid(), canvasEntity);

        yield return null;

        // Test stretch to parent
        bool result = uiElement.StretchToParent();
        Assert.IsTrue(result, "StretchToParent should return true");

        RectTransform rt = uiElement.GetComponent<RectTransform>();
        Assert.IsNotNull(rt, "RectTransform should exist");
        Assert.AreEqual(Vector2.zero, rt.anchorMin, "AnchorMin should be (0,0)");
        Assert.AreEqual(Vector2.one, rt.anchorMax, "AnchorMax should be (1,1)");
        Assert.AreEqual(Vector2.zero, rt.sizeDelta, "SizeDelta should be (0,0)");

        // Cleanup
        Object.DestroyImmediate(canvasGO);
        Object.DestroyImmediate(uiElementGO);
    }

    [UnityTest]
    public IEnumerator UIElementEntity_SetAlignment_Test()
    {
        // Create parent canvas
        GameObject canvasGO = new GameObject("TestCanvas");
        CanvasEntity canvasEntity = canvasGO.AddComponent<CanvasEntity>();
        canvasEntity.Initialize(System.Guid.NewGuid());

        // Create UI element
        GameObject uiElementGO = new GameObject("TestUIElement");
        UIElementEntity uiElement = uiElementGO.AddComponent<UIElementEntity>();
        uiElement.Initialize(System.Guid.NewGuid(), canvasEntity);

        yield return null;

        // Test center alignment
        bool result = uiElement.SetAlignment(UIElementAlignment.Center);
        Assert.IsTrue(result, "SetAlignment should return true");

        RectTransform rt = uiElement.GetComponent<RectTransform>();
        Assert.IsNotNull(rt, "RectTransform should exist");
        Assert.AreEqual(new Vector2(0.5f, 0.5f), rt.anchorMin, "AnchorMin should be (0.5, 0.5) for center");
        Assert.AreEqual(new Vector2(0.5f, 0.5f), rt.anchorMax, "AnchorMax should be (0.5, 0.5) for center");

        // Test left alignment
        result = uiElement.SetAlignment(UIElementAlignment.Left);
        Assert.IsTrue(result, "SetAlignment should return true");
        Assert.AreEqual(new Vector2(0f, 0.5f), rt.anchorMin, "AnchorMin should be (0, 0.5) for left");
        Assert.AreEqual(new Vector2(0f, 0.5f), rt.anchorMax, "AnchorMax should be (0, 0.5) for left");

        // Cleanup
        Object.DestroyImmediate(canvasGO);
        Object.DestroyImmediate(uiElementGO);
    }

    [UnityTest]
    public IEnumerator TextEntity_TextFormatting_Test()
    {
        // Create parent canvas
        GameObject canvasGO = new GameObject("TestCanvas");
        CanvasEntity canvasEntity = canvasGO.AddComponent<CanvasEntity>();
        canvasEntity.Initialize(System.Guid.NewGuid());

        // Create text entity
        GameObject textGO = new GameObject("TestText");
        TextEntity textEntity = textGO.AddComponent<TextEntity>();
        textEntity.Initialize(System.Guid.NewGuid(), canvasEntity);

        yield return null;

        // Test bold
        bool result = textEntity.SetBold(true);
        Assert.IsTrue(result, "SetBold should return true");
        Assert.IsTrue(textEntity.GetBold(), "Text should be bold");

        // Test italic
        result = textEntity.SetItalic(true);
        Assert.IsTrue(result, "SetItalic should return true");
        Assert.IsTrue(textEntity.GetItalic(), "Text should be italic");

        // Test underline
        result = textEntity.SetUnderline(true);
        Assert.IsTrue(result, "SetUnderline should return true");
        Assert.IsTrue(textEntity.GetUnderline(), "Text should be underlined");

        // Test strikethrough
        result = textEntity.SetStrikethrough(true);
        Assert.IsTrue(result, "SetStrikethrough should return true");
        Assert.IsTrue(textEntity.GetStrikethrough(), "Text should have strikethrough");

        // Test text alignment
        result = textEntity.SetTextAlignment(TextAlignment.Center);
        Assert.IsTrue(result, "SetTextAlignment should return true");
        Assert.AreEqual(TextAlignment.Center, textEntity.GetTextAlignment(), "Text alignment should be center");

        // Test text wrapping
        result = textEntity.SetTextWrapping(TextWrapping.Wrap);
        Assert.IsTrue(result, "SetTextWrapping should return true");
        Assert.AreEqual(TextWrapping.Wrap, textEntity.GetTextWrapping(), "Text wrapping should be enabled");

        // Cleanup
        Object.DestroyImmediate(canvasGO);
        Object.DestroyImmediate(textGO);
    }
}