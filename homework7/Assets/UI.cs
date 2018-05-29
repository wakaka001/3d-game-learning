using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour {

    // Use this for initialization
    //GameObject obj;
    //ParticleSystem bling;
    ParticleSystem[] frame;
    int flag;
    void Start () {
        //bling = GetComponent<ParticleSystem>();
        frame = GetComponentsInChildren<ParticleSystem>();
        flag = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("d"))
            transform.position += new Vector3(0.1f, 0, 0);
        if (Input.GetKey("a"))
            transform.position -= new Vector3(0.1f, 0, 0);
        if (Input.GetKey("w"))
            transform.position += new Vector3(0, 0, 0.1f);
        if (Input.GetKey("s"))
            transform.position += new Vector3(0, 0, -0.1f);
        if (transform.position.x > 7) flag = 1;
        else flag = 0;
        switchColor();
    }
    void switchColor()
    {
        //bling.startColor = new Color(0, 0, 255);
        foreach(ParticleSystem item in frame)
        {
            
            if(flag == 0) item.startColor = new Color(213,236,0);
            else if(flag == 1) item.startColor = new Color(0, 234, 81);
        }
    }
}
