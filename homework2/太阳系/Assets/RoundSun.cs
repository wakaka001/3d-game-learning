using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSun : MonoBehaviour {

    // Use this for initialization
    public Transform Sun;
    public Transform Earth;
    public Transform Mars;
    public Transform shuixing;
    public Transform jinxing;
    public Transform muxing;
    public Transform tuxing;
    public Transform tianwangxing;
    public Transform haiwangxing;
    Vector3[] arr = new Vector3[10];

    void Start () {
        for(int i=0;i<10;i++)
        {
            arr[i] = roll();
        }
        


	}
	
	// Update is called once per frame
	void Update () {
        Sun.Rotate(Vector3.up * 10 * Time.deltaTime);
        Earth.RotateAround(Sun.position, arr[0], 90 * Time.deltaTime);
        Earth.Rotate(Vector3.up * 10 * Time.deltaTime);
        shuixing.RotateAround(Sun.position, arr[1], 100 * Time.deltaTime);
        shuixing.Rotate(Vector3.up * 10 * Time.deltaTime);
        jinxing.RotateAround(Sun.position, arr[2], 80 * Time.deltaTime);
        jinxing.Rotate(Vector3.up * 10 * Time.deltaTime);
        Mars.RotateAround(Sun.position, arr[3], 75 * Time.deltaTime);
        Mars.Rotate(Vector3.up * 10 * Time.deltaTime);
        muxing.RotateAround(Sun.position, arr[4], 20 * Time.deltaTime);
        muxing.Rotate(Vector3.up * 10 * Time.deltaTime);
        tuxing.RotateAround(Sun.position, arr[5], 13 * Time.deltaTime);
        tuxing.Rotate(Vector3.up * 10 * Time.deltaTime);
        tianwangxing.RotateAround(Sun.position, arr[6], 15 * Time.deltaTime);
        tianwangxing.Rotate(Vector3.up * 10 * Time.deltaTime);
        haiwangxing.RotateAround(Sun.position, arr[7], 5 * Time.deltaTime);
        haiwangxing.Rotate(Vector3.up * 10 * Time.deltaTime);
    }
    Vector3 roll()
    {
        float rx, ry;
        rx = Random.Range(10, 60);
        ry = Random.Range(10, 60);
        return new Vector3(rx, ry, 0);
    }
}
