using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMoveScript : MonoBehaviour
{

    Coin coinScript;

    // Start is called before the first frame update
    void Start()
    {
        coinScript = gameObject.GetComponent<Coin>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = new Vector3(coinScript.playerTransform.position.x, coinScript.playerTransform.position.y + .4f
            , coinScript.playerTransform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, playerPos ,
            coinScript.coinMoveSpeed* Time.deltaTime);

    }   
}
