using Meta.XR.MRUtilityKit;
using UnityEngine;

public class FloorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _floorPrefab;
    [SerializeField] private GameObject _treePrefab;
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

        CreateTrees(floor);

        return floor;
    }

    private void CreateTrees(GameObject floor)
    {
        float floorDepth = floor.GetComponent<MeshRenderer>().bounds.size.z;
        float floorWidth = floor.GetComponent<MeshRenderer>().bounds.size.x;
        float treeWidth = _treePrefab.GetComponentInChildren<Renderer>().bounds.size.x;

        float treeStart = floor.transform.position.x-(floorWidth / 2);

        int treeCount = ((int)(floorWidth / treeWidth)) + 1;
        Debug.Log($"Tree count: {treeCount}");
        for (int i = 0; i < treeCount; i++)
        {
            GameObject tree = Instantiate(_treePrefab);
            tree.gameObject.name = $"Tree{i}";
            tree.transform.position = new Vector3(treeStart + (i * treeWidth), 0, -floorDepth);
        }
    }

}
