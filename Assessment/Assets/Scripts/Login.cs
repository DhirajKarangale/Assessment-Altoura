using TMPro;
using UnityEngine;

public class Login : MonoBehaviour
{
    private bool isLogin;
    private readonly string pass = "pass@123";
    private readonly string userName = "user@mail.com";

    [SerializeField] GameObject objEyeLine;
    [SerializeField] TMP_InputField inputUserName;
    [SerializeField] TMP_InputField inputPass;


    private void InLogin()
    {
        Msg.instance.Disable();
        Loading.instance.LoadLevel(1, 1);
    }


    public void ButtonLogin()
    {
        if (isLogin) return;

        if (!inputUserName.text.ToLower().Equals(userName.ToLower()))
        {
            Msg.instance.DisplayMsg("Enter valid User Name", Color.red);
            return;
        }

        if (!inputPass.text.Equals(pass))
        {
            Msg.instance.DisplayMsg("Enter valid Password", Color.red);
            return;
        }

        Msg.instance.DisplayMsg("Logging in...", Color.white);
        isLogin = true;

        Invoke(nameof(InLogin), 0.5f);
    }

    public void ButtonShowPass()
    {
        if (inputPass.contentType == TMP_InputField.ContentType.Password)
        {
            inputPass.contentType = TMP_InputField.ContentType.Standard;
            objEyeLine.SetActive(true);
        }
        else
        {
            inputPass.contentType = TMP_InputField.ContentType.Password;
            objEyeLine.SetActive(false);
        }
       
        inputPass.DeactivateInputField();
        inputPass.ActivateInputField();
    }
}