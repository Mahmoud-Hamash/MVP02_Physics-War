using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    private void Update()
    {
        // transform.LookAt(CameraLoader.xrCamera.transform.position+Vector3.up);
        if(transform.position.y < -0.5)
        {
            Destroy(gameObject);
        }   
    }
}
