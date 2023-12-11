// Copyright (c) 2019-2023 Five Squared Interactive. All rights reserved.

using System;
using System.Collections.Generic;
using UnityEngine;
using FiveSQD.WebVerse.WorldEngine.Utilities;

namespace FiveSQD.WebVerse.WorldEngine.Entity
{
    /// <summary>
    /// The Entity Manager class manages all of the entities in a world and
    /// serves as the top-level entity interface to other components.
    /// </summary>
    public class EntityManager : BaseManager
    {
        /// <summary>
        /// Prefab for a character controller.
        /// </summary>
        [Tooltip("Prefab for a character controller.")]
        public GameObject characterControllerPrefab;

        /// <summary>
        /// Prefab for an input entity.
        /// </summary>
        [Tooltip("Prefab for an input entity.")]
        public GameObject inputEntityPrefab;

        /// <summary>
        /// Prefab for a voxel.
        /// </summary>
        [Tooltip("Prefab for a voxel.")]
        public GameObject voxelPrefab;

        /// <summary>
        /// Dictionary of loaded entities.
        /// </summary>
        private Dictionary<Guid, BaseEntity> entities = new Dictionary<Guid, BaseEntity>();
        
        /// <summary>
        /// Get a new entity ID.
        /// </summary>
        private Guid GetEntityID()
        {
            return Guid.NewGuid();
        }

        // Initialize the Entity Manager.
        public override void Initialize()
        {
            
        }
        
        /// <summary>
        /// Load a container entity.
        /// </summary>
        /// <param name="parentEntity">Parent entity to give the container entity.</param>
        /// <param name="position">Position to apply to the container entity.</param>
        /// <param name="rotation">Rotation to apply to the container entity.</param>
        /// <param name="scale">Scale/size to apply to the container entity.</param>
        /// <param name="id">ID to apply to the container entity.</param>
        /// <param name="isSize">Whether or not the scale value is for a size.</param>
        /// <param name="tag">Tag to give the container entity.</param>
        /// <param name="onLoaded">Action to perform when loading is complete.</param>
        /// <returns>The ID of the new container entity.</returns>
        public Guid LoadContainerEntity(BaseEntity parentEntity,
            Vector3 position, Quaternion rotation, Vector3 scale, Guid? id = null,
            string tag = null, bool isSize = false, Action onLoaded = null)
        {
            Guid entityID = id.HasValue ? id.Value : GetEntityID();
            StartCoroutine(LoadEmptyEntity(entityID, parentEntity,
                position, rotation, scale, isSize, tag, onLoaded));
            return entityID;
        }

        /// <summary>
        /// Load a character entity.
        /// </summary>
        /// <param name="parentEntity">Parent entity to give the character entity.</param>
        /// <param name="position">Position to apply to the character entity.</param>
        /// <param name="rotation">Rotation to apply to the character entity.</param>
        /// <param name="scale">Scale/size to apply to the character entity.</param>
        /// <param name="id">ID to apply to the character entity.</param>
        /// <param name="tag">Tag to apply to the character entity.</param>
        /// <param name="isSize">Whether or not the scale value is for a size.</param>
        /// <param name="onLoaded">Action to perform when loading is complete.</param>
        /// <returns>The ID of the new character entity.</returns>
        public Guid LoadCharacterEntity(BaseEntity parentEntity,
            Vector3 position, Quaternion rotation, Vector3 scale, Guid? id = null,
            string tag = null, bool isSize = false, Action onLoaded = null)
        {
            Guid entityID = id.HasValue ? id.Value : GetEntityID();
            StartCoroutine(LoadDefaultCharacterEntity(entityID, tag,
                parentEntity, position, rotation, scale, isSize, onLoaded));
            return entityID;
        }

