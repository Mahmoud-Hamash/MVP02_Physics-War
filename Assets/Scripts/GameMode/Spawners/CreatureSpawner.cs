using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    [SerializeField] private CreatureBehaviour _creaturePrefab;
    [SerializeField] private PalisadeSpawner _palisadeSpawner;
    [SerializeField] private float _spawnDelayStart;
    [SerializeField] private float _spawnDelayEnd;
    [SerializeField] private float _yStart;
    [SerializeField] private float _zDistance;

    private void Start()
    {
        Invoke(nameof(SpawnCreatures), 2f);
    }

    private void SpawnCreatures()
    {
        StartCoroutine(SpawnCreaturesWithDelay());
    }

    private IEnumerator SpawnCreaturesWithDelay()
    {
        while(true)
        {
            CreatureBehaviour creature = Instantiate(_creaturePrefab);
            creature.transform.forward = -GetPalisadeDirection();

            float startZ = (-creature.transform.forward * _zDistance).z;
            float endZ = (creature.transform.forward * _zDistance).z;

            creature.transform.position = GetRandomPosition(startZ);
            creature.SetEndZ(endZ);

            yield return new WaitForSeconds(Random.Range(_spawnDelayStart, _spawnDelayEnd));
        }
    }
    
    private Vector3 GetRandomPosition(float z)
    {
        float x = Random.Range(GetFirstPalisadeX(), GetLastPalisadeX());
        return new Vector3(x, _yStart, z);
    }

    private Vector3 GetPalisadeDirection()
    {
        List<PalisadeBehaviour> palisades = _palisadeSpawner.GetPalisades();
        return palisades[0].transform.forward;
    }

    private float GetFirstPalisadeX()
    {
        List<PalisadeBehaviour> palisades = _palisadeSpawner.GetPalisades();
        return palisades[0].transform.position.x;
    }

    private float GetLastPalisadeX()
    {
        List<PalisadeBehaviour> palisades = _palisadeSpawner.GetPalisades();
        return palisades[^1].transform.position.x;
    }
}
