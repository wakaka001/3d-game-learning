using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{

    private Button yourButton;
    public Text text;
    private int frame = 20;

    // Use this for initialization  
    void Start()
    {
        Button btn = this.gameObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    IEnumerator rotateIn()
    {
        float x = 0;
        float y = 120;
        for (int i = 0; i < frame; i++)
        {
            x -= 90f / frame;
            y -= 120f / frame;
            text.transform.rotation = Quaternion.Euler(x, 0, 0);
            text.rectTransform.sizeDelta = new Vector2(text.rectTransform.sizeDelta.x, y);
            if (i == frame - 1)
            {
                text.gameObject.SetActive(false);
            }
            yield return null;
        }
    }

    IEnumerator rotateOut()
    {
        float x = -90;
        float y = 0;
        for (int i = 0; i < frame; i++)
        {
            x += 90f / frame;
            y += 120f / frame;
            text.transform.rotation = Quaternion.Euler(x, 0, 0);
            text.rectTransform.sizeDelta = new Vector2(text.rectTransform.sizeDelta.x, y);
            if (i == 0)
            {
                text.gameObject.SetActive(true);
            }
            yield return null;
        }
    }


    void TaskOnClick()
    {
        if (text.gameObject.activeSelf)
        {
            StartCoroutine(rotateIn());
        }
        else
        {
            StartCoroutine(rotateOut());
        }

    }
}