        /// <summary>
        /// Load a light entity.
        /// </summary>
        /// <param name="parentEntity">Parent entity to give the light entity.</param>
        /// <param name="position">Position to apply to the light entity.</param>
        /// <param name="rotation">Rotation to apply to the light entity.</param>
        /// <param name="id">ID to apply to the light entity.</param>
        /// <param name="tag">Tag to apply to the light entity.</param>
        /// <param name="onLoaded">Action to perform when loading is complete.</param>
        /// <returns>The ID of the new light entity.</returns>
        public Guid LoadLightEntity(BaseEntity parentEntity,
            Vector3 position, Quaternion rotation, Guid? id = null,
            string tag = null, Action onLoaded = null)
        {
            Guid entityID = id.HasValue ? id.Value : GetEntityID();
            StartCoroutine(LoadLightEntity(entityID, parentEntity,
                position, rotation, tag, onLoaded));
            return entityID;
        }

        /// <summary>
        /// Load a mesh entity.
        /// </summary>
        /// <param name="parentEntity">Parent entity to give the mesh entity.</param>
        /// <param name="meshPrefab">Prefab to load mesh entity from.</param>
        /// <param name="position">Position to apply to the mesh entity.</param>
        /// <param name="rotation">Rotation to apply to the mesh entity.</param>
        /// <param name="id">ID to apply to the mesh entity.</param>
        /// <param name="tag">Tag to apply to the mesh entity.</param>
        /// <param name="onLoaded">Action to perform when loading is complete.</param>
        /// <returns>The ID of the new mesh entity.</returns>
        public Guid LoadMeshEntity(BaseEntity parentEntity, GameObject meshPrefab,
            Vector3 position, Quaternion rotation, Guid? id = null,
            string tag = null, Action onLoaded = null)
        {
            Guid entityID = id.HasValue ? id.Value : GetEntityID();
            StartCoroutine(LoadMeshEntity(meshPrefab, entityID, parentEntity,
                position, rotation, tag, onLoaded));
            return entityID;
        }

        /// <summary>
        /// Load a terrain entity.
        /// </summary>
        /// <param name="length">Length of the terrain.</param>
        /// <param name="width">Width of the terrain.</param>
        /// <param name="height">Height of the terrain.</param>
        /// <param name="heights">2D array of heights for the terrain.</param>
        /// <param name="parentEntity">Parent entity to give the terrain entity.</param>
        /// <param name="position">Position to apply to the terrain entity.</param>
        /// <param name="rotation">Rotation to apply to the terrain entity.</param>
        /// <param name="scale">Scale/size to apply to the terrain entity.</param>
        /// <param name="id">ID to apply to the terrain entity.</param>
        /// <param name="isSize">Whether or not the scale value is for a size.</param>
        /// <param name="tag">Tag to apply to the terrain entity.</param>
        /// <param name="onLoaded">Action to perform when loading is complete.</param>
        /// <returns>The ID of the new terrain entity.</returns>
        public Guid LoadTerrainEntity(float length, float width, float height,
            float[,] heights, BaseEntity parentEntity,
            Vector3 position, Quaternion rotation, Vector3 scale, Guid? id = null,
            bool isSize = false, string tag = null, Action onLoaded = null)
        {
            Guid entityID = id.HasValue ? id.Value : GetEntityID();
            StartCoroutine(LoadTerrainEntity(length, width, height, heights, entityID,
                parentEntity, position, rotation, tag, onLoaded));
            return entityID;
        }

        /// <summary>
        /// Load a canvas entity.
        /// </summary>
        /// <param name="parentEntity">Parent entity to give the canvas entity.</param>
        /// <param name="position">Position to apply to the canvas entity.</param>
        /// <param name="rotation">Rotation to apply to the canvas entity.</param>
        /// <param name="scale">Scale/size to apply to the canvas entity.</param>
        /// <param name="id">ID to apply to the canvas entity.</param>
        /// <param name="isSize">Whether or not the scale value is for a size.</param>
        /// <param name="tag">Tag to apply to the canvas entity.</param>
        /// <param name="onLoaded">Action to perform when loading is complete.</param>
        /// <returns>The ID of the new canvas entity.</returns>
        public Guid LoadCanvasEntity(BaseEntity parentEntity,
            Vector3 position, Quaternion rotation, Vector3 scale,
            Guid? id = null, bool isSize = false,
            string tag = null, Action onLoaded = null)
        {
            Guid entityID = id.HasValue ? id.Value : GetEntityID();
            StartCoroutine(LoadCanvasEntity(entityID, parentEntity,
                position, rotation, scale, isSize, tag, onLoaded));

            return entityID;
        }

