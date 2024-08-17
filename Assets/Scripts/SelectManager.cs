using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour
{
    public Button AsunoYozoraShoukaihan;
    public Button HatsuneMikunoShoushitsu;
    public Button Unwelcome_School;
    public Sprite AsunoYozoraShoukaihanArt;
    public Sprite HatsuneMikunoShoushitsuArt;
    public Sprite Unwelcome_SchoolArt;
    public Image image;
    public Text title;
    public Text auther;
    public Text bpm;
    AudioManager audioManager;
    
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        AsunoYozoraShoukaihan.onClick.AddListener(asu);
        HatsuneMikunoShoushitsu.onClick.AddListener(shousitu);
        Unwelcome_School.onClick.AddListener(unwelcome);
        if(PlayerPrefs.GetString("Song") == "アスノヨゾラ哨戒班") asu();
        else if (PlayerPrefs.GetString("Song") == "初音ミクの消失") shousitu();
        else if (PlayerPrefs.GetString("Song") == "Unwelcome School") unwelcome();
        else asu();
        audioManager.LoadAudio(Application.streamingAssetsPath + "/Audio/" + "Kyoku", "wav");
        audioManager.PlayAudio();
    }
    void asu()
    {
        SaveSelect("アスノヨゾラ哨戒班");
        image.sprite = AsunoYozoraShoukaihanArt;
        title.text = "アスノヨゾラ哨戒班";
        auther.text = "Orangestar";
        bpm.text = "BPM 185";
        AudioPlay();
        
    }
    void shousitu()
    {
        SaveSelect("初音ミクの消失");
        image.sprite = HatsuneMikunoShoushitsuArt;
        title.text = "初音ミクの消失";
        auther.text = "cosMo@暴走P";
        bpm.text = "BPM 240";
        AudioPlay();
    }
    void unwelcome()
    {
        SaveSelect("Unwelcome School");
        image.sprite = Unwelcome_SchoolArt;
        title.text = "Unwelcome School";
        auther.text = "Blue Archive OST";
        bpm.text = "BPM 180";
        AudioPlay();
    }   
    void SaveSelect(string name)
    {
        PlayerPrefs.SetString("Song", name);
    
PlayerPrefs.Save();
    }

    void AudioPlay()
    {
        audioManager.Stop(play);
    }

    void play()
    {
        audioManager.LoadAudio(Application.persistentDataPath + "/" + PlayerPrefs.GetString("Song"), "wav");
        audioManager.PlayAudio();
    }
}
