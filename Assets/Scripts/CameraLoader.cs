using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLoader : MonoBehaviour
{
    [SerializeField] public Camera _xrCamera;

    private static CameraLoader _cameraLoader;
    private static CameraLoader _cameraLoaderInstance
    {
        get
        {
            if(_cameraLoader) return _cameraLoader;

            _cameraLoader = FindFirstObjectByType<CameraLoader>();
            if (!_cameraLoader)
                Debug.LogError("There needs to be one active CameraLoader script on a GameObject in your scene.");
            return _cameraLoader;
        }
    }

    public static Camera xrCamera => _cameraLoaderInstance._xrCamera;
}
