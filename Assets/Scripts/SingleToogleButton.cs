using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleToogleButton : MonoBehaviour
{
    public Renderer _renderer;
    public WeaponType optionType; 
    public GameObject model3D;
    
    public void ChangeSpeed3DModel(float newSpeed)
    {
        model3D.GetComponent<RotateObject>().rotationSpeed = newSpeed;
    }

}

public enum WeaponType
{
    TREBUCHET,
    SLINGSHOT
}
