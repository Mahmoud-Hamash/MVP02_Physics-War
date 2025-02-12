using System.Collections;
using UnityEngine;

public class Banner : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 3f; // Time to fade out
    [SerializeField] private float displayTime = 3f;  // Time before fade starts
    [SerializeField] private AudioSource bannerSound;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowBanner()
    {
        gameObject.SetActive(true);
        canvasGroup.alpha = 1f; // Ensure it's fully visible
        bannerSound.Play();
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(displayTime);

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }
}