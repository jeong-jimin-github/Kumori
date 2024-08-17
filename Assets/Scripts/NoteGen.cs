using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class Data
{
    public string name;
    public int maxBlock;
    public int BPM;
    public int offset;
    public Note[] notes;
}

[Serializable]
public class Note
{
    public int type;
    public int num;
    public int block;
    public int LPB;
    public Note[] notes;
}

public class NoteGen : MonoBehaviour
{
    int BPM;
    float offset;
    int garim = 3;
    public int noteNum;
    private string songName;
    public List<int> LaneNum = new List<int>();
    public List<int> NoteType = new List<int>();
    public List<float> NotesTime = new List<float>();
    public List<GameObject> NotesObj = new List<GameObject>();
    public Material lineMaterial;

    [SerializeField] private float NotesSpeed;
    [SerializeField] GameObject noteObj;
    [SerializeField] GameObject RNotePrefab;

    void Start()
    {
        NotesSpeed = PlayerPrefs.GetInt("Speed");
        noteNum = 0;
        songName = PlayerPrefs.GetString("Song");
        if (songName != "")
        {
            Load(songName);
        }
        else
        {
            Load("アスノヨゾラ哨戒班");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AdjustNotePositions("-");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AdjustNotePositions("+");
        }
    }

private void AdjustNotePositions(string mp)
{
    // 10 이하, 20 이상으로 속도 조절을 못하게 함
    if (NotesSpeed > 10 && mp == "+")
    {
        return;
    }
    else if (NotesSpeed < 20 && mp == "-")
    {
        return;
    }
    else
    {
        if (mp == "+")
        {
            NotesSpeed += 1f;
        }
        else if (mp == "-")
        {
            NotesSpeed -= 1f;
        }

        PlayerPrefs.SetInt("Speed", (int)NotesSpeed);

        for (int i = 0; i < NotesObj.Count; i++)
        {
            NoteMove noteMove = NotesObj[i].GetComponent<NoteMove>();
            if (noteMove != null)
            {
                noteMove.AdjustPosition(NotesSpeed);
            }
        }
    }
}


    private void Load(string SongName)
    {
        string inputString = System.IO.File.ReadAllText(Application.persistentDataPath + "/" + SongName + ".json");
        Data inputJson = JsonUtility.FromJson<Data>(inputString);

        BPM = inputJson.BPM;
        offset = (float)inputJson.offset / 44100f;
        print("Offset: " + offset);
        noteNum = inputJson.notes.Length;
        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            float kankaku = 60 / (inputJson.BPM * (float)inputJson.notes[i].LPB);
            float beatSec = kankaku * (float)inputJson.notes[i].LPB;
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + offset + PlayerPrefs.GetFloat("Offset") + garim;

            if (inputJson.notes[i].type == 1)
            {
                NotesTime.Add(time);
                LaneNum.Add(inputJson.notes[i].block);
                NoteType.Add(inputJson.notes[i].type);

                float z = NotesTime[NotesTime.Count - 1] * NotesSpeed;
                NotesObj.Add(Instantiate(noteObj, new Vector3(inputJson.notes[i].block - 1.5f, z, 0), Quaternion.identity));
            }
            else if (inputJson.notes[i].type == 2)
            {
                NotesTime.Add(time);
                LaneNum.Add(inputJson.notes[i].block);
                NoteType.Add(inputJson.notes[i].type);

                float z = NotesTime[NotesTime.Count - 1] * NotesSpeed;
                GameObject rNote = Instantiate(noteObj, new Vector3(inputJson.notes[i].block - 1.5f, z, 0), Quaternion.identity);
                NotesObj.Add(rNote);

                LineRenderer lineRenderer = rNote.AddComponent<LineRenderer>();
                lineRenderer.material = lineMaterial;
                lineRenderer.startWidth = 0.3f;
                lineRenderer.endWidth = 0.3f;

                lineRenderer.alignment = LineAlignment.View;

                List<Vector3> positions = new List<Vector3>();
                positions.Add(rNote.transform.position);
                for (int a = 0; a < inputJson.notes[i].notes.Length; a++)
                {
                    float timea = (beatSec * inputJson.notes[i].notes[a].num / (float)inputJson.notes[i].notes[a].LPB) + offset + PlayerPrefs.GetFloat("Offset") + garim;
                    float zz = timea * NotesSpeed;

                    NotesTime.Add(timea);
                    LaneNum.Add(inputJson.notes[i].notes[a].block);
                    NoteType.Add(3);

                    GameObject rNoteChild = Instantiate(RNotePrefab, new Vector3(inputJson.notes[a].block - 1.5f, zz, 0), Quaternion.identity);
                    NotesObj.Add(rNoteChild);

                    positions.Add(rNoteChild.transform.position);
                }
                lineRenderer.positionCount = positions.Count;
                lineRenderer.SetPositions(positions.ToArray());
            }
        }
    }
}
