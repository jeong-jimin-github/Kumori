using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class MusicManager : MonoBehaviour
{
    int i = 0;
    public GameObject timer;
    AudioManager audioManager;
    float offset;
    string SongName;

    void Start()
    {
        SongName = PlayerPrefs.GetString("Song");
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.LoadAudio(Application.persistentDataPath + "/" + SongName, "wav");
        offset = 3;
        print(offset);

    }


    IEnumerator Play()
    {
        yield return new WaitForSeconds(offset);
        audioManager.PlayAudio();
    }


    private void Update()
    {

        if (timer.GetComponent<timer>().start == true)
        {
            if (i == 0)
            {
                StartCoroutine(Play());
                i++;
            }
        }
    }
}