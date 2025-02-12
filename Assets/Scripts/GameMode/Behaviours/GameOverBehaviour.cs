using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _gameOverText;

    private bool _isZooming = false;
    private Vector3 _targetPosition;
    private Quaternion _targetRotation;

    public void SetGameOverText(string text)
    {
        _gameOverText.text = text;
    }

    public void Zoom()
    {
        _isZooming = true;
        transform.position = CameraLoader.xrCamera.transform.position;
        transform.rotation = CameraLoader.xrCamera.transform.rotation * Quaternion.Euler(0, 0, -180);

        _targetRotation =  CameraLoader.xrCamera.transform.rotation;
        _targetPosition = transform.position + (CameraLoader.xrCamera.transform.forward * 0.5f);
        transform.position += CameraLoader.xrCamera.transform.forward * 2f;

        Time.timeScale = 0.0f;
    }

    private void Update()
    {
        if(!_isZooming) return;

        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, 1f * Time.unscaledDeltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, 72f * Time.unscaledDeltaTime);
    }
}
