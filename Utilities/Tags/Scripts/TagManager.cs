// Copyright (c) 2019-2023 Five Squared Interactive. All rights reserved.

using FiveSQD.WebVerse.Utilities;

namespace FiveSQD.WebVerse.WorldEngine.Tags
{
    /// <summary>
    /// Class for the tag manager.
    /// </summary>
    public class TagManager : BaseManager
    {
        /// <summary>
        /// Mesh collider tag.
        /// </summary>
        public static readonly string meshColliderTag = "MeshColliders";

        /// <summary>
        /// Physics collider tag.
        /// </summary>
        public static readonly string physicsColliderTag = "PhysicsColliders";
    }
}