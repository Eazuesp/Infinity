using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class TestCharController : MonoBehaviour
{

    public float movementSpeed;
    public SpawnManager spawnManager;
    public float rotationSpeed;
    public float jumpSpeed;
    public CharacterController characterController;
    private float ySpeed;
    public Animator animator = new Animator();
    // public Rigidbody myRigidbody;
    // private bool isJump = false;

    // private bool run = false;

    // buff
    public float speeding = 0;
    public float bubbled = 0;
    public GameObject bubble;
    public CoinDetector coinDetectorSrp;


    // call coin collect
    public GameManager gameManager;

    // particle effect
    public ParticleSystem dirtParticles;
    public ParticleSystem landingParticles;
    public ParticleSystem enemyHitParticle;

    // Bear Material
    public Material opaqueMat;
    public Material transparentMat;

    public PostProcessVolume postProcessVolume;
    private ChromaticAberration chromaticAberration;
    private LensDistortion lensDistortion;
    private Vignette vignette;
    private float ldIntensity = 0;

    private PlayerRagdoll playerRagdoll;

    public AudioClip jumpSound;
    public AudioClip hitSound;

    // Start is called before the first frame update
    void Start()
    {
        // characterController= GetComponent<CharacterController>();   
        //coinDetectorSrp = GameObject.FindGameObjectWithTag("Coin Detector").GetComponent<CoinDetector>();
        coinDetectorSrp = GameObject.FindObjectOfType<CoinDetector>();
        playerRagdoll = GetComponent<PlayerRagdoll>();

        postProcessVolume.profile.TryGetSettings(out chromaticAberration);
        postProcessVolume.profile.TryGetSettings(out lensDistortion);
        postProcessVolume.profile.TryGetSettings(out vignette);
        //chromaticAberration = postProcessVolume.GetComponent<ChromaticAberration>();
        //lensDistortion = postProcessVolume.GetComponent<LensDistortion>();
        //vignette = postProcessVolume.GetComponent<Vignette>();
    }

    // Update is called once per frame
    void Update()
    {
        float hMovement = Input.GetAxis("Horizontal") * movementSpeed / 1.5f;
        // forward
        float vMovement = Input.GetAxis("Vertical") * movementSpeed;
        Vector3 movementDirection = new Vector3(hMovement, 0, vMovement);
        Vector3 velocity = movementDirection;
        

        //ySpeed += Physics.gravity.y * Time.deltaTime;

        // run forward

        //if (movementDirection != Vector3.zero)
        //{
        //    if (!isJump)
        //    {
        //        ResetAllBool();
        //        animator.SetBool("Run Forward", true);
        //    }
        //    run = true;
        //}
        //else
        //{
        //    if (!isJump)
        //    {
        //        ResetAllBool();
        //        animator.SetBool("Idle", true);
        //    }
        //    run = false;
        //}

        //if (characterController.isGrounded)
        //{
        //    // resotre movement and jump status after hit the ground
        //    if (isJump)
        //    {
        //        if (run == true)
        //        {
        //            animator.SetBool("Run Forward", true);
        //        }
        //        isJump = false;
        //    }
        //    else
        //    {
        //        // if jump buttom is pressed
        //        ySpeed = -0.5f;
        //        if (Input.GetButtonDown("Jump"))
        //        {
        //            // myRigidbody.velocity = Vector3.up * 7;
        //            ySpeed = jumpSpeed;
        //            ResetAllBool();
        //            animator.SetBool("Jump", true);
        //            isJump = true;
        //        }
        //    }

        //}
        //Debug.Log(ySpeed);
        if (characterController.isGrounded)
        {
            if (ySpeed < -.7) { ySpeed = 0;
                
                landingParticles.Play();
            }
            if (!dirtParticles.isPlaying)
            {
                dirtParticles.Play();
            }
            if (movementDirection != Vector3.zero)
            {
                ResetAllBool();
                animator.SetBool("Run Forward", true);
            } else
            {
               ResetAllBool();
                animator.SetBool("Idle", true);
            }
            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpSpeed;
                if (jumpSound)
                    AudioSource.PlayClipAtPoint(jumpSound, transform.position);
            }
        }
        else
        {
            dirtParticles.Pause();
            ySpeed += Physics.gravity.y * Time.deltaTime;
            //animator.GetAnimatorTransitionInfo(0).IsName("Jump")
            ResetAllBool();
            animator.SetBool("Jump", true);
        }

        //if (hMovement > 0 || vMovement > 0)
        //{
        //    animationTest.animator.SetBool("Run Forward", true);
        //} else
        //{
        //    animationTest.ResetAllBool();
        //    animationTest.animator.SetBool("Idle", true);
        //}

        // transform.Translate(new Vector3(hMovement, 0, vMovement) * Time.deltaTime);
        // https://www.youtube.com/watch?v=BJzYGsMcy8Q

        // transform.Translate(velocity);

        velocity.y = ySpeed;
        characterController.Move(velocity * Time.deltaTime);
        //gameObject.transform.forward = (velocity* Time.deltaTime);


        // characterController.gameObject.GetComponent<Collider>().enabled = false;

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            // transform.forward = movementDirection;
        }
        UpdateBuff();
    }

    public void ResetAllBool()
    {
        animator.SetBool("Run Forward", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Jump", false);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

    }
    void OnCollisionEnter(Collision hit)
    {

        //hit.collider.enabled = false;
        //Rigidbody rigidbody = hit.collider.attachedRigidbody;
        //if (rigidbody != null)
        //{
        //    Debug.Log("hit" + rigidbody.name);
        //}

        if (hit.gameObject.tag == "Enemy" && bubbled <= 0 && !hit.gameObject.GetComponent<Enemy>().defeated)
        {
            if (hitSound)
                AudioSource.PlayClipAtPoint(hitSound, transform.position);
            //gameObject.GetComponent<Rigidbody>().isKinematic = true;
            //movementSpeed = 0;
            hit.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            //hit.gameObject.GetComponent<Enemy>().search = false;
            GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject emeny in enemys)
            {
                emeny.GetComponent<Enemy>().search = false;
                emeny.GetComponent<Enemy>().currentState = SlimeAnimationState.Idle;
                emeny.GetComponent<Enemy>().animator.speed = 1;
            }
            //hit.gameObject.GetComponent<Enemy>().currentState = SlimeAnimationState.Idle;
            Instantiate(enemyHitParticle, this.transform.position, hit.transform.rotation).Play();

            playerRagdoll.SetRagdollState(true);

            StartCoroutine(WaitAndRestart(0.5f));
            //SceneManager.LoadScene("Level001");

        }
        //if (characterController.gameObject.layer != collision.gameObject.layer)
        //{
        //    Physics.IgnoreLayerCollision(characterController.gameObject.layer, collision.gameObject.layer, true);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(transform.rotation.y);
        
        if (other.tag == "Coin")
        {
            gameManager.CoinCollected();
        }
        if (other.tag == "Magnet")
        {
            coinDetectorSrp.getMagnet();
            //StartCoroutine(coinDetectorSrp.ActivateCoin());
        }
        if (other.tag == "Honey")
        {
            bubbled = coinDetectorSrp.buffTime;
        }
        if (other.tag == "Speed")
        {
            speeding = coinDetectorSrp.buffTime;
        }
        if (other.tag == "SpawnTrigger")
        {
            if (-.5 < transform.rotation.y && transform.rotation.y < .5)
            {

                if (!other.GetComponent<SpawnTrigger>().isEntered)
                {
                    spawnManager.SpawnTriggerEntered();
                }
                other.GetComponent<SpawnTrigger>().isEntered = true;
            }
            else
            {
                other.isTrigger = false;
                //other.gameObject.GetComponent<Collider>().enabled = false;
            }
        }

    }

    private void UpdateBuff()
    {
        Renderer rend = gameObject.GetComponentInChildren<Renderer>();
        if (speeding > 0)
        {
            movementSpeed = 15f;
            //if (characterController.gameObject.layer != hit.gameObject.layer)
            {
                rend.material = transparentMat;
                Color newColor = rend.material.color;
                //gameObject.GetComponentInChildren<Renderer>();
                newColor.a = 0.5f;
                rend.material.color = newColor;

                chromaticAberration.active = true;
                lensDistortion.active = true;
                vignette.active = true;

                if (lensDistortion.intensity > -50)
                {
                    lensDistortion.intensity.value -= Time.deltaTime * 5;
                }

                Physics.IgnoreLayerCollision(characterController.gameObject.layer, 1, true);

            }
            speeding -= Time.deltaTime;
            //return;
        }
        else
        {
            movementSpeed = 12f;
            rend.material = opaqueMat;
            Color newColor = rend.material.color;
            //gameObject.GetComponentInChildren<Renderer>();
            newColor.a = 1f;
            rend.material.color = newColor;
            chromaticAberration.active = false;
            vignette.active = false;
            if (lensDistortion.intensity < 0)
            {
                lensDistortion.intensity.value += Time.deltaTime * 5;
            }
            else
            {
            lensDistortion.active = false;
            }
            Physics.IgnoreLayerCollision(characterController.gameObject.layer, 1, false);
        }
        if (bubbled > 0)
        {
            bubble.SetActive(true);
            bubbled -= Time.deltaTime;
        }
        else 
        {
            bubble.SetActive(false); 
        }
    }
    private IEnumerator WaitAndRestart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //SceneManager.LoadScene("Level001");
        gameManager.GameOver();
    }

    // Char Animation

    //void OnLand(Vector3 _v)
    //{
    //    Debug.Log("on land");
    //}
    //void OnJump(Vector3 _v)
    //{
    //    Debug.Log("Jump");
    //}
}
