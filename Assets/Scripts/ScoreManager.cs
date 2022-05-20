using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;
    public Text scoreText;

    void Start()
    {
        scoreText.text = score.ToString() + " POINTS";
    }

    void Update()
    {
        var player = GameObject.FindWithTag("Player").GetComponent<Player>();
        score = player.score;
        if (score == 1) {
            scoreText.text = score.ToString() + " POINT";
        } else {
        scoreText.text = score.ToString() + " POINTS";
        }
    }
}
