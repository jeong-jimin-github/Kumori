using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class DataIO : MonoBehaviour
{
    private string url = "https://rhythm.iwinv.net/data.php";

    // POST 요청 예제
    public void SendPostRequest(string userId, string jsonData)
    {
        StartCoroutine(PostRequest(userId, jsonData));
    }

    private IEnumerator PostRequest(string userId, string jsonData)
    {
        WWWForm form = new WWWForm();
        form.AddField("user_id", userId);
        form.AddField("json_data", jsonData);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Response: " + www.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error: " + www.error);
            }
        }
    }

    public class YourDataClass
    {
        public string nickname;
        public string lv;
        public string exp;
        public string gold;
        public string allgame;
        public string win;
        public string lose;
    }

    public class JsonDataWrapper
    {
        public string json_data;
    }

    // GET 요청 예제
    public void SendGetRequest(string userId)
    {
        StartCoroutine(GetRequest(userId));
    }

    private IEnumerator GetRequest(string userId)
    {
        string fullUrl = url + "?user_id=" + userId;

        using (UnityWebRequest www = UnityWebRequest.Get(fullUrl))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Raw JSON Response: " + www.downloadHandler.text);

                try
                {
                    // 첫 번째 역직렬화: JSON 배열에서 json_data 필드를 포함한 객체를 추출
                    var wrapperList = JsonConvert.DeserializeObject<List<JsonDataWrapper>>(www.downloadHandler.text);

                    if (wrapperList != null && wrapperList.Count > 0)
                    {
                        var jsonDataString = wrapperList[0].json_data;
                        
                        // 두 번째 역직렬화: json_data 필드의 JSON 문자열을 YourDataClass로 역직렬화
                        var data = JsonConvert.DeserializeObject<YourDataClass>(jsonDataString);
                        
                        if (data != null)
                        {
                            // 데이터 저장
                            PlayerPrefs.SetString("nickname", data.nickname);
                            PlayerPrefs.SetString("lv", data.lv);
                            PlayerPrefs.SetString("exp", data.exp);
                            PlayerPrefs.SetString("gold", data.gold);
                            PlayerPrefs.SetString("allgame", data.allgame);
                            PlayerPrefs.SetString("win", data.win);
                            PlayerPrefs.SetString("lose", data.lose);

                            // 저장된 데이터 로그 출력
                            Debug.Log("nickname: " + PlayerPrefs.GetString("nickname", "default"));
                            Debug.Log("lv: " + PlayerPrefs.GetString("lv", "default"));
                            Debug.Log("exp: " + PlayerPrefs.GetString("exp", "default"));
                            Debug.Log("gold: " + PlayerPrefs.GetString("gold", "default"));
                            Debug.Log("allgame: " + PlayerPrefs.GetString("allgame", "default"));
                            Debug.Log("win: " + PlayerPrefs.GetString("win", "default"));
                            Debug.Log("lose: " + PlayerPrefs.GetString("lose", "default"));

                            Debug.Log("Data loaded and stored successfully.");
                        }
                        else
                        {
                            Debug.LogWarning("Failed to deserialize json_data.");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("No data found or the list is empty.");
                    }
                }
                catch (JsonSerializationException ex)
                {
                    Debug.LogError("JSON parsing error: " + ex.Message);
                }
            }
            else
            {
                Debug.LogError("Error: " + www.error);
            }
        }
    }
}
