using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserManager : MonoBehaviour
{
    public Text Name;
    public Text Level;
    // Start is called before the first frame update
    void Start()
    {
        Name.text = PlayerPrefs.GetString("nickname");
        print(PlayerPrefs.GetString("nickname"));
        print(PlayerPrefs.GetString("lv"));
        Level.text = PlayerPrefs.GetString("lv");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
