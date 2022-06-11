using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] Text scoreText;

    void OnEnable() => Character.OnAddScoreEvent += UpdateScore;

    void OnDisable() => Character.OnAddScoreEvent -= UpdateScore;

    public void UpdateScore(int _score) => scoreText.text = "Score: " + _score;
}
