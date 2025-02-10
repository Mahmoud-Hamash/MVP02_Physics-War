using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpartanSpawner : MonoBehaviour
{
    [SerializeField] private AudioSource _chargeSound;
    [SerializeField] private PalisadeSpawner _palisadeSpawner;
    [SerializeField] private float _spawnZ;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private GameObject _spartanPrefab;

    private void Start()
    {
        Invoke(nameof(SpawnSpartans), 2f);

        GameEventManager.StartListening(GameEventType.SpartanDied, OnSpartanDied);
    }

    private void SpawnSpartans()
    {
        StartCoroutine(SpawnSpartansWithDelay());
    }

    private IEnumerator SpawnSpartansWithDelay()
    {
        _chargeSound.Play();
        List<PalisadeBehaviour> palisades = _palisadeSpawner.GetPalisades();
        for(int i = 0; i < palisades.Count; i++)
        {
            PalisadeBehaviour palisade = palisades[i];
            SpawnSpartan(palisade, i);

            yield return new WaitForSeconds(_spawnDelay);
        }
    }

    private GameObject SpawnSpartan(PalisadeBehaviour palisade, int col)
    {
        GameObject spartan = Instantiate(_spartanPrefab);
        spartan.gameObject.name = $"Spartan{col}";

        SpartanBehaviour spartanBehaviour = spartan.GetComponentInChildren<SpartanBehaviour>();
        spartanBehaviour.SetColumn(col);

        spartan.transform.rotation = palisade.transform.rotation;
        spartan.transform.position = palisade.transform.position;// + new Vector3(_palisadeSpawner.GetPalisadeWidth()/2,0,0);
        spartan.transform.position += spartan.transform.right * _palisadeSpawner.GetPalisadeWidth() / 2;
        spartan.transform.Rotate(0, 180, 0);
        spartan.transform.position += -spartan.transform.forward * _spawnZ;

        return spartan;
    }

    public void OnSpartanDied(System.Object obj)
    {
        int col = (int) obj;

        SpawnSpartan(_palisadeSpawner.GetPalisades()[col], col);
    }
}
