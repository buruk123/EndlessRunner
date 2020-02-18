using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    [SerializeField] private Button login;
    [SerializeField] private Button register;
    public static string userName;

    [Header("Login requires")]
    [SerializeField] private InputField loginTextLogin;
    [SerializeField] private InputField passwordTextLogin;
    [SerializeField] private GameObject errorEmptyTextLogin;
    [SerializeField] private GameObject errorLoginTextLogin;
    [SerializeField] private Button confirmLogin;

    [Header("Register requires")]
    [SerializeField] private InputField loginTextRegister;
    [SerializeField] private InputField passwordTextRegister;
    [SerializeField] private InputField repeatPasswordTextRegister;
    [SerializeField] private GameObject errorEmptyTextRegister;
    [SerializeField] private GameObject errorLoginTextRegister;
    [SerializeField] private GameObject errorPasswordNotMatchRegister;
    [SerializeField] private GameObject errorNameExistsRegister;
    [SerializeField] private Button confirmRegister;

    [Header("Containers")]
    [SerializeField] private GameObject loginAndRegisterContainer;
    [SerializeField] private GameObject registerContainer;
    [SerializeField] private GameObject loginContainer;


    private void Start()
    {
        login.onClick.AddListener(Login);
        register.onClick.AddListener(Register);
        confirmLogin.onClick.AddListener(ConfirmLogin);
        confirmRegister.onClick.AddListener(ConfirmRegister);
        loginAndRegisterContainer.SetActive(true);
        loginContainer.SetActive(false);
        registerContainer.SetActive(false);
        errorEmptyTextLogin.SetActive(false);
        errorLoginTextLogin.SetActive(false);
        errorEmptyTextRegister.SetActive(false);
        errorLoginTextRegister.SetActive(false);
        errorPasswordNotMatchRegister.SetActive(false);
        errorNameExistsRegister.SetActive(false);
    }

    private void Login()
    {
        loginAndRegisterContainer.SetActive(false);
        loginContainer.SetActive(true);
    } 

    private void Register()
    {
        loginAndRegisterContainer.SetActive(false);
        registerContainer.SetActive(true);
    }

    private async void ConfirmLogin()
    {
        if(loginTextLogin.text == null || passwordTextLogin == null)
        {
            errorEmptyTextLogin.SetActive(true);
            await Task.Delay(2000);
            errorEmptyTextLogin.SetActive(false);
        }
        else
        {
            StartCoroutine(ConnectToWWW());
        }
    }

    private IEnumerator ConnectToWWW()
    {
        WWW site = new WWW("https://runnerendless.000webhostapp.com/loginpassword.php?name=" + loginTextLogin.text + "&password=" + passwordTextLogin.text);
        yield return site;
        if (site.text == "-1")
        {
            errorLoginTextLogin.SetActive(true);
            yield return new WaitForSeconds(2f);
            errorLoginTextLogin.SetActive(false);
        }
        else
        {
            userName = loginTextLogin.text;
            SceneManager.LoadScene("MainMenuScene");
        }
    }

    private async void ConfirmRegister()
    {
        if (loginTextRegister.text == null || passwordTextRegister == null || repeatPasswordTextRegister == null)
        {
            errorEmptyTextRegister.SetActive(true);
            await Task.Delay(2000);
            errorEmptyTextRegister.SetActive(false);
        }
        else if(passwordTextRegister.text != repeatPasswordTextRegister.text)
        {
            errorPasswordNotMatchRegister.SetActive(true);
            await Task.Delay(2000);
            errorPasswordNotMatchRegister.SetActive(false);
        }
        else
        {
            StartCoroutine(AddUserAndConnect());
        }
    }

    private IEnumerator AddUserAndConnect()
    {
        WWW site = new WWW("http://runnerendless.000webhostapp.com/register.php?name=" + loginTextRegister.text + "&password=" + passwordTextRegister.text);
        yield return site;
        Debug.Log(site.text);
        if(site.text == "-1")
        {
            errorNameExistsRegister.SetActive(true);
            yield return new WaitForSeconds(2f);
            errorNameExistsRegister.SetActive(false);
        }
        else if(site.text == "1")
        {
            userName = loginTextRegister.text;
            SceneManager.LoadScene("MainMenuScene");
        }
    }

    public string GetUserName()
    {
        return loginTextLogin.text != null ? loginTextLogin.text : loginTextRegister.text != null ? loginTextRegister.text : "";
    }
}
