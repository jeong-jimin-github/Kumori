using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorManager : MonoBehaviour
{
    public Button Okay;
    public GameObject ERRORUI;
    // Start is called before the first frame update
    void Start()
    {
     Okay.onClick.AddListener(OkayFunc);   
    }

    void OkayFunc()
    {
        ERRORUI.SetActive(false);
    }
}
