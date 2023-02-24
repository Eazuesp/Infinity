using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerRagdoll : MonoBehaviour
{

    private Collider[] colliders;
    private Rigidbody[] rigidbodies;

    private Rigidbody charRigidbody;
    private Collider charCollider;
    private Animator charAnimator;
    // private AnimationControl charAnimationControl;
    // private Mover charMover;
    // private AdvanceWalkerController charWalkerController;
    private TestCharController charController;

    public GameObject ragdollRootObj;
    // Start is called before the first frame update
    void Start()
    {
        charRigidbody = GetComponent<Rigidbody>();
        charCollider = GetComponent<MeshCollider>();
        charAnimator = GetComponent<Animator>();
        // charAnimationControl = GetComponent<AnimationControl>();
        // charMover = GetComponent<Mover>();
        // charWalkerController = GetComponent<AdvancedWalkerController>();
        charController = GetComponent<TestCharController>();

        colliders = ragdollRootObj.GetComponentsInChildren<Collider>();
        rigidbodies = ragdollRootObj.GetComponentsInChildren<Rigidbody>();
        SetRagdollState(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRagdollState(bool state)
    {
        charCollider.enabled = !state;
        charAnimator.enabled = !state;
        charController.enabled = !state;
        charRigidbody.isKinematic = state;

        foreach(Collider collider in colliders)
        {
            collider.enabled = state;
        }

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = !state;
        }
    }
}
