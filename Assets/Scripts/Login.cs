using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{
    public Button join;
    private AudioManager audioManager;
    public Button LoginButton;

    public InputField ID;
    public InputField PW;
    public DataIO dataIO;
    public GameObject ERRORUI;
    public Text ERRORTEXT;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.LoadAudio(Application.streamingAssetsPath + "/Audio/" + "Login", "mp3");
        audioManager.PlayAudio();
        LoginButton.onClick.AddListener(LoginFunc);
        join.onClick.AddListener(joinfunc);
    }

    void ERROR(string REASON){
        if(REASON == "ACCOUNT")
        {
            ERRORUI.SetActive(true);
            ERRORTEXT.text = "계정 정보가 잘못되었습니다.\n다시 시도해 주십시오.";
        }
        else if(REASON == "NETWORK")
        {
            ERRORUI.SetActive(true);
            ERRORTEXT.text = "서버 연결에 실패했습니다.\n인터넷 연결 후 다시 시도해 주십시오.";
        }
        else
        {
            ERRORUI.SetActive(true);
            ERRORTEXT.text = "알 수 없는 에러가 발생했습니다.";
        }
    }
    void LoginFunc()
    {
        string userid = ID.text;
        string userpassword = PW.text;
        if (userid == "" || userpassword == "")
        {
            ERROR("ACCOUNT");
        }
        else
        {
            StartCoroutine(LoginRequest(userid, userpassword));
        }
    }

    IEnumerator LoginRequest(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post("https://rhythm.iwinv.net/login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                ERROR("NETWORK");
            }
            else
            {
                string responseText = www.downloadHandler.text;
                print(responseText);
                if(responseText == "ERROR")
                {
                    ERROR("ACCOUNT");
                    yield break;
                }
                else{
                    PlayerPrefs.SetString("ID", responseText);
                    dataIO.SendGetRequest(responseText);
                    audioManager.Stop(OnFadeOutComplete);
                }
            }
        }
    }

    void joinfunc()
    {
        Application.OpenURL("https://rhythm.iwinv.net/register.php");
    }

    void OnFadeOutComplete()
    {
        SceneManager.LoadScene("DL");
    }
}
