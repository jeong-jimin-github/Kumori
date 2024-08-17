using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Select2Game : MonoBehaviour
{
    AudioManager audioManager;
    public Button togame;
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        togame.onClick.AddListener(game);
    }
    void game()
    {
        audioManager.Stop(gotogame);
    }

    void gotogame()
    {
        SceneManager.LoadSceneAsync("Game");
    }
}
