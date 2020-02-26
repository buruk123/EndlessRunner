using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOL : MonoBehaviour
{
    private static DDOL instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this);
        }
    }
}
