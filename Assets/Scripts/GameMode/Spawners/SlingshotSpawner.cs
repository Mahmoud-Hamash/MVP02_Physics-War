using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using Meta.WitAi.Utilities;
using Meta.XR.MRUtilityKit;
using UnityEngine;

public class SlingshotSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _slingshotPrefab;
    [SerializeField] private GameObject _shotPrefab;
    [SerializeField] private RoomLayout _roomLayout;

    private void Start()
    {
        Invoke(nameof(SpawnSlingshot), 1f);
    }

    private void SpawnSlingshot()
    {
        MRUKAnchor table = _roomLayout.GetTable();
        Vector3 tableExtents = table.VolumeBounds.Value.extents;
        Vector3 slingshotExtents = _slingshotPrefab.GetComponentInChildren<MeshRenderer>().bounds.extents;
        Vector3 shotExtents = _shotPrefab.GetComponentInChildren<MeshRenderer>().bounds.extents;

        Vector3 myPosition = _roomLayout.GetTrackerAnchor().position;
        Vector3 myRelativePosition = table.transform.InverseTransformPoint(myPosition);

        float x = Mathf.Min(Mathf.Max(myRelativePosition.x, -tableExtents.x+slingshotExtents.x), tableExtents.x-slingshotExtents.x);
        float y = Mathf.Min(Mathf.Max(myRelativePosition.y, -tableExtents.y+slingshotExtents.y), tableExtents.y-slingshotExtents.y);

        GameObject slingshot = Instantiate(_slingshotPrefab, table.transform);
        slingshot.transform.localPosition = new Vector3(x, y, 0);
        slingshot.SetActive(true);

        float[] xOptions = new float[] {
            slingshot.transform.localPosition.x - slingshotExtents.x - shotExtents.x,
            slingshot.transform.localPosition.x + slingshotExtents.x + shotExtents.x,
        };
        float shotX;
        foreach(float xOption in xOptions)
        {
            if(xOption > -tableExtents.x && xOption < tableExtents.x)
            {
                shotX = xOption;
                GameObject shot = Instantiate(_shotPrefab, table.transform);
                shot.transform.localPosition = new Vector3(shotX, slingshot.transform.localPosition.y, 0);
                shot.SetActive(true);
                break;
            }
        }
    }
}
