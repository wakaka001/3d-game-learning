using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    private int[,] blocks = new int[3, 3];
    private int player = 1;
    //player1 means dog, player2 means cat
    public Texture2D player1_icon;
    public Texture2D player2_icon;
    // Use this for initialization
    void Start() {
        Reset();
    }

    // Update is called once per frame
    void Update() {

    }
    //return 0 means there is none win,return 3 means draw,1 and 2 means player
    int IsWin()
    {
        for (int i = 0; i < 3; i++)
        {
            if (blocks[i, 0] != 0 && blocks[i, 0] == blocks[i, 1] && blocks[i, 1] == blocks[i, 2])
            {
                return blocks[i, 0];
            }
        }
        for (int i = 0; i < 3; i++)
        {
            if (blocks[0, i] != 0 && blocks[0, i] == blocks[1, i] && blocks[1, i] == blocks[2, i])
            {
                return blocks[0, i];
            }
        }
        if (blocks[1, 1] != 0 && ((blocks[0, 0] == blocks[1, 1] && blocks[1, 1] == blocks[2, 2]) || (blocks[0, 2] == blocks[1, 1] && blocks[2, 0] == blocks[1, 1])))
        {
            return blocks[1, 1];
        }
        for (int i = 0; i < 3; i++)

        { for (int j = 0; j < 3; j++) if(blocks[i, j] == 0) return 0; }
            
        
        return 3;
    }
     void Reset()
    {
        player = 1;
        for(int i=0;i<3;i++)
        {
            for(int j=0;j<3;j++)
            {
                blocks[i,j] = 0;
            }
        }
    }
    private void OnGUI()
    {
        if (GUI.Button(new Rect(420, 300, 100, 50), "reset"))
            Reset();
        int result = IsWin();
        if(result == 1)
        {
            GUI.Label(new Rect(420, 350, 100, 50), "dog win");
        }
        else if(result == 2)
        {
            GUI.Label(new Rect(420, 350, 100, 50), "cat win");
        }
        else if(result == 3)
        {
            GUI.Label(new Rect(450, 350, 100, 50), "draw");
        }
        for(int i=0;i<3;i++)
        {
            for(int j=0;j<3;j++)
            {
                
                if (blocks[i, j] == 1) GUI.Button(new Rect(400 + i * 50, 100 + j * 50, 50, 50), player1_icon);
                if (blocks[i, j] == 2) GUI.Button(new Rect(400 + i * 50, 100 + j * 50, 50, 50), player2_icon);
                if(GUI.Button(new Rect(400 + i * 50, 100 + j * 50, 50, 50), ""))
                {
                    if(result == 0)
                    {
                        blocks[i, j] = player;
                        if (player == 1) player = 2;
                        else player = 1;
                    }
                }
            }
        }
    }
}
