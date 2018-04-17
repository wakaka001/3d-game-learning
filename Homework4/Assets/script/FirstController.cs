using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Manager;

public class FirstController : MonoBehaviour
{
    GameStatus _gameStatus;
    ScenceStatus _scenceStatus;
    GameScenceController _controller;
    IUserInterface _uerInterface;
    IQueryStatus _queryStatus;
    IScore _Score;
    // Use this for initialization
    void Start()
    {
        _controller = GameScenceController.getGSController();
        _uerInterface = GameScenceController.getGSController() as IUserInterface;
        _queryStatus = GameScenceController.getGSController() as IQueryStatus;
        _Score = GameScenceController.getGSController() as IScore;
        _controller.setGameStatus(GameStatus.load);

    }

    // Update is called once per frame
    void Update()
    {
        _gameStatus = _queryStatus.queryGameStatus();
        _scenceStatus = _queryStatus.queryScenceStatus();
        
        if (_gameStatus == GameStatus.ing)
        {
            if (_scenceStatus == ScenceStatus.waiting && Input.GetKeyDown("space"))
            {
                _uerInterface.sendDisk();
               
            }
            if (_scenceStatus == ScenceStatus.shooting && Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Disk")
                {
                    _uerInterface.destroyDisk(hit.collider.gameObject);
                    _Score.addScore();
                }
            }
        }
        else
        {
            //reset
            if(Input.GetKeyDown("space"))
            {
                if (_gameStatus == GameStatus.load) _controller.setGameStatus(GameStatus.ing);
                else
                {
                    _controller.setGameStatus(GameStatus.load);
                    _Score.reset();
                    Debug.Log("reseting");
                }
                    
            }
        }
    }
}
