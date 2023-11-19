using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerLogic : MonoBehaviour
   
    
{
    // Start is called before the first frame update
    [Header("Player Settings")]
    private Rigidbody rb;
    public float walkspeed, runspeed, jumppower, fallspeed, airMultiplier;
    public Transform PlayerOrientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    bool grounded = true;
    bool aerialboost = true;
    bool TPSMode = true;
    bool AimMode = false;
    public Animator anim;
    public float HitPoints = 100f;
    public CameraLogic camlogic;
 
    [Header("SFX")]
    public AudioClip ShootAudio;
    public AudioClip StepAudio;
    public AudioClip DeathAudio;
    AudioSource PlayerAudio;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        PlayerAudio = this.GetComponent<AudioSource>();
        // PlayerOrientation = this.GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
        AimModeAdjuster();
        ShootLogic();
        if (Input.GetKeyDown(KeyCode.F))
        {
              PlayerGetHit(100f);
              anim.SetBool("Dead", true);
        }
    }
    private void Movement()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        moveDirection = PlayerOrientation.forward * verticalInput + PlayerOrientation.right * horizontalInput;
       if (grounded && moveDirection != Vector3.zero)
       {
            if (Input.GetKey(KeyCode.LeftShift)) 
            {
                anim.SetBool("Run", true);
                anim.SetBool("Walk", false);
                rb.AddForce(moveDirection.normalized * runspeed * 10f, ForceMode.Force);
            }
            else
            {
                anim.SetBool("Run", false);
                anim.SetBool("Walk", true);
                rb.AddForce(moveDirection.normalized * walkspeed * 10f, ForceMode.Force);
            
            }
    }
    else{
            anim.SetBool("Run", false);
            anim.SetBool("Walk", false);
    }
    }

    public void step()
    {
        Debug.Log("Step");
        PlayerAudio.clip = StepAudio;
        PlayerAudio.Play();
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
           rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
           rb.AddForce(transform.up* jumppower, ForceMode.Impulse);
           grounded = false;
           anim.SetBool("Jump", true);
            // jumping = true;
        }
        else if (!grounded)
        {
            rb.AddForce(Vector3.down * fallspeed * rb.mass, ForceMode.Force);
            if(aerialboost)
            {
                rb.AddForce(moveDirection.normalized * walkspeed * 10f * airMultiplier, ForceMode.Impulse);
                aerialboost = false;
            }
        }

    }

    public void AimModeAdjuster()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if(AimMode)
            {
                TPSMode = true;
                AimMode = false;
                anim.SetBool("AimMode", false);
            }
            else if(TPSMode)
            {
                TPSMode = false;
                AimMode = true;
                anim.SetBool("AimMode", true);
            }
            camlogic.CameraModeChanger(TPSMode, AimMode);
        }
    }

    private void ShootLogic()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            PlayerAudio.clip = ShootAudio;
            PlayerAudio.Play();

           if(moveDirection.normalized != Vector3.zero)
            {
                anim.SetBool("WalkShoot", true);
                anim.SetBool("IdleShoot", false);
            }
            else
            {
                anim.SetBool("IdleShoot", true);    
                anim.SetBool("WalkShoot", false);
            }

        }
        else
        {
            
                anim.SetBool("WalkShoot", false);
                anim.SetBool("IdleShoot", false);
        }
    }

    public void PlayerGetHit(float damage)
    {
        
        Debug.Log("Player Received Damage - " + damage);
        HitPoints -= damage;
        
        Debug.Log("Wadaw");
        if(HitPoints <= 0)
        {
            PlayerAudio.clip = DeathAudio;
            PlayerAudio.Play();
            Debug.Log("Player Died");
            anim.SetBool("Dead", true);
            
        }
        else{
            anim.SetTrigger("GetHit");
        }
    }

    public void groundedchanger()
    {
        grounded = true;
        aerialboost = true;
        anim.SetBool("Jump", false);
    }
}
