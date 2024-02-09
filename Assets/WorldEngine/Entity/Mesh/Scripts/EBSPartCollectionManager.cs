// Copyright (c) 2019-2024 Five Squared Interactive. All rights reserved.

using System.Collections.Generic;
using UnityEngine;
#if USE_EBS
using EasyBuildSystem.Features.Runtime.Bases;
using EasyBuildSystem.Features.Runtime.Buildings.Part;
using EasyBuildSystem.Features.Runtime.Buildings.Manager;
using EasyBuildSystem.Features.Runtime.Buildings.Placer;
using EasyBuildSystem.Features.Runtime.Buildings.Socket;
using EasyBuildSystem.Features.Runtime.Buildings.Part.Conditions;
#endif

namespace FiveSQD.WebVerse.WorldEngine.Entity.Placement
{
    /// <summary>
    /// WIP
    /// </summary>
    public class EBSPartCollectionManager : MonoBehaviour
    {
#if USE_EBS
        void Start()
        {
            InitializePieceCollection();
        }

        public static void AddToBuildManager(BuildingPart pieceBehaviour)
        {
            BuildingManager.Instance.BuildingPartReferences.Add(pieceBehaviour);
        }

        public static void RemoveFromBuildManager(BuildingPart pieceBehaviour)
        {
            BuildingManager.Instance.BuildingPartReferences.Remove(pieceBehaviour);
        }

        public static BuildingPart AddStaticInstance(BaseEntity entity)
        {
            BuildingPart pieceBehaviour = entity.gameObject.AddComponent<BuildingPart>();

            pieceBehaviour.ChangeState(BuildingPart.StateType.PLACED);

            AddToBuildManager(pieceBehaviour);
            return pieceBehaviour;
        }

        public static void RemoveStaticInstance(BuildingPart pieceBehaviour)
        {
            RemoveFromBuildManager(pieceBehaviour);

            Destroy(pieceBehaviour);
        }

        public static BuildingPart StartPlacing(BaseEntity entity)
        {
            BuildingPart pieceBehaviour = entity.gameObject.AddComponent<BuildingPart>();
            /*pieceBehaviour.bouMeshBounds = entity.gameObject.GetChildsBounds();
            pieceBehaviour.Sockets = SetUpSockets(entity.gameObject, entity.sockets, pieceBehaviour);
            pieceBehaviour.PreviewDisableObjects = new GameObject[0];
            pieceBehaviour.PreviewDisableBehaviours = new MonoBehaviour[0];
            pieceBehaviour.PreviewDisableColliders = new Collider[0];

            BuildingPlacer.Instance.SelectBuildingPart(pieceBehaviour);
            BuildingPlacer.Instance.ChangeBuildMode(BuildingPlacer.BuildMode.PLACE);*/

            return pieceBehaviour;
        }

        public static void StopPlacing(BuildingPart partBehaviour)
        {
            if (partBehaviour.State == BuildingPart.StateType.PLACED)
            {
                return;
            }

            if (!BuildingPlacer.Instance.CanPlacing)
            {
                return;
            }

            /*BuildingPart previewPart = BuilderBehaviour.Instance.CurrentPreview;
            if (previewPart)
            {
                partBehaviour.transform.position = previewPart.transform.position;
                partBehaviour.transform.rotation = previewPart.transform.rotation;
                Destroy(previewPart.gameObject);
            }
            partBehaviour.ChangeState(StateType.Placed);
            BuilderBehaviour.Instance.CurrentMode = EasyBuildSystem.Features.Scripts.Core.Base.Builder.Enums.BuildMode.None;
            if (BuilderBehaviour.Instance.CurrentEditionPreview) Destroy(BuilderBehaviour.Instance.CurrentEditionPreview.gameObject);
            if (BuilderBehaviour.Instance.CurrentPreview) Destroy(BuilderBehaviour.Instance.CurrentPreview.gameObject);
            RemoveSocketPartOffsets(partBehaviour);*/
        }

        public static void UpdatePartBehaviourSocketOffsets(BuildingPart pb)
        {
            RemovePartBehaviourSocketOffsets(pb);
            AddPartBehaviourSocketOffsets(pb);
        }

        private void InitializePieceCollection()
        {
            //BuildingManager.Instance.Pieces = new List<BuildingPart>();
        }

        /*private static BuildingSocket[] SetUpSockets(GameObject prefab, List<PlacementSocket> sockets, BuildingPart pb)
        {
            if (sockets == null)
            {
                return new BuildingSocket[0];
            }

            List<BuildingSocket> socks = new List<BuildingSocket>();

            foreach (PlacementSocket socket in sockets)
            {
                socks.Add(SetUpSocket(prefab, socket.position, socket.rotation, pb));
            }

            return socks.ToArray();
        }*/

