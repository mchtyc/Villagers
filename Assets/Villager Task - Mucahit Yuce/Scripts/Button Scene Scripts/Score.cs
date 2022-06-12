using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] Text scoreText;

    void OnEnable() 
    {
        Character.OnAddScoreEvent += UpdateScore;
        Drop.OnItemCollected += UpdateScore;
    }

    void OnDisable()
    {
        Character.OnAddScoreEvent -= UpdateScore;
        Drop.OnItemCollected -= UpdateScore;
    }

    public void UpdateScore(int _score) => scoreText.text = "Score: " + _score;
}
