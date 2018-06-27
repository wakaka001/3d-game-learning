using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Controller : NetworkBehaviour
{
    public GameObject blackobj;
    public GameObject whiteobj;
    
    int[,] map;
    
    int count;
    public Text text;
    void Start()
    {
        map = new int[3,3];
        count = 0;
        for(int i=0;i<3;i++)
        {
            for(int j=0;j<3;j++)
            {
                map[i,j] = 0;
            }
        }
    }
    private void Reset()
    {
        Start();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.name);
                int index = hit.collider.name[8] - '0';
                Cmdputone(index);
                Debug.Log(index);

            }
        }
        if(isWin() != 0)
        {
            string temp = "winner:";
            if (isWin() == 1)
                temp += "white";
            else
                temp += "black";
            text.text = temp;
        }
    }
    [Command]
    void Cmdputone(int index)
    {
        int x=0, y=0;
        bool isWhite = false ;

        Vector3 pos = new Vector3(0, 0, 0);
        if(count % 2 == 1)
        {
            isWhite = true;
        }
        switch (index)
        {
            case 1:
                {
                    pos = new Vector3(-2, 1.5f, 2);
                    x = 0;y = 0;
                    break;
                }
            case 2:
                {
                    pos = new Vector3(0, 1.5f, 2);
                    x = 1;y = 0;
                    break;
                }
            case 3:
                {
                    pos = new Vector3(2, 1.5f, 2);
                    x = 2;y = 0;
                    break;
                }
            case 4:
                {
                    pos = new Vector3(-2, 1.5f, 0);
                    x = 0;y = 1;
                    break;
                }
            case 5:
                {
                    pos = new Vector3(0, 1.5f, 0);
                    x = 1;y = 1;
                    break;
                }
            case 6:
                {
                    pos = new Vector3(2, 1.5f, 0);
                    x = 2;y = 1;
                    break;
                }
            case 7:
                {
                    pos = new Vector3(-2, 1.5f, -2);
                    x = 0; y = 2;
                    break;
                }
            case 8:
                {
                    pos = new Vector3(0, 1.5f, -2);
                    x = 1; y = 2;
                    break;
                }
            case 9:
                {
                    pos = new Vector3(2, 1.5f, -2);
                    x = 2; y = 2;
                    break;
                }
        }
        if (map[x, y] != 0) return;
        else
        {
            if (isWhite) map[x, y] = 1;
            else map[x, y] = 2;
        }
        if (isWhite)
        {
            GameObject temp = Instantiate(whiteobj) as GameObject;
            temp.transform.position = pos;
            NetworkServer.Spawn(temp);
        }
         else
        {
            GameObject temp = Instantiate(blackobj) as GameObject;
            temp.transform.position = pos;
            NetworkServer.Spawn(temp);
        }
           
        count++;
    }


    int isWin()//1 white 2 black
    {
        for (int i = 0; i < 3; i++)
        {
            int flag = map[i,0];
            for (int j = 0; j < 3; j++)
            {
                if(flag != map[i, j])
                {
                    flag = 0;
                    break;
                }
                
            }
            if (flag != 0) return flag;
        }
        return 0;
    }
}
