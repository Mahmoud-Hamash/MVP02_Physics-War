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

    void Update()
    {
        if (transform.position.y < -5)  // fell down from the scene
        {
            Debug.Log("FALLING");
            if (massCategory == MassCategory.Heavy)
            {
                Debug.Log("HEAVY");
                var teacher = FindFirstObjectByType<Teacher>();
                if (teacher != null)
                {
                    Debug.Log("TEACHER FOUND");
                    if (teacher.GetCurrentEvent() == 3)
                    {
                        teacher.TriggerEvent(3);
                        Debug.Log("TEACHER TRIGGERED EVENT");
                    }
                }
            }
            Destroy(gameObject);
        }
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
