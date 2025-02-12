using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using UnityEngine;

public class PalisadeSpawner : MonoBehaviour
{
    [SerializeField] private PalisadeBehaviour _palisadePrefab;
    [SerializeField] private RoomLayout _roomLayout;

    private GameObject _palisadeWall;
    private List<PalisadeBehaviour> _palisades = new List<PalisadeBehaviour>();
    private bool _isWrecked = false;

    private void Start()
    {
        Invoke(nameof(SpawnPalisade),1f);
    }

    private void Update()
    {
        if(_palisades.Count <= 0) return;
        if(_isWrecked) return;

        _isWrecked = true;
        foreach (PalisadeBehaviour palisade in _palisades)
        {
            if (!palisade.IsWrecked())
            {
                _isWrecked = false;
                break;
            }
        }
        if(_isWrecked)
        {
            Debug.Log("Full Palisade is wrecked!");
            GameEventManager.TriggerEvent(GameEventType.PalisadeWrecked);
        }
    }

    private void SpawnPalisade()
    {
        MRUKAnchor frontWall = _roomLayout.GetFrontWall();
        // Destroy(frontWall.GetComponentInChildren<MeshCollider>());

        _palisadeWall = CreatePalisadeWall(frontWall);

        float wallHeight = frontWall.PlaneRect.Value.size.y;
        float wallWidth = frontWall.PlaneRect.Value.size.x;
        float palisadeWidth = _palisadePrefab.GetComponentInChildren<Renderer>().bounds.size.x;

        float palisadeStart = -(wallWidth / 2);

        int palisadeCount = ((int)(wallWidth / palisadeWidth))+1;
        for (int i = 0; i < palisadeCount; i++)
        {
            PalisadeBehaviour palisade = Instantiate(_palisadePrefab, _palisadeWall.transform);
            palisade.gameObject.name = $"Palisade{i}";
            palisade.transform.localPosition = new Vector3(palisadeStart + (i * palisadeWidth), 0, 0);
            if(i == 2) palisade.EnableScoreboard();
            _palisades.Add(palisade);
        }
    }

    private GameObject CreatePalisadeWall(MRUKAnchor frontWall)
    {
        GameObject palisadeWall = new GameObject("PalisadeWall");
        palisadeWall.transform.position = new Vector3(
            frontWall.transform.position.x,
            0,
            frontWall.transform.position.z
        );

        Quaternion rotation = frontWall.transform.rotation;
        Vector3 position = palisadeWall.transform.forward;
        Vector3 rotatedPosition = rotation * position;

        // rotate y by 180 degrees
        rotatedPosition = Quaternion.Euler(0, 180, 0) * rotatedPosition;
        palisadeWall.transform.forward = rotatedPosition;
        return palisadeWall;
    }

    public GameObject GetPalisadeWall()
    {
        return _palisadeWall;
    }

    public List<PalisadeBehaviour> GetPalisades()
    {
        return _palisades;
    }

    public float GetPalisadeWidth()
    {
        return _palisadePrefab.GetComponentInChildren<Renderer>().bounds.size.x;
    }
}
