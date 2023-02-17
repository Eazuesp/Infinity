using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (characterController.gameObject.layer != other.gameObject.layer)
        {
            Physics.IgnoreLayerCollision(characterController.gameObject.layer, other.gameObject.layer, true);
        }
    }

}
