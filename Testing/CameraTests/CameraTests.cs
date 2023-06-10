// Copyright (c) 2019-2023 Five Squared Interactive. All rights reserved.

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using FiveSQD.WebVerse.WorldEngine;

public class CameraTests
{
    [UnityTest]
    public IEnumerator CameraTests_General()
    {
        // Initialize Camera.
        GameObject camGO = new GameObject();
        Camera camera = camGO.AddComponent<Camera>();
        camera.transform.position = new Vector3(0, 0, -100);
        camGO.tag = "MainCamera";

        // Initialize World Engine and Load World.
        GameObject WEGO = new GameObject();
        WorldEngine we = WEGO.AddComponent<WorldEngine>();
        yield return null;
        WorldEngine.LoadWorld("test");

        Assert.IsNotNull(WorldEngine.ActiveWorld.cameraManager);

        GameObject parentGO = new GameObject("parent");

        // Set Parent.
        WorldEngine.ActiveWorld.cameraManager.SetParent(parentGO);
        Assert.AreEqual(parentGO.transform, WorldEngine.ActiveWorld.cameraManager.cam.transform.parent);

        // Set Position.
        WorldEngine.ActiveWorld.cameraManager.SetPosition(new Vector3(1, 2, 3), false);
        Assert.AreEqual(new Vector3(1, 2, 3), WorldEngine.ActiveWorld.cameraManager.cam.transform.position);
        WorldEngine.ActiveWorld.cameraManager.SetPosition(new Vector3(3, 4, 5), true);
        Assert.AreEqual(new Vector3(3, 4, 5), WorldEngine.ActiveWorld.cameraManager.cam.transform.localPosition);

        // Set Rotation.
        WorldEngine.ActiveWorld.cameraManager.SetRotation(new Quaternion(0.1f, 0.2f, 0.3f, 0.4f), false);
        WorldEngine.ActiveWorld.cameraManager.SetRotation(new Quaternion(0.3f, 0.4f, 0.5f, 0.6f), true);

        // Set Euler Rotation.
        WorldEngine.ActiveWorld.cameraManager.SetEulerRotation(new Vector3(1, 2, 3), false);
        WorldEngine.ActiveWorld.cameraManager.SetEulerRotation(new Vector3(4, 5, 6), true);

        // Set Scale.
        WorldEngine.ActiveWorld.cameraManager.SetScale(new Vector3(1, 2, 3));
        Assert.AreEqual(new Vector3(1, 2, 3), WorldEngine.ActiveWorld.cameraManager.cam.transform.localScale);
    }
}