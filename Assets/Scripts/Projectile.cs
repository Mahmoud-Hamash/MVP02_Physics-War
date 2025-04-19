using System;
using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using TMPro;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public enum MassCategory
    {
        Light,
        Average,
        Heavy
    }
    
    [SerializeField] private MassCategory massCategory;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private TextMeshProUGUI textMass;
    
    private ParticleSystem _fallParticles;
    private AudioSource _fallSound;
    
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateMass();
        var dirt = FindFirstObjectByType<DirtPS>().gameObject;
        _fallParticles = dirt.GetComponent<ParticleSystem>();
        _fallSound = dirt.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (gameObject.CompareTag("Projectile") && transform.position.y < 0) Fall();
    }

    public void SetMassCategory(MassCategory newMassCategory)
    {
        massCategory = newMassCategory;
        UpdateMass();
    }

    public MassCategory GetMassCategory()
    {
        return massCategory;
    }

    private void UpdateMass()
    {
        switch (massCategory)
        {
            case MassCategory.Light:
                rb.mass = 0.5f;
                break;
            case MassCategory.Average:
                rb.mass = 1f;
                break;
            case MassCategory.Heavy:
                rb.mass = 1.5f;
                break;
        }
        textMass.text = $"{rb.mass}\nkg";
    }

    private void OnCollisionEnter(Collision other)
    {
        MRUKAnchor anchor = other.gameObject.GetComponentInParent<MRUKAnchor>();

        if (gameObject.CompareTag("Projectile") && anchor != null && anchor.Label == MRUKAnchor.SceneLabels.FLOOR)
        {
            Fall();
        } 
    }

    private void Fall()
    {
        if (massCategory == MassCategory.Heavy)
        {
            var teacher = FindFirstObjectByType<Teacher>();
            if (teacher != null)
            {
                if (teacher.GetCurrentEvent() == 3)
                {
                    teacher.TriggerEvent(3);
                }
            }
        }
        gameObject.SetActive(false);
        
        if (_fallParticles != null)
        {
            _fallParticles.transform.position = transform.position;
            _fallParticles.Stop();  // Ensure it stops first
            _fallParticles.Clear();
            _fallParticles.Play();
        }

        if (_fallSound != null)
        {
            _fallSound.Play();
        }
        
        Destroy(gameObject);
    }
}
