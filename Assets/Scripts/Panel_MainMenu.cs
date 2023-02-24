using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Panel_MainMenu : MonoBehaviour
{
    public RectTransform rectTransform;
    public GameObject mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = rectTransform.anchoredPosition.x;
        float y = rectTransform.anchoredPosition.y;
        mainMenu.GetComponent<RectTransform>().offsetMin = new Vector2(-x*4.5f/10, -y);//left-bottom
        mainMenu.GetComponent<RectTransform>().offsetMax = new Vector2(x*4.5f/10, y);//right-top (become neg)
        mainMenu.transform.position = new Vector2(x*4.5f/10, y);
        //SetRectTransformSize(mainMenu.GetComponent<RectTransform>(), rectTransform.anchoredPosition);

    }

    //public void SetRectTransformSize(RectTransform rt, Vector2 size)
    //{
    //    rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
    //    rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);

    //}
}