        /// <summary>
        /// Load a text entity.
        /// </summary>
        /// <param name="text">Text to place in the text entity.</param>
        /// <param name="fontSize">Font size to apply to the text entity.</param>
        /// <param name="parentEntity">Parent entity to give the text entity.</param>
        /// <param name="positionPercent">Position to apply to the text entity
        /// as a percentage of the canvas.</param>
        /// <param name="sizePercent">Size to apply to the text entity
        /// as a percentage of the canvas.</param>
        /// <param name="id">ID to apply to the text entity.</param>
        /// <param name="onLoaded">Action to perform when loading is complete.</param>
        /// <param name="tag">Tag to apply to the text entity.</param>
        /// <returns>The ID of the new text entity.</returns>
        public Guid LoadTextEntity(string text, int fontSize, CanvasEntity parentEntity,
            Vector2 positionPercent, Vector2 sizePercent, Guid? id = null,
            string tag = null, Action onLoaded = null)
        {
            Guid entityID = id.HasValue ? id.Value : GetEntityID();
            StartCoroutine(LoadTextEntity(text, fontSize, entityID,
                parentEntity, positionPercent, sizePercent, tag, onLoaded));
            return entityID;
        }

        /// <summary>
        /// Load a button entity.
        /// </summary>
        /// <param name="parentEntity">Parent entity to give the button entity.</param>
        /// <param name="positionPercent">Position to apply to the button entity
        /// as a percentage of the canvas.</param>
        /// <param name="sizePercent">Size to apply to the button entity
        /// as a percentage of the canvas.</param>
        /// <param name="onClick">Action to perform on click of the button.</param>
        /// <param name="id">ID to apply to the button entity.</param>
        /// <param name="tag">Tag to apply to the button entity.</param>
        /// <param name="onLoaded">Action to perform when loading is complete.</param>
        /// <returns>The ID of the new button entity.</returns>
        public Guid LoadButtonEntity(CanvasEntity parentEntity,
            Vector2 positionPercent, Vector2 sizePercent, Action onClick, Guid? id = null,
            string tag = null, Action onLoaded = null)
        {
            Guid entityID = id.HasValue ? id.Value : GetEntityID();
            StartCoroutine(LoadButtonEntity(entityID,
                parentEntity, positionPercent, sizePercent, onClick, tag, onLoaded));
            return entityID;
        }

