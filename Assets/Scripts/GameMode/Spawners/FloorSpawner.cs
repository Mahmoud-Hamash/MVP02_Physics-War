using Meta.XR.MRUtilityKit;
using UnityEngine;

public class FloorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _floorPrefab;
    [SerializeField] private PalisadeSpawner _palisadeSpawner;
    [SerializeField] private RoomLayout _roomLayout;
    [SerializeField] private float _zlength = 30f;


    private void Start()
    {
        Invoke(nameof(SpawnFloor), 0.5f);
    }

    private void SpawnFloor()
    {
        MRUKAnchor frontWall = _roomLayout.GetFrontWall();
        CreateFloor(frontWall);
    }

    private GameObject CreateFloor(MRUKAnchor frontWall)
    {
        GameObject floor = Instantiate(_floorPrefab, frontWall.transform, true);

        Vector3 parentSize = frontWall.PlaneRect.Value.size;
        Vector3 prefabSize = floor.GetComponentInChildren<Renderer>().bounds.size;
        Vector3 scale = new Vector3(
            (parentSize.x+10f) / prefabSize.x,
            prefabSize.y,
            prefabSize.z * _zlength
        );
        floor.transform.localScale = scale;

        MeshRenderer meshRenderer = floor.GetComponent<MeshRenderer>();
        float floorDepth = meshRenderer.bounds.size.z;
        float wallHeight = frontWall.PlaneRect.Value.size.y;
        floor.transform.localPosition = new Vector3(0, -wallHeight/2, -floorDepth/2);

        Quaternion rotation = frontWall.transform.rotation;
        Vector3 position = floor.transform.forward;
        Vector3 rotatedPosition = rotation * position;

        // rotate y by 180 degrees
        rotatedPosition = Quaternion.Euler(0, 180, 0) * rotatedPosition;
        floor.transform.forward = rotatedPosition;

        return floor;
    }

}
