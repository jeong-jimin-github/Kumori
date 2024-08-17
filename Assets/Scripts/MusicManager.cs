using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class MusicManager : MonoBehaviour
{
    int i = 0;
    public GameObject timer;
    AudioManager audioManager;
    float offset;
    string SongName;
    uint songlength;
    float time = 0;

    void Start()
    {
        SongName = PlayerPrefs.GetString("Song");
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.LoadAudio(Application.persistentDataPath + "/" + SongName, "wav");
        songlength = audioManager.getLoadedSoundLength();
        offset = 3;
        print(offset);

    }


    IEnumerator Play()
    {
        yield return new WaitForSeconds(offset);
        audioManager.PlayAudio();
    }
    void gotoResult()
    {
        SceneManager.LoadScene("Result");
    }
    private void Update()
    {

        if (timer.GetComponent<timer>().start == true)
        {
            time = time + Time.deltaTime;
            if (i == 0)
            {
                StartCoroutine(Play());
                i++;
            }

            if (time >= (songlength/1000)) { 
            
                audioManager.Stop(gotoResult);

            }
        }
    }
}