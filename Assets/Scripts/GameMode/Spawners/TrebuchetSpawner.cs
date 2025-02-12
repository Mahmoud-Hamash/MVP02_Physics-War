using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
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
        GameObject palisadeWall = _palisadeSpawner.GetPalisadeWall();

        GameObject trebuchet = Instantiate(_trebuchetPrefab);
        Bounds? bounds = Utilities.GetPrefabBounds(trebuchet);
        const float clearanceDistance = 0.1f;
        Bounds adjustedBounds = new();

        var min = bounds.Value.min-(clearanceDistance*new Vector3(1, 0, 1));
        var max = bounds.Value.max+(clearanceDistance*new Vector3(1, 0, 1));
        adjustedBounds.SetMinMax(min, max);

        Quaternion spawnRotation = palisadeWall.transform.rotation;
        trebuchet.transform.rotation = palisadeWall.transform.rotation;

        Vector3 spawnPosition = palisadeWall.transform.position;
        int layerMask = ~LayerMask.GetMask("Trebuchet");
        for(int attempts = 0; attempts < 100; attempts++)
        {
            spawnPosition = palisadeWall.transform.position-(trebuchet.transform.forward * attempts);

            Collider[] colliders = Physics.OverlapBox(spawnPosition + spawnRotation * adjustedBounds.center, adjustedBounds.extents, spawnRotation, layerMask, QueryTriggerInteraction.Ignore); 
            if(colliders.Length <= 0) break;
        } 

        trebuchet.transform.localScale = trebuchet.transform.localScale * _scale;
        trebuchet.transform.position = spawnPosition;
    }
}
