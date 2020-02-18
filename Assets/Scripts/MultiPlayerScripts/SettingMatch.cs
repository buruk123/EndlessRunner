using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMatch : MonoBehaviour
{
    private Connect connection;
    private int score;

    private void Start()
    {
        connection = FindObjectOfType<Connect>();
    }
}
