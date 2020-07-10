using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    public Weapon currentWeapon;
    // Cached component refernces
    Rigidbody2D myRigidbody;
    Animator myAnimatior;
    Collider2D myCollider2D;
    float gravityScaleAtStart;
    // State
    bool isAlive = true;
    public bool canMove;

    private float nextTimeOfFire = 0;

    public GameObject shootSource;
    public int dir;

    [SerializeField] int health = 200;
    [SerializeField] float runSpeed = 3f;
    [SerializeField] float jumpSpeed = 1f;
    [SerializeField] float climbSpeed = 3f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;
    // Messages, then Methods
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimatior = GetComponent<Animator>();
        myCollider2D = GetComponent<Collider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) { return; }
        if (!isAlive) { return; }
        Run();
        FlipSprite();
        Jump();
        ClimbLadder();
        GunAnimaionStates();

        if (currentWeapon == null)
        {
            return;
        }

        if(Input.GetMouseButton(0))
        {
            if(Time.time >= nextTimeOfFire)
            {
                currentWeapon.Shoot(transform.localScale.x);
                nextTimeOfFire = Time.time + 1 / currentWeapon.fireRate;
            }

        }
    }

    private void GunAnimaionStates()
    {
        if(GetComponent<Player>().currentWeapon)
        {
            myAnimatior.SetBool("withgunIdle", true);
        }
    }
    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // value is between -1 and +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimatior.SetBool("Running", playerHasHorizontalSpeed);
        if(GetComponent<Player>().currentWeapon)
        {
            myAnimatior.SetBool("walkwithGun", playerHasHorizontalSpeed);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    public int GetHealth()
    {
        return health;
    }

    private void Die()
    { 
            isAlive = false;
            myAnimatior.SetTrigger("Die");
            GetComponent<Rigidbody2D>().velocity = deathKick;
       
    }

    private void Jump()
    {
        if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myAnimatior.SetBool("Jumping", false);
            myRigidbody.gravityScale = gravityScaleAtStart;
            return;
        }

        if(CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidbody.velocity += jumpVelocityToAdd;
        }

        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 jumpVelocity = new Vector2(myRigidbody.velocity.y, controlThrow * jumpSpeed);
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimatior.SetBool("Jumping", playerHasVerticalSpeed);
    }


    private void ClimbLadder()
    {
        if(!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            
            myRigidbody.gravityScale = gravityScaleAtStart;
            return;
        }

        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, controlThrow * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
    }


    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) >= Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
        
    }
}
