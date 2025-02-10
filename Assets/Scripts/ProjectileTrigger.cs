using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.HandGrab;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileTrigger : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject sphereMesh;
    
    private bool _projectileIn = false;
    private GameObject _currentProjectile;

    // Update is called once per frame
    void Update()
    {
        if (_projectileIn && projectile != null)
        {
            if (_currentProjectile.GetComponentInChildren<HandGrabInteractable>().SelectingInteractors.Count == 0)    // released
            {
                Destroy(_currentProjectile);
                sphereMesh.SetActive(false);
                projectile.SetActive(true);
                _projectileIn = false;
                _currentProjectile = null;
            }
            else
            {
                sphereMesh.SetActive(true);
            }
        }
        else
        {
            sphereMesh.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the projectile is from outside
        if (other.CompareTag("Loadable"))
        {
            _projectileIn = true;
            _currentProjectile = other.gameObject;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Loadable"))
        {
            _projectileIn = false;
            _currentProjectile = null;
        }
    }
}
