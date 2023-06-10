// Copyright (c) 2019-2023 Five Squared Interactive. All rights reserved.

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using FiveSQD.WebVerse.WorldEngine.Entity;
using System;
using FiveSQD.WebVerse.WorldEngine;
using UnityEditor;
using System.Collections.Generic;

public class EntityManagerTests
{
    [UnityTest]
    public IEnumerator EntityManagerTests_General()
    {
        List<Guid> eIDs = new List<Guid>();

        // Initialize World Engine and Load World.
        GameObject WEGO = new GameObject();
        WorldEngine we = WEGO.AddComponent<WorldEngine>();
        we.characterControllerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/World Engine/Entity/Character/Prefabs/UserAvatar.prefab");
        we.voxelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/World Engine/Entity/Voxel/Prefabs/Voxel.prefab");
        we.inputEntityPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/World Engine/Entity/UI/UIElement/Input/Prefabs/InputEntity.prefab");
        yield return null;
        WorldEngine.LoadWorld("test");

        // Load Container Entity.
        Guid id = WorldEngine.ActiveWorld.entityManager.LoadContainerEntity(null, Vector3.zero, Quaternion.identity, Vector3.one);
        Assert.IsNotNull(id);
        eIDs.Add(id);

        // Load Character Entity.
        id = WorldEngine.ActiveWorld.entityManager.LoadCharacterEntity(null, Vector3.zero, Quaternion.identity, Vector3.one);
        Assert.IsNotNull(id);
        eIDs.Add(id);

        // Load Light Entity.
        id = WorldEngine.ActiveWorld.entityManager.LoadLightEntity(null, Vector3.zero, Quaternion.identity);
        Assert.IsNotNull(id);
        eIDs.Add(id);

        // Load Terrain Entity.
        float[,] heights = new float[256, 256];
        for (int i = 0; i < 256; i++)
        {
            for (int j = 0; j < 256; j++)
            {
                heights[i, j] = i;
            }
        }
        id = WorldEngine.ActiveWorld.entityManager.LoadTerrainEntity(256, 256, 256,
            heights, null, Vector3.zero, Quaternion.identity, Vector3.one);
        Assert.IsNotNull(id);
        eIDs.Add(id);

        // Load Canvas Entity.
        Guid cId = WorldEngine.ActiveWorld.entityManager.LoadCanvasEntity(null, Vector3.zero, Quaternion.identity, Vector3.one);
        Assert.IsNotNull(cId);
        eIDs.Add(cId);

        // Wait for canvas entity to load.
        yield return new WaitForSeconds(3);
        CanvasEntity ce = (CanvasEntity) WorldEngine.ActiveWorld.entityManager.FindEntity(cId);
        Assert.NotNull(ce);

        // Load Text Entity.
        id = WorldEngine.ActiveWorld.entityManager.LoadTextEntity("qwerty", 12, ce, Vector3.zero, Vector3.one);
        Assert.IsNotNull(id);
        eIDs.Add(id);

        // Load Button Entity.
        id = WorldEngine.ActiveWorld.entityManager.LoadButtonEntity(ce, Vector3.zero, Vector3.one, null);
        Assert.IsNotNull(id);
        eIDs.Add(id);

        // Load Input Entity.
        id = WorldEngine.ActiveWorld.entityManager.LoadInputEntity(ce, Vector3.zero, Vector3.one);
        Assert.IsNotNull(id);
        eIDs.Add(id);

        // Load Voxel Entity.
        id = WorldEngine.ActiveWorld.entityManager.LoadVoxelEntity(null, Vector3.zero, Quaternion.identity, Vector3.one);
        Assert.IsNotNull(id);
        eIDs.Add(id);

        // Exists/Find Entity.
        foreach (Guid eID in eIDs)
        {
            Assert.True(WorldEngine.ActiveWorld.entityManager.Exists(eID));
            Assert.IsNotNull(WorldEngine.ActiveWorld.entityManager.FindEntity(eID));
        }

        // Get All Entities.
        BaseEntity[] entities = WorldEngine.ActiveWorld.entityManager.GetAllEntities();
        Assert.AreEqual(eIDs.Count, entities.Length);

        // Unload.
        WorldEngine.ActiveWorld.entityManager.Unload();
        Assert.AreEqual(0, WorldEngine.ActiveWorld.entityManager.GetAllEntities().Length);
    }
}