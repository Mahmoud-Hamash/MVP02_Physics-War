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
            int projectileID = collision.gameObject.GetInstanceID(); // Get unique instance ID of the projectile

            if (parentTower != null)
            {
                Vector3 impactPoint = collision.contacts[0].point;
                parentTower.RegisterHit(impactPoint, transform, projectileID); // Pass instance ID to TowerMed
            }

            Destroy(collision.gameObject); // Destroy projectile after impact
        }
    }
}