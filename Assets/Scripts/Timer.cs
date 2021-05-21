using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    [SerializeField] Text timerText;
    [SerializeField] Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTimer", 1, 1);
        GameManager.GameOver += StopTimer;
    }

    // Update the onscreen timer in the format mm:ss
    private void UpdateTimer()
    {
        int minutes = (int)(Time.timeSinceLevelLoad / 60);
        int seconds = (int)(Time.timeSinceLevelLoad % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void OnDestroy()
    {
        GameManager.GameOver -= StopTimer;
    }

    void StopTimer ()
    {
        CancelInvoke("UpdateTimer");
        timeText.text = "Your time: " + timerText.text;
    }
}
