using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main2Login : MonoBehaviour
{
    private float currentAlpha = 0f;  // 현재 알파 값
    private bool isFadingOut = false;  // 페이드아웃 진행 여부
    public Image panel;
    float time = 0;
    private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.LoadAudio(Application.streamingAssetsPath + "/Audio/" + "Main", "mp3");
        audioManager.PlayAudio();
    }

    public void StartFadeOut()
    {
        isFadingOut = true;
        currentAlpha = 0f;  // 알파 값 초기화
        audioManager.Stop(OnFadeOutComplete);
    }

    private void SetAlpha(float alpha)
    {
        Color color = panel.color;
        color.a = Mathf.Clamp(alpha, 0f, 1f);
        panel.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(time > 7f){
            LoginFunc();
            }
        }
        if (isFadingOut)
        {
            if (currentAlpha < 1.0f)
            {
                currentAlpha += Time.deltaTime / 2f;
                SetAlpha(currentAlpha);
            }
        }
    }
    void LoginFunc()
    {
        StartFadeOut();
    }

    void OnFadeOutComplete()
    {
        SceneManager.LoadScene("Login");
    }
}
