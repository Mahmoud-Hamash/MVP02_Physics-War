using UnityEngine;

public class WoodPlank : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            // Notify the parent Tower to collapse
            Tower tower = GetComponentInParent<Tower>();
            if (tower != null)
            {
                tower.Collapse(collision.contacts[0].point);
            }

            // Destroy the projectile after impact
            Destroy(collision.gameObject);
        }
    }
}