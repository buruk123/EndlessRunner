using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserHolder : MonoBehaviour
{
    [SerializeField] private Text userName;

    private void Start()
    {
        userName.text = "Zalogowany jako: " + LoginManager.userName;
    }
}
