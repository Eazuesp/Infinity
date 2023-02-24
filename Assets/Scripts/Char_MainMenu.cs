using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_MainMenu : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, 2);
        switch (rand) 
        {
            case 0:
                //gameObject.transform.Translate(new Vector3(.4f, 0, 0));
                gameObject.GetComponent<Animator>().SetTrigger("Sleep");
                break;
            case 1:                
                gameObject.GetComponent<Animator>().SetTrigger("Sit");
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
