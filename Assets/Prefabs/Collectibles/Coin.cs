using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Coin : MonoBehaviour {

	public enum CollectibleTypes {NoType, Type1, Type2, Type3, Type4, Type5}; // you can replace this with your own labels for the types of collectibles in your game!

	public CollectibleTypes CollectibleType; // this gameObject's type

	public bool rotate; // do you want it to rotate?

	public float rotationSpeed;

	public AudioClip collectSound;

    public GameObject collectEffect;

	//public Transform playerTransform;
	public GameObject player;
	public float coinMoveSpeed = 17f;
	public float reactDistance = 30f;
	public bool attr = false;

    //CoinMoveScript coinMoveScript;



	// Use this for initialization
	void Start () {
		//playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		player = GameObject.FindGameObjectWithTag("Player");
        //coinMoveScript = gameObject.GetComponent<CoinMoveScript>();

    }
	
	// Update is called once per frame
	void Update () {

		if (rotate)
			transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (player.GetComponent<CoinDetector>().timer > 0 && distance <= reactDistance)
		{
			attr = true;
		}

		if (attr)
		{
			move();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") {
			Collect ();
		}
		//if (other.gameObject.tag == ("Coin Detector"))
		//{
		//	//coinMoveScript.enabled = true;
		//	attr = true;
  //      }
	}

    public void move()
    {
        Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y + .4f
            , player.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, playerPos,
            coinMoveSpeed * Time.deltaTime);
    }

    public void Collect()
	{
		if (collectSound)
			AudioSource.PlayClipAtPoint(collectSound, transform.position);

		if (collectEffect) { 
			Instantiate(collectEffect, transform.position, Quaternion.identity);
			//Instantiate(collectEffect, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
		}


		Destroy (gameObject);
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


	}
}