        private static BuildingSocket SetUpSocket(GameObject part, Vector3 pos, Quaternion rot, BuildingPart pb)
        {
            GameObject sockObj = new GameObject("Socket");
            sockObj.transform.SetParent(part.transform);
            sockObj.transform.localPosition = pos; // TODO: meters.
            sockObj.transform.localRotation = rot;
            BuildingSocket sock = sockObj.AddComponent<BuildingSocket>();

            return sock;
        }

        private static void AddPartBehaviourSocketOffsets(BuildingPart pb)
        {
            foreach (Transform t in pb.transform)
            {
                BuildingSocket sb = t.GetComponent<BuildingSocket>();
                if (sb)
                {
                    AddSocketPartOffsets(sb, pb);
                }
            }
        }

        private static void RemovePartBehaviourSocketOffsets(BuildingPart pb)
        {
            foreach (Transform t in pb.transform)
            {
                BuildingSocket sb = t.GetComponent<BuildingSocket>();
                if (sb)
                {
                    RemoveSocketPartOffsets(pb);
                }
            }
        }

        private static void AddSocketPartOffsets(BuildingSocket sockToAddFor, BuildingPart pb)
        {
            foreach (BaseEntity entity in WorldEngine.ActiveWorld.entityManager.GetAllEntities())
            {
                foreach (BuildingSocket sb in entity.GetComponentsInChildren<BuildingSocket>())
                {
                    /*if (!sb.PartOffsets.Exists(x => x.Piece == pb))
                    {
                        // Socket behaviour doesn't have offset for this part. Create it.
                        AddSocketPartOffsetsToSocket(sb, pb);
                    }*/
                }
            }
        }

        private static void RemoveSocketPartOffsets(BuildingPart pb)
        {
            foreach (BaseEntity entity in WorldEngine.ActiveWorld.entityManager.GetAllEntities())
            {
                foreach (BuildingSocket sb in entity.GetComponentsInChildren<BuildingSocket>())
                {
                    //sb.PartOffsets.Clear();
                }
            }
        }

        private static void AddSocketPartOffsetsToSocket(BuildingSocket sbToAddTo, BuildingPart pbToAdd)
        {
            BuildingSocket sbToAdd = null;
            float minDiff = float.MaxValue;
            Quaternion adjoiningRot = Quaternion.identity;
            foreach (Transform t in pbToAdd.transform)
            {
                BuildingSocket sb = t.GetComponent<BuildingSocket>();
                if (sb)
                {
                    foreach (Quaternion rot in GetAdjoiningRotations(sb.transform.rotation))
                    {
                        float diff = Quaternion.Angle(sbToAddTo.transform.rotation, rot);
                        if (diff < minDiff)
                        {
                            minDiff = diff;
                            sbToAdd = sb;
                            adjoiningRot = rot;
                        }
                    }
                }
            }

            /*EasyBuildSystem.Features.Scripts.Core.Base.Socket.Data.Offset poToAdd
                = new EasyBuildSystem.Features.Scripts.Core.Base.Socket.Data.Offset(pbToAdd);
            if (sbToAdd)
            {
                Vector3 inverseRot = new Vector3(-1 * sbToAddTo.transform.localEulerAngles.x,
                    -1 * sbToAddTo.transform.localEulerAngles.y, -1 * sbToAddTo.transform.localEulerAngles.z);
                poToAdd.Position = Quaternion.Euler(inverseRot) * (-1 * sbToAdd.transform.localPosition);
                poToAdd.Rotation = inverseRot;
            }
            sbToAddTo.PartOffsets.Add(poToAdd);*/
        }

        private static void RemoveSocketPartOffsetsFromSocket(BuildingSocket sbToRemoveFrom, BuildingPart pbToRemove)
        {
            //sbToRemoveFrom.PartOffsets.Remove(new EasyBuildSystem.Features.Scripts.Core.Base.Socket.Data.Offset(pbToRemove));
        }

        // TODO: Create separate socket class.
        private static Quaternion[] GetAdjoiningRotations(Quaternion rotation)
        {
            Quaternion[] returnVal = new Quaternion[2];

            GameObject temp = new GameObject();
            temp.transform.rotation = rotation;
            temp.transform.Rotate(0, 180, 0);
            returnVal[0] = temp.transform.rotation;
            temp.transform.rotation = rotation;
            temp.transform.Rotate(0, 0, 180f);
            returnVal[1] = temp.transform.rotation;
            Destroy(temp);

            return returnVal;
        }
#endif
    }
}