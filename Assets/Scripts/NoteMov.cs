using UnityEngine;

public class NoteMove : MonoBehaviour
{
    GameObject timer;
    public float NoteSpeed;
    LineRenderer lineRenderer;
    private float previousSpeed;

    private void Start()
    {
        previousSpeed = PlayerPrefs.GetInt("Speed");
        timer = GameObject.Find("Timer");
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        MoveNote();
    }

    void MoveNote()
    {
        NoteSpeed = PlayerPrefs.GetInt("Speed");
        if (timer.GetComponent<timer>().start == true)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * NoteSpeed, 0f);

            if (lineRenderer != null)
            {
                Vector3[] positions = new Vector3[lineRenderer.positionCount];
                lineRenderer.GetPositions(positions);
                for (int i = 0; i < positions.Length; i++)
                {
                    positions[i] = new Vector3(positions[i].x, positions[i].y - Time.deltaTime * NoteSpeed, positions[i].z);
                }
                lineRenderer.SetPositions(positions);
            }
        }
    }

    public void AdjustPosition(float newSpeed)
    {
        float timeElapsed = (transform.position.y / previousSpeed);
        float newY = timeElapsed * newSpeed;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        if (lineRenderer != null)
        {
            Vector3[] positions = new Vector3[lineRenderer.positionCount];
            lineRenderer.GetPositions(positions);
            for (int i = 0; i < positions.Length; i++)
            {
                float positionTimeElapsed = (positions[i].y / previousSpeed);
                positions[i] = new Vector3(positions[i].x, positionTimeElapsed * newSpeed, positions[i].z);
            }
            lineRenderer.SetPositions(positions);
        }

        previousSpeed = newSpeed;
    }
}
