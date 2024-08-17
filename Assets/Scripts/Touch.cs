using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Toucah : MonoBehaviour
{
    public Text guiTextObject;
    public Text score;
    public Text scoreText;
    public Animation aa;
    public Animation one;
    public Animation two;
    public Animation three;
    public Animation four;

    public Animation perfecta;
    public Animation greata;
    public Animation gooda;
    public Animation missa;
    int maxCombo = 0;
    public GameObject sccb;
    public GameObject lineRenderersParent;
    public LineRenderer collidedLineRenderer;

    public NoteGen noteGen; // NoteGen 스크립트를 참조합니다.
    private float perfectThreshold = 0.05f;
    private float greatThreshold = 0.15f;
    private float goodThreshold = 0.25f;
    public GameObject timer;
    float time = 0;
    double percent;

    private void Start()
    {
        PlayerPrefs.SetInt("Perfect", 0);
        PlayerPrefs.SetInt("Great", 0);
        PlayerPrefs.SetInt("Good", 0);
        PlayerPrefs.SetInt("Miss", 0);
        PlayerPrefs.SetInt("Score", 0);
    }

    void Update()
    {
        if (maxCombo != 0)
        {
            percent = int.Parse(score.text) / (maxCombo * 300.0) * 100.0;
        }
        else
        {
            percent = 100.00;
        }

        string formattedPercent = percent.ToString("F2");

        scoreText.text = formattedPercent;

        if (timer.GetComponent<timer>().start == true)
        {
            time = time + Time.deltaTime;
        }

        float currentTime = time;

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (Time.timeScale == 1)
            {
                one.Play();
                CheckNoteInLine(0, currentTime);
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Time.timeScale == 1)
            {
                two.Play();
                CheckNoteInLine(1, currentTime);
            }
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (Time.timeScale == 1)
            {
                three.Play();
                CheckNoteInLine(2, currentTime);
            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (Time.timeScale == 1)
            {
                four.Play();
                CheckNoteInLine(3, currentTime);
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            CheckHoldNoteInLine(0, currentTime);
        }
        if (Input.GetKey(KeyCode.F))
        {
            CheckHoldNoteInLine(1, currentTime);
        }
        if (Input.GetKey(KeyCode.J))
        {
            CheckHoldNoteInLine(2, currentTime);
        }
        if (Input.GetKey(KeyCode.K))
        {
            CheckHoldNoteInLine(4, currentTime);
        }

        CheckMissedNotes(currentTime);
    }

    void CheckNoteInLine(int lineIndex, float currentTime)
    {
        for (int i = 0; i < noteGen.NotesTime.Count; i++)
        {
            if (noteGen.LaneNum[i] == lineIndex && noteGen.NoteType[i] != 3)
            {
                float noteTime = noteGen.NotesTime[i];
                float timeDifference = Mathf.Abs(noteTime - currentTime);
                if (timeDifference <= perfectThreshold)
                {
                    perfect();
                    UpdateMaxCombo();
                    RemoveNoteAt(i);
                    return;
                }
                else if (timeDifference <= greatThreshold)
                {
                    great();
                    UpdateMaxCombo();
                    RemoveNoteAt(i);
                    return;
                }
                else if (timeDifference <= goodThreshold)
                {
                    good();
                    UpdateMaxCombo();
                    RemoveNoteAt(i);
                    return;
                }
                else if (timeDifference <= goodThreshold + 0.05)
                {
                    miss();
                    UpdateMaxCombo();
                    RemoveNoteAt(i);
                    return;
                }
            }
        }
    }

    void CheckHoldNoteInLine(int lineIndex, float currentTime)
    {
        for (int i = 0; i < noteGen.NotesTime.Count; i++)
        {
            if (noteGen.LaneNum[i] == lineIndex && noteGen.NoteType[i] == 3)
            {
                float noteTime = noteGen.NotesTime[i];
                float timeDifference = Mathf.Abs(noteTime - currentTime);

                if (timeDifference >= 0 && timeDifference <= goodThreshold)
                {
                    perfect();
                    UpdateMaxCombo();
                    RemoveNoteAt(i);
                    return;
                }
            }
        }
    }

    void RemoveNoteAt(int index)
    {
        if (noteGen.NoteType[index] != 2 && noteGen.NoteType[index] != 3)
        {
            Destroy(noteGen.NotesObj[index]);
        }
        noteGen.NotesTime.RemoveAt(index);
        noteGen.LaneNum.RemoveAt(index);
        noteGen.NoteType.RemoveAt(index);
        noteGen.NotesObj.RemoveAt(index);
    }

    void CheckMissedNotes(float currentTime)
    {
        for (int i = noteGen.NotesTime.Count - 1; i >= 0; i--)
        {
            if (noteGen.NotesTime[i] < currentTime - goodThreshold)
            {
                miss();
                UpdateMaxCombo();
                RemoveNoteAt(i);
            }
        }
    }

    void UpdateMaxCombo()
    {
        maxCombo++;
    }

    void perfect()
    {
        perfecta.Play();
        guiTextObject.text = (int.Parse(guiTextObject.text) + 1).ToString();
        aa.Play();
        score.text = (int.Parse(score.text) + 300).ToString();
        PlayerPrefs.SetInt("Score", int.Parse(score.text));
        PlayerPrefs.SetInt("Perfect", PlayerPrefs.GetInt("Perfect") + 1);
    }

    void great()
    {
        greata.Play();
        aa.Play();
        guiTextObject.text = (int.Parse(guiTextObject.text) + 1).ToString();
        score.text = (int.Parse(score.text) + 200).ToString();
        PlayerPrefs.SetInt("Score", int.Parse(score.text));
        PlayerPrefs.SetInt("Great", PlayerPrefs.GetInt("Great") + 1);
    }

    void good()
    {
        gooda.Play();
        aa.Play();
        guiTextObject.text = (int.Parse(guiTextObject.text) + 1).ToString();
        score.text = (int.Parse(score.text) + 100).ToString();
        PlayerPrefs.SetInt("Score", int.Parse(score.text));
        PlayerPrefs.SetInt("Good", PlayerPrefs.GetInt("Good") + 1);
    }

    void miss()
    {
        missa.Play();
        guiTextObject.text = "0";
        PlayerPrefs.SetInt("Miss", PlayerPrefs.GetInt("Miss") + 1);
    }
}
