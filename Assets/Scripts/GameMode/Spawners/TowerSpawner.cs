using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private int _towerCount;
    [SerializeField] private GameObject _towerPrefab;
    [SerializeField] private PalisadeSpawner _palisadeSpawner;
    [SerializeField] private float _zDistance;
    [SerializeField] private float _scale;

    private void Start()
    {
        Invoke(nameof(SpawnTower), 2f);
    }

    private void SpawnTower()
    {
        GameObject tower = Instantiate(_towerPrefab);
        GameObject palisadeWall = _palisadeSpawner.GetPalisadeWall();
        tower.transform.localScale = tower.transform.localScale * _scale;
        tower.transform.SetPositionAndRotation(palisadeWall.transform.position + new Vector3(0,0,0), palisadeWall.transform.rotation);
        tower.transform.Rotate(0, 180, 0);
        tower.transform.position += -tower.transform.forward * _zDistance;
    }
}
