using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Project2 : MonoBehaviour
{
    [SerializeField] Vector3 camPos1, camPos2;
    [SerializeField] Quaternion camRot1, camRot2;
    [SerializeField] float fadeDuration;
    [SerializeField] Transform cam;
    [SerializeField] TMP_Text txtTitle;
    [SerializeField] GameObject objTxt;
    [SerializeField] GameObject objImg;
    [SerializeField] Image imaFade;

    private int interaction;


    private void Start()
    {
        Load();
    }


    private IEnumerator IEFade(bool fadeIn)
    {
        float elapsedTime = 0f;
        Color startColor = imaFade.color;
        Color endColor = fadeIn ? new Color(startColor.r, startColor.g, startColor.b, 1) : new Color(startColor.r, startColor.g, startColor.b, 0);

        while (elapsedTime < fadeDuration)
        {
            Color currentColor = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            imaFade.color = currentColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        imaFade.color = endColor;
    }

    private IEnumerator IEFade()
    {
        yield return IEFade(true);
        yield return new WaitForSeconds(fadeDuration + 0.1f);
        yield return IEFade(false);
    }

    private IEnumerator Interaction1()
    {
        if (cam.position != camPos1) StartCoroutine(IEFade());
        else
        {
            txtTitle.text = "Interaction 1";
            objTxt.SetActive(true);
            objImg.SetActive(false);
        }

        yield return new WaitForSecondsRealtime(0.65f);

        cam.position = camPos1;
        cam.rotation = camRot1;

        txtTitle.text = "Interaction 1";
        objTxt.SetActive(true);
        objImg.SetActive(false);
    }

    private IEnumerator Interaction2()
    {
        if (cam.position != camPos1) StartCoroutine(IEFade());
        else
        {
            txtTitle.text = "Interaction 2";
            objTxt.SetActive(true);
            objImg.SetActive(true);
        }

        yield return new WaitForSecondsRealtime(0.65f);

        cam.position = camPos1;
        cam.rotation = camRot1;

        txtTitle.text = "Interaction 2";
        objTxt.SetActive(true);
        objImg.SetActive(true);
    }

    private IEnumerator Interaction3()
    {
        if (cam.position != camPos2) StartCoroutine(IEFade());
        else
        {
            txtTitle.text = "Interaction 3";
            objTxt.SetActive(false);
            objImg.SetActive(false);
        }

        yield return new WaitForSecondsRealtime(0.65f);

        cam.position = camPos2;
        cam.rotation = camRot2;

        txtTitle.text = "Interaction 3";
        objTxt.SetActive(false);
        objImg.SetActive(false);

    }


    private void OnCharacterLoaded(GameObject gameObject)
    {
        GameObject character = Instantiate(gameObject);
        character.transform.SetParent(transform);
        character.transform.localScale = Vector3.one;
        character.transform.localPosition = new Vector3(37, 0.36f, -2.6f);
        character.transform.rotation = Quaternion.Euler(0, -115, 0);
    }

    private void OnEnviromrntLoaded(GameObject gameObject)
    {
        GameObject enviroment = Instantiate(gameObject);
        enviroment.transform.SetParent(transform);
        enviroment.transform.localScale = Vector3.one;
        enviroment.transform.localPosition = Vector3.zero;
        enviroment.transform.rotation = Quaternion.Euler(-90, 0, 0);

        Loading.instance.Disable();
    }


    private void Load()
    {
        AddressablesManager.instance.Load<GameObject>("character", OnCharacterLoaded);
        AddressablesManager.instance.Load<GameObject>("enviromrnt", OnEnviromrntLoaded);
    }


    public void ButtonNavigate(int val)
    {
        interaction = (interaction + val + 3) % 3;

        switch (interaction)
        {
            case 0:
                StartCoroutine(Interaction1());
                break;
            case 1:
                StartCoroutine(Interaction2());
                break;
            case 2:
                StartCoroutine(Interaction3());
                break;
        }
    }

    public void ButtonExit()
    {
        Loading.instance.LoadLevel(1, 1);
    }
}