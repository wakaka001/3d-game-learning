using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.Manager;

public class TextManager : MonoBehaviour
{

    GameObject scoreText;
    GameObject billbored;
    IScore _getScore = GameScenceController.getGSController() as IScore;
    int round;
    GameStatus _gameStatu;
    // Use this for initialization
    void Start()
    {
        scoreText = GameObject.Find("Score");
        billbored = GameObject.Find("Billbored");
        
    }

    // Update is called once per frame
    void Update()
    {
        _gameStatu = GameScenceController.getGSController().queryGameStatus();
        round = GameScenceController.getGSController()._DiskThrower.round;
        string score = "Score:" + Convert.ToString(_getScore.getScore());
        string round_text = "Round:" + Convert.ToString(round);
        if (_gameStatu == GameStatus.fail)
        {
            billbored.GetComponent<Text>().text = "Fail!\nPress Space to restart!";
        }
        else if (_gameStatu == GameStatus.win)
        {
            billbored.GetComponent<Text>().text = "Win!";
        }
        else if (_gameStatu == GameStatus.ing)
            billbored.GetComponent<Text>().text = " ";
        else if (_gameStatu == GameStatus.load)
            billbored.GetComponent<Text>().text = "Press Space to start!\nAfter 5 rounds you will win\nGood Luck!";
        scoreText.GetComponent<Text>().text = score + "\n" + round_text; ;
    }
}
