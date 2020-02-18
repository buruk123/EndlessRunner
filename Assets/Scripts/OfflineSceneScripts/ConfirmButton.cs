using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmButton : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Start()
    {
        button.onClick.AddListener(ExitApplicationOnConfirm);
    }

    private void ExitApplicationOnConfirm()
    {
        Application.Quit();
    }


}
