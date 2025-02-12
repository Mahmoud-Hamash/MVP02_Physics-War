using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _arrowPrefab;
    [SerializeField] private GameObject _handle;
    [SerializeField] private float _angle = 45f;
    [SerializeField] private float _frequency = 2f;
    [SerializeField] private float _force = 1000f;

    private void Start()
    {
        InvokeRepeating(nameof(ShootArrow), _frequency, Random.Range(1f, _frequency));
    }

    private void ShootArrow()
    {
        GameObject arrow = Instantiate(_arrowPrefab);
        arrow.transform.position = _handle.transform.position;
        arrow.transform.LookAt(CameraLoader.xrCamera.transform);
        arrow.transform.Rotate(-_angle, 0, 0);
        arrow.GetComponent<Rigidbody>().AddForce(arrow.transform.forward * _force);
    }
}
