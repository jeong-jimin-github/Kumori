using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class MusicManager : MonoBehaviour
{
    int i = 0;
    public GameObject timer;
    AudioManager audioManager;
    float offset;
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.LoadAudio("bip");
        offset = (float)30 / PlayerPrefs.GetInt("Speed");
        print(offset);

    }




    private void Update()
    {

        if (timer.GetComponent<timer>().start == true)
        {
            if (i == 0)
            {
                //audioPlayer.PlayDelayed(offset);
                i++;
            }
        }
    }
}