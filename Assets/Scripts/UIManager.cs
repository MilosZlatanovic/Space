﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _LiveImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _GameOver;
    [SerializeField]
    private Text _restartGame;
    private GameManager _gameManager;
    
    void Start()
    {
       
        _scoreText.text = "Score:" + 0;
        _GameOver.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.Log("GameManager is NULL");
        }
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score:" + playerScore;
    }
    public void UpdateLives(int currentLives)
    {
        _LiveImg.sprite = _liveSprites[currentLives];

        if (currentLives == 0)
        {
            GameOverSequence();
        }

    }
    void GameOverSequence()
    {
        _GameOver.gameObject.SetActive(true);
        _restartGame.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
        _gameManager.GameIsOver();
    }
    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _GameOver.text = "GAME OVER!";
            yield return new WaitForSeconds(0.5f);
            _GameOver.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
