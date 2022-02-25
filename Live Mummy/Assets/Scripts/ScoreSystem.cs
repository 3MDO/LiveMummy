using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _highScoreText;
    [SerializeField] TMP_Text _multiplierText;
    [SerializeField] FloatScoreText _floatingTextPrefab;
    [SerializeField] Canvas _floatingScoreCanvas;

    int _score;
    private int _highScore;
    private float _scoreMultiplierExpiration;
    private int _killMultiplier;

    void Start()
    {
        Mummy.Died += Mummy_Died;
        _highScore = PlayerPrefs.GetInt("HighScore");
        _highScoreText.SetText("High Score : " + _highScore);
    }

    void OnDestroy()
    {
        Mummy.Died -= Mummy_Died;
    }

    void Mummy_Died(Mummy mummy)
    {               
        UpdateKillMultiplier();
        
        _score += _killMultiplier;

        if (_score > _highScore)
        {
            _highScore = _score;
            _highScoreText.SetText("High Score : " + _highScore);
            PlayerPrefs.SetInt("HighScore", _highScore);
        }
        _scoreText.SetText(_score.ToString());

        var floatingText = Instantiate(
            _floatingTextPrefab, 
            mummy.transform.position, 
            _floatingScoreCanvas.transform.rotation,
            _floatingScoreCanvas.transform);

        floatingText.SetScoreValue(_killMultiplier);
    }

    void UpdateKillMultiplier()
    {
        if (Time.time <= _scoreMultiplierExpiration)
        {
            _killMultiplier++;
        }
        else
        {
            _killMultiplier = 1;
        }

        _scoreMultiplierExpiration = Time.time + 1f;
        _multiplierText.SetText("x " + _killMultiplier);

        if (_killMultiplier < 3)
            _multiplierText.color = Color.white;
        if (_killMultiplier < 10)
            _multiplierText.color = Color.green;
        if (_killMultiplier < 3)
            _multiplierText.color = Color.yellow;
        if (_killMultiplier < 3)
            _multiplierText.color = Color.red;
    }

}
