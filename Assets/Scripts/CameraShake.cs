using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
/*HOW TO USE
CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();
if (cameraShake != null)
{
    StartCoroutine(cameraShake.Shake(0.5f, 0.2f)); // Adjust duration and magnitude as needed
}*/