        /// <summary>
        /// Load an input entity.
        /// </summary>
        /// <param name="parentEntity">Parent entity to give the input entity.</param>
        /// <param name="positionPercent">Position to apply to the input entity
        /// as a percentage of the canvas.</param>
        /// <param name="sizePercent">Size to apply to the input entity
        /// as a percentage of the canvas.</param>
        /// <param name="id">ID to apply to the input entity.</param>
        /// <param name="tag">Tag to apply to the input entity.</param>
        /// <param name="onLoaded">Action to perform when loading is complete.</param>
        /// <returns>The ID of the new input entity.</returns>
        public Guid LoadInputEntity(CanvasEntity parentEntity,
            Vector2 positionPercent, Vector2 sizePercent, Guid? id = null,
            string tag = null, Action onLoaded = null)
        {
            Guid entityID = id.HasValue ? id.Value : GetEntityID();
            StartCoroutine(LoadInputEntity(entityID,
                parentEntity, positionPercent, sizePercent, tag, onLoaded));
            return entityID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentEntity">Parent entity to give the voxel entity.</param>
        /// <param name="position">Position to apply to the voxel entity.</param>
        /// <param name="rotation">Rotation to apply to the voxel entity.</param>
        /// <param name="scale">Scale to apply to the voxel entity.</param>
        /// <param name="id">ID to apply to the voxel entity.</param>
        /// <param name="tag">Tag to apply to the voxel entity.</param>
        /// <param name="onLoaded">Action to perform when loading is complete.</param>
        /// <returns>The ID of the new voxel entity.</returns>
        public Guid LoadVoxelEntity(BaseEntity parentEntity,
            Vector3 position, Quaternion rotation, Vector3 scale, Guid? id = null,
            string tag = null, Action onLoaded = null)
        {
            Guid entityID = id.HasValue ? id.Value : GetEntityID();
            StartCoroutine(LoadVoxelEntity(entityID,
                parentEntity, position, rotation, scale, tag, onLoaded));
            return entityID;
        }

        /// <summary>
        /// Loads the default character entity model.
        /// </summary>
        /// <param name="id">ID of the character entity.</param>
        /// <param name="tag">Tag of the character entity.</param>
        /// <param name="parent">Parent of the character entity.</param>
        /// <param name="position">Position of the character entity.</param>
        /// <param name="rotation">Rotation of the character entity.</param>
        /// <param name="scale">Scale/size of the character entity.</param>
        /// <param name="isSize">Whether or not the scale value is for a size.</param>
        /// <param name="onLoaded">Action to perform when loading is complete.</param>
        /// <param name="radius">Radius of the character entity model.</param>
        /// <param name="height">Height of the character entity model.</param>
        /// <param name="center">Center of the character entity model.</param>
        /// <returns>Coroutine, completes after invocation of the onLoaded action.</returns>
        private System.Collections.IEnumerator LoadDefaultCharacterEntity(Guid id, string tag, BaseEntity parent,
            Vector3 position, Quaternion rotation, Vector3 scale, bool isSize, Action onLoaded,
            float radius = 0.22f, float height = 1.69f, float center = 0.854f)
        {
            GameObject characterEntityObject = new GameObject();
            characterEntityObject.name = "CharacterEntity-" + id.ToString();
            CharacterEntity entity = characterEntityObject.AddComponent<CharacterEntity>();
            entities.Add(id, entity);
            entity.SetParent(parent);
            entity.SetPosition(position, true);
            entity.SetRotation(rotation, true);
            if (isSize)
            {
                entity.SetSize(scale);
            }
            else
            {
                entity.SetScale(scale);
            }
            entity.center = center;
            entity.radius = radius;
            entity.height = height;
            entity.Initialize(id);
            entity.entityTag = tag == null ? "" : tag;

            if (onLoaded != null)
            {
                onLoaded.Invoke();
            }
            
            yield return null;
        }

        /// <summary>
        /// Loads an empty entity.
        /// </summary>
        /// <param name="id">ID of the empty entity.</param>
        /// <param name="parent">Parent of the empty entity.</param>
        /// <param name="position">Position of the empty entity.</param>
        /// <param name="rotation">Rotation of the empty entity.</param>
        /// <param name="scale">Scale/size of the empty entity.</param>
        /// <param name="isSize">Whether or not the scale value is for a size.</param>
        /// <param name="tag">Tag of the empty entity.</param>
        /// <param name="onLoaded">Action to perform when loading is complete.</param>
        /// <returns>Coroutine, completes after invocation of the onLoaded action.</returns>
        private System.Collections.IEnumerator LoadEmptyEntity(Guid id, BaseEntity parent,
            Vector3 position, Quaternion rotation, Vector3 scale, bool isSize,
            string tag, Action onLoaded)
        {
            GameObject emptyEntityObject = new GameObject("EmptyEntity-" + id.ToString());
            ContainerEntity entity = emptyEntityObject.AddComponent<ContainerEntity>();
            entities.Add(id, entity);
            entity.SetParent(parent);
            entity.entityTag = tag;
            entity.SetPosition(position, true);
            entity.SetRotation(rotation, true);
            if (isSize)
            {
                entity.SetSize(scale);
            }
            else
            {
                entity.SetScale(scale);
            }
            entity.Initialize(id);

            if (onLoaded != null)
            {
                onLoaded.Invoke();
            }

            yield return null;
        }

        /// <summary>
        /// Loads a light entity.
        /// </summary>
        /// <param name="id">ID of the light entity.</param>
        /// <param name="parent">Parent of the light entity.</param>
        /// <param name="position">Position of the light entity.</param>
        /// <param name="rotation">Rotation of the light entity.</param>
        /// <param name="tag">Tag of the light entity</param>
        /// <param name="onLoaded">Action to perform when loading is complete.</param>
        /// <returns>Coroutine, completes after invocation of the onLoaded action.</returns>
        private System.Collections.IEnumerator LoadLightEntity(Guid id, BaseEntity parent,
            Vector3 position, Quaternion rotation, string tag, Action onLoaded)
        {
            GameObject lightEntityObject = new GameObject("LightEntity-" + id.ToString());
            LightEntity entity = lightEntityObject.AddComponent<LightEntity>();
            entities.Add(id, entity);
            entity.SetParent(parent);
            entity.entityTag = tag;
            entity.SetPosition(position, true);
            entity.SetRotation(rotation, true);
            entity.Initialize(id);

            if (onLoaded != null)
            {
                onLoaded.Invoke();
            }

            yield return null;
        }

        /// <summary>
        /// Loads a mesh entity.
        /// </summary>
        /// <param name="meshPrefab">Prefab to load the mesh entity from.</param>
        /// <param name="id">ID of the mesh entity.</param>
        /// <param name="parent">Parent of the mesh entity.</param>
        /// <param name="position">Position of the mesh entity.</param>
        /// <param name="rotation">Rotation of the mesh entity.</param>
        /// <param name="tag">Tag of the mesh entity.</param>
        /// <param name="onLoaded">Action to perform when loading is complete.</param>
        /// <returns>Coroutine, completes after invocation of the OnLoaded action.</returns>
        private System.Collections.IEnumerator LoadMeshEntity(GameObject meshPrefab, Guid id, BaseEntity parent,
            Vector3 position, Quaternion rotation, string tag, Action onLoaded)
        {
            GameObject meshGO = Instantiate(meshPrefab);
            meshGO.name = "MeshEntity-" + id.ToString();
            MeshEntity entity = meshGO.AddComponent<MeshEntity>();
            entities.Add(id, entity);
            entity.SetParent(parent);
            entity.entityTag = tag;
            entity.SetPosition(position, true);
            entity.SetRotation(rotation, true);
            entity.Initialize(id);

            if (onLoaded != null)
            {
                onLoaded.Invoke();
            }

            yield return null;
        }

        /// <summary>
        /// Loads a terrain entity.
        /// </summary>
        /// <param name="length">Length of the terrain.</param>
        /// <param name="width">Width of the terrain.</param>
        /// <param name="height">Height of the terrain.</param>
        /// <param name="heights">2D array of heights for the terrain.</param>
        /// <param name="id">ID of the terrain entity.</param>
        /// <param name="parent">Parent of the terrain entity.</param>
        /// <param name="position">Position of the terrain entity.</param>
        /// <param name="rotation">Rotation of the terrain entity.</param>
        /// <param name="tag">Tag of the terrain entity.</param>
        /// <param name="onLoaded">Action to perform when loading is complete.</param>
        /// <returns>Coroutine, completes after invocation of the onLoaded action.</returns>
        private System.Collections.IEnumerator LoadTerrainEntity(float length, float width, float height,
            float[,] heights, Guid id, BaseEntity parent,
            Vector3 position, Quaternion rotation, string tag, Action onLoaded)
        {
            TerrainEntity entity = TerrainEntity.Create(length, width, height, heights, id);
            GameObject terrainEntityObject = new GameObject("TerrainEntity-" + id.ToString());
            entities.Add(id, entity);
            entity.SetParent(parent);
            entity.entityTag = tag;
            entity.SetPosition(position, true);
            entity.SetRotation(rotation, true);

            if (onLoaded != null)
            {
                onLoaded.Invoke();
            }

            yield return null;
        }

        /// <summary>
        /// Loads a canvas entity.
        /// </summary>
        /// <param name="id">ID of the canvas entity.</param>
        /// <param name="parent">Parent of the canvas entity.</param>
        /// <param name="position">Position of the canvas entity.</param>
        /// <param name="rotation">Rotation of the canvas entity.</param>
        /// <param name="scale">Scale/size of the canvas entity.</param>
        /// <param name="isSize">Whether or not the scale value is for a size.</param>
        /// <param name="tag">Tag of the canvas entity.</param>
        /// <param name="onLoaded">Action to perform when loading is complete.</param>
        /// <returns>Coroutine, completes after invocation of the onLoaded action.</returns>
        private System.Collections.IEnumerator LoadCanvasEntity(Guid id, BaseEntity parent,
            Vector3 position, Quaternion rotation, Vector3 scale, bool isSize,
            string tag, Action onLoaded)
        {
            GameObject canvasEntityObject = new GameObject("CanvasEntity-" + id.ToString());
            CanvasEntity entity = canvasEntityObject.AddComponent<CanvasEntity>();
            entities.Add(id, entity);
            entity.Initialize(id);
            entity.SetParent(parent);
            entity.entityTag = tag;
            entity.SetPosition(position, true);
            entity.SetRotation(rotation, true);

            if (isSize)
            {
                entity.SetSize(scale);
            }
            else
            {
                entity.SetScale(scale);
            }

            if (onLoaded != null)
            {
                onLoaded.Invoke();
            }

            yield return null;
        }

        /// <summary>
        /// Loads a text entity.
        /// </summary>
        /// <param name="text">Text of the text entity.</param>
        /// <param name="fontSize">Font size of the text entity.</param>
        /// <param name="id">ID of the text entity.</param>
        /// <param name="parent">Parent of the text entity.</param>
        /// <param name="positionPercent">Position of the text entity as a percentage of the canvas.</param>
        /// <param name="sizePercent">Size of the text entity as a percentage of the canvas.</param>
        /// <param name="onLoaded">Action to perform when loading is complete.</param>
        /// <param name="tag">Tag of the text entity.</param>
        /// <returns>Coroutine, completes after invocation of the onLoaded action.</returns>
        private System.Collections.IEnumerator LoadTextEntity(string text, int fontSize,
            Guid id, CanvasEntity parent, Vector2 positionPercent, Vector2 sizePercent,
            string tag, Action onLoaded)
        {
            GameObject textEntityObject = new GameObject("TextEntity-" + id.ToString());
            TextEntity entity = textEntityObject.AddComponent<TextEntity>();
            entities.Add(id, entity);
            entity.Initialize(id, parent);
            entity.entityTag = tag;
            entity.SetPositionPercent(positionPercent, true);
            entity.SetSizePercent(sizePercent, true);
            entity.SetText(text);
            entity.SetFontSize(fontSize);

            if (onLoaded != null)
            {
                onLoaded.Invoke();
            }

            yield return null;
        }

        /// <summary>
        /// Loads a button entity.
        /// </summary>
        /// <param name="id">ID of the button entity.</param>
        /// <param name="parent">Parent of the button entity.</param>
        /// <param name="positionPercent">Position of the button entity as a percentage of the canvas.</param>
        /// <param name="sizePercent">Size of the button entity as a percentage of the canvas.</param>
        /// <param name="tag">Tag of the button entity.</param>
        /// <param name="onLoaded">Action to perform when loading is complete.</param>
        /// <returns>Coroutine, completes after invocation of the onLoaded action.</returns>
        private System.Collections.IEnumerator LoadButtonEntity(Guid id, CanvasEntity parent,
            Vector2 positionPercent, Vector2 sizePercent, Action onClick, string tag, Action onLoaded)
        {
            GameObject buttonEntityObject = new GameObject("ButtonEntity-" + id.ToString());
            ButtonEntity entity = buttonEntityObject.AddComponent<ButtonEntity>();
            entities.Add(id, entity);
            entity.Initialize(id, parent);
            entity.entityTag = tag;
            entity.SetPositionPercent(positionPercent, true);
            entity.SetSizePercent(sizePercent, true);

            if (onLoaded != null)
            {
                onLoaded.Invoke();
            }

            entity.SetOnClick(onClick);

            yield return null;
        }

        /// <summary>
        /// Loads an input entity.
        /// </summary>
        /// <param name="id">ID of the input entity.</param>
        /// <param name="parent">Parent of the input entity.</param>
        /// <param name="positionPercent">Position of the input entity as a percentage of the canvas.</param>
        /// <param name="sizePercent">Size of the input entity as a percentage of the canvas.</param>
        /// <param name="tag">Tag of the input entity.</param>
        /// <param name="onLoaded">Action to perform when loading is complete.</param>
        /// <returns>Coroutine, completes after invocation of the onLoaded action.</returns>
        private System.Collections.IEnumerator LoadInputEntity(Guid id, CanvasEntity parent,
            Vector2 positionPercent, Vector2 sizePercent, string tag, Action onLoaded)
        {
            GameObject inputEntityObject = Instantiate(inputEntityPrefab);
            inputEntityObject.name = "InputEntity-" + id.ToString();
            InputEntity entity = inputEntityObject.AddComponent<InputEntity>();
            entities.Add(id, entity);
            entity.Initialize(id, parent);
            entity.entityTag = tag;
            entity.SetPositionPercent(positionPercent, true);
            entity.SetSizePercent(sizePercent, true);

            if (onLoaded != null)
            {
                onLoaded.Invoke();
            }

            yield return null;
        }

        /// <summary>
        /// Loads a voxel entity.
        /// </summary>
        /// <param name="id">ID of the voxel entity.</param>
        /// <param name="parent">Parent of the voxel entity.</param>
        /// <param name="position">Position of the voxel entity.</param>
        /// <param name="rotation">Rotation of the voxel entity.</param>
        /// <param name="scale">Scale of the voxel entity.</param>
        /// <param name="tag">Tag of the voxel entity.</param>
        /// <param name="onLoaded">Action to perform when loading is complete.</param>
        /// <returns>Coroutine, completes after invocation of the onLoaded action.</returns>
        private System.Collections.IEnumerator LoadVoxelEntity(Guid id, BaseEntity parent,
            Vector3 position, Quaternion rotation, Vector3 scale, string tag, Action onLoaded)
        {
            GameObject voxelEntityObject = new GameObject("VoxelEntity-" + id.ToString());
            VoxelEntity entity = voxelEntityObject.AddComponent<VoxelEntity>();
            entities.Add(id, entity);
            entity.Initialize(id);
            entity.SetParent(parent);
            entity.entityTag = tag;
            entity.SetPosition(position, true);
            entity.SetRotation(rotation, true);
            entity.SetScale(scale);

            if (onLoaded != null)
            {
                yield return new WaitForSeconds(0.01f);
                onLoaded.Invoke();
            }

            yield return null;
        }
        
        /// <summary>
        /// Check if entity with given ID exists.
        /// </summary>
        /// <param name="id">ID of entity to check.</param>
        /// <returns>Whether or not entity exists.</returns>
        public bool Exists(Guid id)
        {
            return entities.ContainsKey(id);
        }
        
        /// <summary>
        /// Find entity with given ID.
        /// </summary>
        /// <param name="id">ID of entity to find.</param>
        /// <returns>Reference to the found entity.</returns>
        public BaseEntity FindEntity(Guid id)
        {
            return entities[id];
        }
        
        /// <summary>
        /// Get all entities.
        /// </summary>
        /// <returns>Array of entities.</returns>
        public BaseEntity[] GetAllEntities()
        {
            BaseEntity[] allEntities = new BaseEntity[entities.Count];
            entities.Values.CopyTo(allEntities, 0);
            return allEntities;
        }

        /// <summary>
        /// Unload all entities.
        /// </summary>
        public void Unload()
        {
            foreach (BaseEntity entity in entities.Values)
            {
                entity.Delete();
            }
            entities.Clear();
        }
    }
}