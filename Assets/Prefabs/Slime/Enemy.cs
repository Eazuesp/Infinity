using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static EnemyAi;
//using static UnityEditor.PlayerSettings;

public class Enemy : MonoBehaviour
{

    // AI from Quick 'n Dirty Devlog
    public float movementSpeed;
    private Rigidbody enemyRb;
    private GameObject player;
    private float reactDistance = 50f;

    public GameObject coin;

    // search for player
    public bool search = true;

    public Face faces;
    public GameObject SmileBody;
    public SlimeAnimationState currentState;
    public Animator animator;
    // public NavMeshAgent agent;
    public Transform[] waypoints;
    public int damType;
    // private int m_CurrentWaypointIndex;
    private bool move;
    private Material faceMaterial;

    public bool defeated = false;

    public AudioClip hitSound;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");

        // Animation
        faceMaterial = SmileBody.GetComponent<Renderer>().materials[1];
        currentState = SlimeAnimationState.Idle;

    }

    // Update is called once per frame
    void Update()
    {
        if (search)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            Vector3 lookDirection;
            Vector3 targetPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            if (distance <= reactDistance)
            {
                if (distance > 5f)
                {
                    targetPos.z += (distance / 2f);
                }
                lookDirection = (targetPos - transform.position).normalized;
                //enemyRb.AddForce(lookDirection * movementSpeed);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(new Vector3(lookDirection.x, 0, lookDirection.z)), 700 * Time.deltaTime);
                //Quaternion.LookRotation(new Vector3(lookDirection.x, 0 ,lookDirection.z));
                currentState = SlimeAnimationState.Walk;
                //animator.SetFloat("Speed", movementSpeed);
                animator.speed = movementSpeed;
                //transform.position = Vector3.MoveTowards(transform.position, targetPos,
                //    movementSpeed);
                if (distance < 5f)
                {                    
                    enemyRb.AddForce(lookDirection * 15);
                    currentState = SlimeAnimationState.Attack;
                }
            }
            else
            {
                lookDirection = (targetPos - transform.position).normalized;
                //enemyRb.AddForce(lookDirection * movementSpeed * 0.2f);
                transform.rotation = Quaternion.LookRotation(lookDirection);
                currentState = SlimeAnimationState.Walk;
                // animator.SetFloat("Speed", movementSpeed * .2f);
                animator.speed = movementSpeed * .2f;
                //transform.position = Vector3.MoveTowards(transform.position, targetPos,
                //    movementSpeed * .2f);

            }
            if ((transform.position.z - player.transform.position.z) < -3f)
            {
                Destroy(gameObject);
            }
        }


        switch (currentState)
        {
            case SlimeAnimationState.Idle:

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) return;
                //StopAgent();
                SetFace(faces.Idleface);
                animator.SetFloat("Speed", 0);
                break;

            case SlimeAnimationState.Walk:

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")) return;
                SetFace(faces.WalkFace);
                animator.SetFloat("Speed", 5);
                //if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")) return;

                //SetFace(faces.WalkFace);
                //agent.isStopped = false;
                //agent.updateRotation = true;

                //if (walkType == WalkType.ToOrigin)
                //{
                //    agent.SetDestination(originPos);
                //    // Debug.Log("WalkToOrg");
                //    SetFace(faces.WalkFace);
                //    // agent reaches the destination
                //    if (agent.remainingDistance < agent.stoppingDistance)
                //    {
                //        walkType = WalkType.Patroll;

                //        //facing to camera
                //        transform.rotation = Quaternion.identity;

                //        currentState = SlimeAnimationState.Idle;
                //    }

                //}
                ////Patroll
                //else
                //{
                //    if (waypoints[0] == null) return;

                //    agent.SetDestination(waypoints[m_CurrentWaypointIndex].position);

                //    // agent reaches the destination
                //    if (agent.remainingDistance < agent.stoppingDistance)
                //    {
                //        currentState = SlimeAnimationState.Idle;

                //        //wait 2s before go to next destionation
                //        Invoke(nameof(WalkToNextDestination), 2f);
                //    }

                //}
                //// set Speed parameter synchronized with agent root motion moverment
                //animator.SetFloat("Speed", agent.velocity.magnitude);


                break;

            case SlimeAnimationState.Jump:

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")) return;

                //StopAgent();
                SetFace(faces.jumpFace);
                animator.SetTrigger("Jump");

                //Debug.Log("Jumping");
                break;

            case SlimeAnimationState.Attack:

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) return;
                //StopAgent();
                SetFace(faces.attackFace);
                animator.SetTrigger("Attack");

                // Debug.Log("Attacking");

                break;
            case SlimeAnimationState.Damage:

                // Do nothing when animtion is playing
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Damage0")
                     || animator.GetCurrentAnimatorStateInfo(0).IsName("Damage1")
                     || animator.GetCurrentAnimatorStateInfo(0).IsName("Damage2")) return;

                //StopAgent();
                animator.SetTrigger("Damage");
                animator.SetInteger("DamageType", damType);
                SetFace(faces.damageFace);

                //Debug.Log("Take Damage");
                break;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bubble")
        {
            if (hitSound)
            {
                AudioSource.PlayClipAtPoint(hitSound, transform.position);
            }
            search = false;
            defeated = true;
            damType = 2;
            currentState = SlimeAnimationState.Damage;
            StartCoroutine(WaitAndRestart(1f));
        }
    }
    private IEnumerator WaitAndRestart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        GameObject coinObj;
        coinObj = Instantiate(coin, new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z), coin.transform.rotation);
        coinObj.GetComponent<Coin>().attr = true;
        coinObj = Instantiate(coin, new Vector3(transform.position.x, transform.position.y, transform.position.z+2f), coin.transform.rotation);
        coinObj.GetComponent<Coin>().attr = true;
        coinObj = Instantiate(coin, new Vector3(transform.position.x, transform.position.y, transform.position.z-2f), coin.transform.rotation);
        coinObj.GetComponent<Coin>().attr = true;
        Destroy(gameObject);
    }

void SetFace(Texture tex)
{
    faceMaterial.SetTexture("_MainTex", tex);
}

    public void AlertObservers(string message)
    {

        if (message.Equals("AnimationDamageEnded"))
        {
            // When Animation ended check distance between current position and first position 
            //if it > 1 AI will back to first position 

            //float distanceOrg = Vector3.Distance(transform.position, originPos);
            //if (distanceOrg > 1f)
            //{
            //    walkType = WalkType.ToOrigin;
            //    currentState = SlimeAnimationState.Walk;
            //}
            //else currentState = SlimeAnimationState.Idle;

            ////Debug.Log("DamageAnimationEnded");
        }

        if (message.Equals("AnimationAttackEnded"))
        {
            currentState = SlimeAnimationState.Idle;
        }

        if (message.Equals("AnimationJumpEnded"))
        {
            currentState = SlimeAnimationState.Idle;
        }
    }

    void OnAnimatorMove()
    {
        // apply root motion to AI
        Vector3 position = animator.rootPosition;
        //position.y = agent.nextPosition.y;
        transform.position = new Vector3(position.x, position.y, position.z);
            //    movementSpeed * .2f);
        //transform.position = position;
        //agent.nextPosition = transform.position;
    }
}
