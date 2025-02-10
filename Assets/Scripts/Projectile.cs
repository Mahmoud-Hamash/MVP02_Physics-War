using System.Collections;
using System.Collections.Generic;
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
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateMass();
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
}
