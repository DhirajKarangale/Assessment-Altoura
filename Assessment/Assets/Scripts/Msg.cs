using UnityEngine;
using System.Collections;

public class Msg : PersistentSingleton<Msg>
{
    [SerializeField] GameObject obj;
    [SerializeField] TMPro.TMP_Text txtMsg;
    private float popDuration = 0.35f;

    private void Start()
    {
        Disable();
    }

    private IEnumerator IEFadeTxt()
    {
        txtMsg.CrossFadeAlpha(1, 0.5f, false);
        yield return new WaitForSecondsRealtime(3);
        txtMsg.CrossFadeAlpha(0, 0.5f, false);
        StartCoroutine(IEPopDown());
    }

    private IEnumerator IEPopUp()
    {
        obj.transform.localScale = Vector3.one * 0.5f;
        Vector3 startScale = obj.transform.localScale;
        float elapsedTime = 0.0f;

        while (elapsedTime < popDuration)
        {
            float t = elapsedTime / popDuration;
            obj.transform.localScale = Vector3.Lerp(startScale, new Vector3(1.3f, 1.3f, 1.3f), t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        startScale = obj.transform.localScale;
        elapsedTime = 0.0f;

        while (elapsedTime < popDuration)
        {
            float t = elapsedTime / popDuration;
            obj.transform.localScale = Vector3.Lerp(startScale, new Vector3(1, 1, 1), t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.localScale = Vector3.one;
    }

    private IEnumerator IEPopDown()
    {
        obj.transform.localScale = Vector3.one;
        Vector3 startScale = obj.transform.localScale;
        float elapsedTime = 0.0f;

        while (elapsedTime < popDuration)
        {
            float t = elapsedTime / popDuration;
            obj.transform.localScale = Vector3.Lerp(startScale, new Vector3(0f, 0f, 0f), t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.localScale = Vector3.zero;
    }


    internal void Disable()
    {
        StopAllCoroutines();
        txtMsg.CrossFadeAlpha(0, 0, false);
        obj.transform.localScale = Vector3.zero;
    }

    internal void DisplayMsg(string msg, Color color, float size = 50f)
    {
        StopAllCoroutines();

        txtMsg.color = color;
        txtMsg.text = msg;
        txtMsg.fontSize = size;

        StartCoroutine(IEPopUp());
        StartCoroutine(IEFadeTxt());
    }
}