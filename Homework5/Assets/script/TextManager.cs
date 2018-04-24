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
    GameObject sport_button;
    GameObject physical_button;
    IScore _getScore = GameScenceController.getGSController() as IScore;
    int round;
    GameStatus _gameStatu;
    String mode;
    // Use this for initialization
    void Start()
    {
        scoreText = GameObject.Find("Score");
        billbored = GameObject.Find("Billbored");
        sport_button = GameObject.Find("sport");
        physical_button = GameObject.Find("Physical");
        sport_button.GetComponent<Button>().onClick.AddListener(delegate () {
            this.OnClick(sport_button);
        });
        physical_button.GetComponent<Button>().onClick.AddListener(delegate () {
            this.OnClick(physical_button);
        });
        mode = "Physical";
    }

    // Update is called once per frame
    public void OnClick(GameObject sender)
    {
        switch (sender.name)
        {
            case "sport":
                mode = "sport";
                GameScenceController.getGSController().setmode(mode);
                Debug.Log("sport");
                Destroy(sport_button);
                Destroy(physical_button);

                break;
            case "Physical":
                mode = "Physical";
                GameScenceController.getGSController().setmode(mode);
                Destroy(sport_button);
                Destroy(physical_button);
                Debug.Log("Physical");
                break;
            default:
                Debug.Log("none");
                break;
        }
    }
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
