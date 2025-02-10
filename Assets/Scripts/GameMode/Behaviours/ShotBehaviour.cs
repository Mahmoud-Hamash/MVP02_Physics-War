using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.Samples;

public class ShotBehaviour : MonoBehaviour
{
    private RespawnOnDrop _respawnOnDrop;

    private void Start()
    {
        _respawnOnDrop = GetComponent<RespawnOnDrop>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _respawnOnDrop.Respawn();
        }
    }
}
