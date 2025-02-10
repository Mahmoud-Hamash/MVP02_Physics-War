using UnityEngine;

public class WoodPlankMid : MonoBehaviour
{
    private TowerMed parentTower;

    private void Start()
    {
        // Cache reference to parent TowerMed component
        parentTower = GetComponentInParent<TowerMed>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            if (parentTower != null)
            {
                Vector3 impactPoint = collision.contacts[0].point;
                parentTower.RegisterHit(impactPoint, transform); // Notify parent tower of the hit
            }

            Destroy(collision.gameObject); // Destroy projectile after impact
        }
    }
}
