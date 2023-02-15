using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Magnet : MonoBehaviour {

	public enum CollectibleTypes {NoType, Type1, Type2, Type3, Type4, Type5}; // you can replace this with your own labels for the types of collectibles in your game!

	public CollectibleTypes CollectibleType; // this gameObject's type

	public bool rotate; // do you want it to rotate?

	public float rotationSpeed;

	public AudioClip collectSound;

	public GameObject collectEffect;


	//public GameObject player;
	//public GameObject coinDetectorObj;
    public CoinDetector coinDetectorSrp;


    // Use this for initialization
    void Start () {
		// not work if inactive
		//coinDetectorObj = GameObject.FindGameObjectWithTag("Coin Detector").GetComponent<CoinDetector>().gameObject;
		//coinDetectorSrp = GameObject.FindGameObjectWithTag("Coin Detector").GetComponent<CoinDetector>();

		// work even detector inactive
		//player = GameObject.FindGameObjectWithTag("Player");
		//coinDetectorObj = player.transform.Find("CoinDetector").gameObject;
		//coinDetectorSrp = coinDetectorObj.GetComponent<CoinDetector>();

		//coinDetectorObj = GameObject.FindObjectOfType<CoinDetector>(true).gameObject;
		//coinDetectorSrp = GameObject.FindObjectOfType<CoinDetector>(true);

		//coinDetectorObj.SetActive(true);
        //coinDetectorObj.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        if (rotate)
			transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") {
            Collect ();
			//StartCoroutine(gameManager.ActivateCoin());
            //StartCoroutine(coinDetectorSrp.ActivateCoin());
            //coinDetectorSrp.ActivateCoinDetector();

        }
	}




        public void Collect()
	{
		if(collectSound)
			AudioSource.PlayClipAtPoint(collectSound, transform.position);
		if(collectEffect)
			Instantiate(collectEffect, transform.position, Quaternion.identity);

		//Below is space to add in your code for what happens based on the collectible type

		if (CollectibleType == CollectibleTypes.NoType) {

			//Add in code here;

			//Debug.Log ("Do NoType Command");
		}
		if (CollectibleType == CollectibleTypes.Type1) {

			//Add in code here;

			//Debug.Log ("Do NoType Command");
		}
		if (CollectibleType == CollectibleTypes.Type2) {

			//Add in code here;

			//Debug.Log ("Do NoType Command");
		}
		if (CollectibleType == CollectibleTypes.Type3) {

			//Add in code here;

			//Debug.Log ("Do NoType Command");
		}
		if (CollectibleType == CollectibleTypes.Type4) {

			//Add in code here;

			//Debug.Log ("Do NoType Command");
		}
		if (CollectibleType == CollectibleTypes.Type5) {

			//Add in code here;

			//Debug.Log ("Do NoType Command");
		}

		Destroy (gameObject);

	}
}
