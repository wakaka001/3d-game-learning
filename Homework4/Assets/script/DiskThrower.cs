using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Manager;


public class DiskThrower : MonoBehaviour
{

    public int round { get; set; }
    public int diskCount { get; private set; }
    private float diskScale;
    private Color diskColor;
    private Vector3 startPosition;
    private Vector3 startDiretion;
    private float diskSpeed;

    List<GameObject> usingDisks;

    public void Reset(int round)
    {
        this.round = round;
        this.diskCount = round;
        this.diskScale = 1;
        this.diskSpeed = 0.1f;
        //come from left
        if (round % 2 == 1)
        {
            this.startPosition = new Vector3(-5, 3, -15);
            this.startDiretion = new Vector3(3, 8, 5);
        }
        //come from right
        else
        {
            this.startPosition = new Vector3(5, 3, -15);
            this.startDiretion = new Vector3(-3, 8, 5);
        }
        
    }

    public void sendDisk(List<GameObject> usingDisks)
    {
        this.usingDisks = usingDisks;
        this.Reset(round);
        for (int i = 0; i < usingDisks.Count; i++)
        {
            var localScale = usingDisks[i].transform.localScale;
            usingDisks[i].transform.localScale = new Vector3(localScale.x * diskScale, localScale.y * diskScale, localScale.z * diskScale);

            usingDisks[i].GetComponent<Renderer>().material.color = GetRandomColor();
            usingDisks[i].transform.position = new Vector3(startPosition.x, startPosition.y + i, startPosition.z);
            Rigidbody rigibody;
            rigibody = usingDisks[i].GetComponent<Rigidbody>();
            rigibody.WakeUp();
            rigibody.useGravity = true;
            rigibody.AddForce(startDiretion * diskSpeed * 6 / 5, ForceMode.Impulse);
            GameScenceController.getGSController().setScenceStatus(ScenceStatus.shooting);
        }
    }
    public Color GetRandomColor()
    {
        int color = Random.Range(1, 5);
        if (color == 1) return Color.red;
        else if (color == 2) return Color.yellow;
        else if (color == 3) return Color.green;
        else if (color == 4) return Color.blue;
        else if (color == 5) return Color.black;
        return Color.white;
    }
    public void destroyDisk(GameObject disk)
    {
        disk.GetComponent<Rigidbody>().Sleep();
        disk.GetComponent<Rigidbody>().useGravity = false;
        disk.transform.position = new Vector3(0f, -99f, 0f);
    }

    public void scenceUpdate()
    {
        round++;
        Reset(round);
    }

    private void Start()
    {
        this.round = 1;
        Reset(round);
    }

    private void Update()
    {
        if (usingDisks != null)
        {
            for (int i = 0; i < usingDisks.Count; i++)
            {
                if (usingDisks[i].transform.position.y <= usingDisks[i].transform.localScale.y)
                {
                    GameScenceController.getGSController().destroyDisk(usingDisks[i]);
                    GameScenceController.getGSController().subScore();
                }
            }
            if (usingDisks.Count == 0)
            {
                GameScenceController.getGSController().setScenceStatus(ScenceStatus.waiting);
            }
        }
    }
}
