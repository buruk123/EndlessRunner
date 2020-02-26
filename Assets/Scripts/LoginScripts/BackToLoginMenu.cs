using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToLoginMenu : MonoBehaviour
{
    [SerializeField] private Button backToLoginButtonOnLogin;
    [SerializeField] private Button backToLoginButtonOnRegister;
    [SerializeField] private GameObject loginContainer;
    [SerializeField] private GameObject registerContainer;
    [SerializeField] private GameObject loginAndRegisterContainer;

    private void Start()
    {
        backToLoginButtonOnLogin.onClick.AddListener(BackToMenu);
        backToLoginButtonOnRegister.onClick.AddListener(BackToMenu);
    }

    private void BackToMenu()
    {
        loginContainer.SetActive(false);
        registerContainer.SetActive(false);
        loginAndRegisterContainer.SetActive(true);
    }
}
