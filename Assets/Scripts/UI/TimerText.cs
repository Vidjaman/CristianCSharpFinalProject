using System;
using TMPro;
using UnityEngine;

public class TimerText : MonoBehaviour
{
    TextMeshProUGUI text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(GameManager.Instance.timer);
        string timeText = string.Format(" {0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        text.text= "Time left:"+timeText;
    }
}
