using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class offset : MonoBehaviour
{
    AudioManager audioManager;
    public Button aB;
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        aB.onClick.AddListener(setting);
    }
    void setting()
    {
        audioManager.Stop(gotooffset);
    }

    void gotooffset()
    {
        SceneManager.LoadSceneAsync("Setting");
     }
}
