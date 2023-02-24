using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : MonoBehaviour
{
    public RectTransform rectTransform;
    public GameObject infoMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = rectTransform.anchoredPosition.x;
        float y = rectTransform.anchoredPosition.y;
        infoMenu.GetComponent<RectTransform>().offsetMin = new Vector2(-x * .1f, -y);//left-bottom
        infoMenu.GetComponent<RectTransform>().offsetMax = new Vector2(x * .1f, -y * 1.1f);//right-top (become neg)
        infoMenu.transform.position = new Vector2(x * 1.7f, y * 1.75f);
    }
}
