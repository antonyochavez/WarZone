using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{

    TextMeshProUGUI scoreText;
    ScoreTracker scoreTracker;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        scoreTracker = FindObjectOfType<ScoreTracker>();
        Debug.Log(scoreText);
        Debug.Log(scoreTracker);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = scoreTracker.GetScore().ToString();
    }
}
