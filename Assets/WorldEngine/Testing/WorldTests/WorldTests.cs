// Copyright (c) 2019-2023 Five Squared Interactive. All rights reserved.

using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using FiveSQD.WebVerse.WorldEngine.World;
using UnityEditor;
using System.Collections;

public class WorldTests
{
    [UnityTest]
    public IEnumerator WorldTests_World()
    {
        GameObject worldGO = new GameObject();
        World world = worldGO.AddComponent<World>();

        // Initialize.
        World.WorldInfo worldInfo = new World.WorldInfo()
        {
            characterControllerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/WorldEngine/Entity/Character/Prefabs/UserAvatar.prefab"),
            highlightMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit")),
            inputEntityPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/WorldEngine/Entity/UI/UIElement/Input/Prefabs/InputEntity.prefab"),
            maxEntryLength = 128,
            maxKeyLength = 16,
            maxStorageEntries = 16,
            skyMaterial = AssetDatabase.LoadAssetAtPath<Material>("Assets/WorldEngine/Environment/Materials/skybox.mat"),
            voxelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/WorldEngine/Entity/Voxel/Prefabs/Voxel.prefab")
        };
        world.Initialize(worldInfo);
        Assert.IsNotNull(world.entityManager);
        Assert.IsNotNull(world.storageManager);
        Assert.IsNotNull(world.cameraManager);
        Assert.IsNotNull(world.materialManager);

        // Unload.
        world.Unload();
        yield return null;
        Assert.IsTrue(world.entityManager == null);
        Assert.IsTrue(world.storageManager == null);
        Assert.IsTrue(world.cameraManager == null);
        Assert.IsTrue(world.materialManager == null);
    }
}