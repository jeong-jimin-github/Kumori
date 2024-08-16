using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Metronome : MonoBehaviour
{
    public float bpm = 120.0f;
    private float beatInterval;
    private float timer;
    private float totalOffset;
    private int touchCount;
    AudioManager audioManager;

    public Text offsetText;


    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.LoadAudio("bip");
        beatInterval = 60.0f / bpm;
        timer = beatInterval;
        totalOffset = 0.0f;
        touchCount = 0;

    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0.0f)
        {
            Debug.Log("Beat!");
            audioManager.PlayAudio();
            timer += beatInterval;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            float offset = timer;
            if (offset < 0)
            {
                offset += beatInterval; // 음수 offset을 beatInterval로 보정
            }

            totalOffset += offset;
            touchCount++;

            float averageOffset = totalOffset / touchCount;
            Debug.Log("Average offset from beat: " + averageOffset.ToString("F3") + " seconds");
            offsetText.text = averageOffset.ToString("F3");

            if (touchCount >= 10)
            {
                PlayerPrefs.SetFloat("Offset", averageOffset);
                SceneManager.LoadScene("Select");
            }
        }
    }
}