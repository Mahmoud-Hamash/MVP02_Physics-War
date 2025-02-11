using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBehaviour : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    float _endZ = 0;

    public void SetEndZ(float z)
    {
        _endZ = z;
    }

    private void Update()
    {
        float distance = Mathf.Abs(transform.position.z - _endZ);
        if(distance > 0.1f)
        {
            transform.position += transform.forward * _speed * Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
