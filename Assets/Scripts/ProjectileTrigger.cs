using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileTrigger : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    
    private bool _projectileIn = false;
    private GameObject _currentProjectile;
    
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_projectileIn)
        {
            // Matrix4x4 matrix = Matrix4x4.TRS(transform.position, transform.rotation, _hoveredMeshFilter.transform.lossyScale);
            // Graphics.RenderMesh(_renderParams, _hoveredMeshFilter.mesh, 0, matrix);
            if (_currentProjectile.transform.parent == null)    // released
            {
                Destroy(_currentProjectile);
                projectile.SetActive(true);
                // Signal 
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            _projectileIn = true;
            _currentProjectile = other.gameObject;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            _projectileIn = false;
            _currentProjectile = null;
        }
    }
}
