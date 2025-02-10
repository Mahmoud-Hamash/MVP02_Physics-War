using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;
using System.Security.Cryptography.X509Certificates;
using TMPro;

public class RoomLayout : MonoBehaviour
{
    [SerializeField] private GameObject _wallEffectPrefab;
    [SerializeField] private Transform _trackerAnchor;
    
    private MRUKAnchor _table;
    private List<MRUKAnchor> _frontWalls = new List<MRUKAnchor>();

    void Start()
    {
        MRUK.Instance.RegisterSceneLoadedCallback(() => StartSpawn(MRUK.Instance.GetCurrentRoom()));
    }

    private void StartSpawn(MRUKRoom room = null)
    {
        if(room.TryGetClosestSurfacePosition(_trackerAnchor.position, out Vector3 position, out MRUKAnchor closestAnchor, LabelFilter.Included(MRUKAnchor.SceneLabels.TABLE|MRUKAnchor.SceneLabels.COUCH|MRUKAnchor.SceneLabels.BED)) < Mathf.Infinity)
        {
            _table = closestAnchor;
        }

        foreach(MRUKAnchor wall in room.WallAnchors)
        {
            if(Vector3.Dot(wall.transform.forward, _trackerAnchor.forward) > 0.5f)
            {
                _frontWalls.Add(wall);
                continue;
            }

            GameObject wallEffect = Instantiate(_wallEffectPrefab, wall.transform, true);

            Vector3 parentSize = wall.PlaneRect.Value.size;
            Vector3 prefabSize = wallEffect.GetComponentInChildren<Renderer>().bounds.size;
            Vector3 scale = new Vector3(
                parentSize.x / prefabSize.x,
                parentSize.y / prefabSize.y,
                parentSize.z / prefabSize.z
            );
              
            wallEffect.transform.localScale = scale;
            wallEffect.transform.localPosition = new Vector3(0, 0, 0.15f);
            wallEffect.transform.localRotation = Quaternion.identity;
        }

        ApplyLayer(GetLargestFrontWall().gameObject, "Virtual");
    }

    private MRUKAnchor GetLargestFrontWall()
    {
        MRUKAnchor largestWall = null;
        float largestArea = 0;
        foreach(MRUKAnchor wall in _frontWalls)
        {
            float area = wall.PlaneRect.Value.size.x * wall.PlaneRect.Value.size.y;
            if(area > largestArea)
            {
                largestArea = area;
                largestWall = wall;
            }
        }
        return largestWall;
    }

    private void ApplyLayer(GameObject wall, string layerName)
    {
        int layer = LayerMask.NameToLayer(layerName);
        wall.layer = layer;

        foreach(Transform child in wall.transform)
        {
            ApplyLayer(child.gameObject, layerName);
        }
    }

    public MRUKAnchor GetFrontWall()
    {
        return GetLargestFrontWall();
    }

    public MRUKAnchor GetTable()
    {
        return _table;
    }

    public Transform GetTrackerAnchor()
    {
        return _trackerAnchor;
    }
}

