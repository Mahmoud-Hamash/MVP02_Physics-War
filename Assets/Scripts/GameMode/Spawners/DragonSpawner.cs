using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSpawner : MonoBehaviour
{
    [SerializeField] private DragonBehaviour _dragonPrefab;
    [SerializeField] private PalisadeSpawner _palisadeSpawner;
    [SerializeField] private float _startSpeed = 1f;
    [SerializeField] private float _chargeSpeed = 5f;
    [SerializeField] private float _spawnY = 10f;
    [SerializeField] private float _spawnZ = 10f;

    private void Start()
    {
        Invoke(nameof(SpawnDragon), 2f);
    }

    private void SpawnDragon()
    {
        DragonBehaviour dragon = Instantiate(_dragonPrefab);
        dragon.SetSpeeds(_startSpeed, _chargeSpeed);

        GameObject palisadeWall = _palisadeSpawner.GetPalisadeWall();
        dragon.transform.SetPositionAndRotation(palisadeWall.transform.position + new Vector3(0,_spawnY,0), palisadeWall.transform.rotation);
        dragon.transform.Rotate(0, 180, 0);
        dragon.transform.position += -dragon.transform.forward * _spawnZ;
    }
}
