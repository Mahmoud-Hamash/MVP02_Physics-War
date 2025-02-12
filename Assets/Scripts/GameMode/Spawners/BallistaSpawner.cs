using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using UnityEngine;

public class BallistaSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _ballistaPrefab;
    [SerializeField] private GameObject _arrowPrefab;
    [SerializeField] private PalisadeSpawner _palisadeSpawner;
    [SerializeField] private float _zDistance;
    [SerializeField] private float _zStart;
    [SerializeField] private float _scale;
    [SerializeField] private float _xDistance;
    [SerializeField] private int _count;

    private List<GameObject> _ballistas = new List<GameObject>();
    private void Start()
    {
        Invoke(nameof(SpawnBallista), 2f);
    }

    private void SpawnBallista()
    {
        Bounds bounds = Utilities.GetPrefabBounds(_ballistaPrefab).Value;

        for(int i = 0; i < _count; i++)
        {
            GameObject ballista = Instantiate(_ballistaPrefab);
            GameObject palisadeWall = _palisadeSpawner.GetPalisadeWall();
            ballista.transform.localScale = ballista.transform.localScale * _scale;
            ballista.transform.SetPositionAndRotation(palisadeWall.transform.position, palisadeWall.transform.rotation);
            ballista.transform.Rotate(0, 180, 0);
            ballista.transform.position += -ballista.transform.forward * _zStart;

            float leftOrRight = Random.Range(0,2) == 0 ? -1 : 1;
            float frontOrBack = Random.Range(0,2) == 0 ? -1 : 1;

            bool spawned = false;
            Vector3 originalPosition = ballista.transform.position;
            for(int attempts = 0; attempts < 3; attempts++)
            {
                ballista.transform.position += ballista.transform.right * (2f+Random.Range(0,_xDistance)) * leftOrRight;
                ballista.transform.position += ballista.transform.forward * Random.Range(0,_zDistance) * frontOrBack;
                Collider[] colliders = Physics.OverlapBox(ballista.transform.position + ballista.transform.rotation * bounds.center, bounds.extents, ballista.transform.rotation, LayerMask.GetMask("Ballista"), QueryTriggerInteraction.Ignore); 
                if(colliders.Length <= 0)
                {
                    spawned = true;
                    break;
                }
                ballista.transform.position = originalPosition;
            }

            if(!spawned)
            {
                Destroy(ballista);
                continue;
            }

            ballista.transform.LookAt(CameraLoader.xrCamera.transform);
        }
    }
}
