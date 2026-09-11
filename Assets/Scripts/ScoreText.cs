using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    TextMeshProUGUI text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text=GetComponent<TextMeshProUGUI>();
        text.text = "High Score: " + GameManager.Instance.GetHighScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
