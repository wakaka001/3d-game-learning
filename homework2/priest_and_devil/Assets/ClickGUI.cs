using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Mygame;

public class ClickGUI : MonoBehaviour {

    UserAction action;
    MyCharactorController charactorController;

    public void setController(MyCharactorController _controller)
    {
        charactorController = _controller;
    }
	// Use this for initialization
	void Start () {
        action = Director.getInstance().currentSceneController as UserAction;
	}

    // Update is called once per frame
    void OnMouseDown()
    {
        if (gameObject.name == "boat")
        {
            action.moveBoat();
        }
        else
        {
            action.characterIsClicked(charactorController);
        }
    }
 }

