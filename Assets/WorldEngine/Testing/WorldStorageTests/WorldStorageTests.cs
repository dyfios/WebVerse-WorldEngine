// Copyright (c) 2019-2025 Five Squared Interactive. All rights reserved.

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using FiveSQD.StraightFour.WorldEngine;
using UnityEditor;

public class WorldStorageTests
{
    [UnityTest]
    public IEnumerator WorldStorageTests_General()
    {
        // Initialize World Engine and Load World.
        GameObject WEGO = new GameObject();
        WorldEngine we = WEGO.AddComponent<WorldEngine>();
        we.skyMaterial = AssetDatabase.LoadAssetAtPath<Material>("Assets/WorldEngine/Environment/Materials/skybox.mat");
        yield return null;
        WorldEngine.LoadWorld("test");

        Assert.IsNotNull(WorldEngine.ActiveWorld.storageManager);

        WorldEngine.ActiveWorld.storageManager.Initialize(16, 16, 16);
        WorldEngine.ActiveWorld.storageManager.SetItem("key", "value");
        Assert.AreEqual("value", WorldEngine.ActiveWorld.storageManager.GetItem("key"));
        WorldEngine.ActiveWorld.storageManager.SetItem("key", "newvalue");
        Assert.AreEqual("newvalue", WorldEngine.ActiveWorld.storageManager.GetItem("key"));
        Assert.AreEqual(null, WorldEngine.ActiveWorld.storageManager.GetItem("nonexistent"));
        WorldEngine.ActiveWorld.storageManager.SetItem("largestkey......", "largestvalue....");
        Assert.AreEqual("largestvalue....", WorldEngine.ActiveWorld.storageManager.GetItem("largestkey......"));
        WorldEngine.ActiveWorld.storageManager.SetItem("toolargekey......", "value");
        Assert.AreEqual("value", WorldEngine.ActiveWorld.storageManager.GetItem("toolargekey....."));
        WorldEngine.ActiveWorld.storageManager.SetItem("somekey", "toolargevalue....");
        Assert.AreEqual("toolargevalue...", WorldEngine.ActiveWorld.storageManager.GetItem("somekey"));
        for (int i = 4; i < 16; i++)
        {
            WorldEngine.ActiveWorld.storageManager.SetItem("key" + i, "value" + i);
            Assert.AreEqual("value" + i, WorldEngine.ActiveWorld.storageManager.GetItem("key" + i));
        }
        LogAssert.Expect(LogType.Warning, "[WorldStorageManager->SetItem] World Storage full.");
        WorldEngine.ActiveWorld.storageManager.SetItem("key16", "value16");
    }
}