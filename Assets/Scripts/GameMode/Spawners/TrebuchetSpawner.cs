using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrebuchetSpawner : MonoBehaviour
{

    [SerializeField] private GameObject _trebuchetPrefab;
    [SerializeField] private PalisadeSpawner _palisadeSpawner;
    [SerializeField] private float _zDistance;
    [SerializeField] private float _scale;

    private void Start()
    {
        Invoke(nameof(SpawnTrebuchet), 2f);
    }

    private void SpawnTrebuchet()
    {
        GameObject trebuchet = Instantiate(_trebuchetPrefab);
        GameObject palisadeWall = _palisadeSpawner.GetPalisadeWall();
        trebuchet.transform.localScale = trebuchet.transform.localScale * _scale;
        trebuchet.transform.SetPositionAndRotation(palisadeWall.transform.position, palisadeWall.transform.rotation);
        // trebuchet.transform.Rotate(0, 180, 0);
        trebuchet.transform.position -= trebuchet.transform.forward * _zDistance;
    }
}
