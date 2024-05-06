using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Loading : PersistentSingleton<Loading>
{
    [SerializeField] GameObject obj;
    [SerializeField] TMPro.TMP_Text txtProgress;
    [SerializeField] UnityEngine.UI.Slider slider;


    private IEnumerator IELoadLevel(int idx, int extraTime)
    {
        slider.value = 0;
        txtProgress.text = "0%";
        txtProgress.gameObject.SetActive(true);
        slider.gameObject.SetActive(true);
        obj.SetActive(true);

        yield return new WaitForEndOfFrame();

        SceneManager.LoadScene(idx);
        slider.value = 0.5f;
        txtProgress.text = "50%";

        float elapsedTime = 0f;
        while (elapsedTime < extraTime)
        {
            elapsedTime += Time.deltaTime;
            slider.value = Mathf.Lerp(0.5f, 1, elapsedTime / extraTime);
            txtProgress.text = $"{(int)(slider.value * 100)}%";
            yield return null;
        }

        Disable();
    }

    private IEnumerator IEDisable()
    {
        slider.value = 1;
        txtProgress.text = $"{(int)(slider.value * 100)}%";
        yield return new WaitForSecondsRealtime(0.5f);
        obj.SetActive(false);
    }


    internal void Active()
    {
        txtProgress.gameObject.SetActive(false);
        slider.gameObject.SetActive(false);
        obj.SetActive(true);
    }

    internal void Disable()
    {
        StopAllCoroutines();
        StartCoroutine(IEDisable());
    }

    internal void LoadLevel(int scene, int extraTime)
    {
        StartCoroutine(IELoadLevel(scene, extraTime));
    }
}