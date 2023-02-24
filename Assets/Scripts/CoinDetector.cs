using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinDetector : MonoBehaviour
{
    //public GameObject coinDetectorObj;
    public float buffTime;
    public float timer;


    // Start is called before the first frame update
    void Start()
    {
        //coinDetectorObj = GameObject.FindGameObjectWithTag("Coin Detector");

        //coinDetectorObj = FindObjectOfType<CoinDetector>().gameObject;

        // player = GameObject.FindGameObjectWithTag("Player");
        // coinDetectorObj = player.transform.Find("CoinDetector").gameObject;

        //coinDetectorObj.tag = "Untagged";
        //coinDetectorObj.SetActive(false);
      
    }

    // Update is called once per frame
    void Update()
    {
        
        if (timer > 0)
        {
            //coinDetectorObj.tag = "Coin Detector";
            //coinDetectorObj.SetActive(true);
            timer -= Time.deltaTime;
        }
        else
        {
            //coinDetectorObj.SetActive(false);
            //coinDetectorObj.tag = "Untagged";
        }
    }

    public void getMagnet()
    {
        timer = buffTime;
    }


    //public void ActivateCoinDetector()
    //{
    //    StartCoroutine(ActivateCoin());
    //}

    void OnTriggerEnter(Collider other)
    {

    }


    //public IEnumerator ActivateCoin()
    //{
    //    Debug.Log("asd");
    //    //magnetic = true;
    //    coinDetectorObj.SetActive(true);
    //    yield return new WaitForSeconds(10f);
    //    coinDetectorObj.SetActive(false);
    //    //magnetic = false;
    //}

    public static IEnumerator WaitForUnscaledSeconds(float time)
    {
        float ttl = 0;
        while (time > ttl)
        {
            ttl += Time.unscaledDeltaTime;
            yield return null;
        }
    }
}
