using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    public static ScreenFlash Instance;

    public Image flashImage;

    private void Awake()
    {
        Instance = this;

        Color c = flashImage.color;
        c.a = 0f;
        flashImage.color = c;
    }

    public void Flash()
    {
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        Color c = flashImage.color;

        c.a = 0.15f;
        flashImage.color = c;

        float t = 0f;

        while (t < 0.1f)
        {
            t += Time.deltaTime;

            c.a = Mathf.Lerp(0.15f, 0f, t / 0.1f);
            flashImage.color = c;

            yield return null;
        }
    }
}
