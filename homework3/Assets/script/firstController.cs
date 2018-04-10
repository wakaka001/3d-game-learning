using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Mygame;

public class firstController : MonoBehaviour, SceneController, UserAction
{
    UserGUI userGUI;

    public shoreController fromShore;
    public shoreController toShore;
    public BoatController boat;
    private MyCharactorController[] characters;

    private FirstSceneActionManager actionManager;

    void Awake()
    {
        Director director = Director.getInstance();
        director.currentSceneController = this;
        userGUI = gameObject.AddComponent<UserGUI>() as UserGUI;
        characters = new MyCharactorController[6];
        loadResources();
    }
    void Start()
    {
        actionManager = GetComponent<FirstSceneActionManager>();
    }
    public void loadResources()
    {
        fromShore = new shoreController("from");
        toShore = new shoreController("to");
        boat = new BoatController();

        loadCharacter();
    }
    private void loadCharacter()
    {
        for (int i = 0; i < 3; i++)
        {
            MyCharactorController cha = new MyCharactorController("priest");
            cha.setName("priest" + i);
            cha.setPosition(fromShore.getEmptyPosition());
            cha.getOnShore(fromShore);
            fromShore.getOnShore(cha);

            characters[i] = cha;
        }

        for (int i = 0; i < 3; i++)
        {
            MyCharactorController cha = new MyCharactorController("devil");
            cha.setName("devil" + i);
            cha.setPosition(fromShore.getEmptyPosition());
            cha.getOnShore(fromShore);
            fromShore.getOnShore(cha);

            characters[i + 3] = cha;
        }
    }
    public void moveBoat()
    {
        if (boat.isEmpty())
            return;
        actionManager.moveBoat(boat);
        boat.Move();

        userGUI.status = check_game_over();
        
    }
    public void characterIsClicked(MyCharactorController characterCtrl)
    {
        if (characterCtrl.isOnBoat())
        {
            shoreController whichShore;
            if (boat.get_to_or_from() == -1)
            { // to->-1; from->1
                whichShore = toShore;
            }
            else
            {
                whichShore = fromShore;
            }

            boat.GetOffBoat(characterCtrl.getName());
            //characterCtrl.moveToPosition(whichShore.getEmptyPosition());
            actionManager.moveCharacter(characterCtrl, whichShore.getEmptyPosition());
            characterCtrl.getOnShore(whichShore);
            whichShore.getOnShore(characterCtrl);

        }
        else
        {                                   // character on Shore
            shoreController whichShore = characterCtrl.getCoastController();

            if (boat.getEmptyIndex() == -1)
            {       // boat is full
                
                return;
            }

            if (whichShore.get_to_or_from() != boat.get_to_or_from())   // boat is not on the side of character
                return;

            whichShore.getoffShore(characterCtrl.getName());
            //characterCtrl.moveToPosition(boat.getEmptyPosition());
            if (characterCtrl == null) Debug.Log("characterCtrl error");
            if (boat.getEmptyPosition() == null) Debug.Log("boat.getEmptyPosition() error");
            if (actionManager == null) Debug.Log("actionManager error");
            actionManager.moveCharacter(characterCtrl, boat.getEmptyPosition());
            characterCtrl.getOnBoat(boat);
            boat.GetOnBoat(characterCtrl);
        }
        userGUI.status = check_game_over();
    }
    int check_game_over()
    {   // 0 means playing, 1 means lose, 2 means win
        int from_priest = 0;
        int from_devil = 0;
        int to_priest = 0;
        int to_devil = 0;

        int[] fromCount = fromShore.getCharacterNum();
        from_priest += fromCount[0];
        from_devil += fromCount[1];

        int[] toCount = toShore.getCharacterNum();
        to_priest += toCount[0];
        to_devil += toCount[1];

        if (to_priest + to_devil == 6)      // win
            return 2;

        int[] boatCount = boat.getCharacterNum();
        if (boat.get_to_or_from() == -1)
        {   // boat at toShore
            to_priest += boatCount[0];
            to_devil += boatCount[1];
        }
        else
        {   // boat at fromShore
            from_priest += boatCount[0];
            from_devil += boatCount[1];
        }
        if (from_priest < from_devil && from_priest > 0)
        {       // lose
            return 1;
        }
        if (to_priest < to_devil && to_priest > 0)
        {
            return 1;
        }
        return 0;           // still playing
    }
    public void restart()
    {
        boat.reset();
        fromShore.reset();
        toShore.reset();
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].reset();
        }
    }
}
