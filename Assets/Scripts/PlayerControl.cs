using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Movement Variables")]
    public float turnSpeed;
    public float moveSpeed;

    [Header("Inputs")]
    public float horizontalAxis;
    public float verticalAxis;

    [Header("Connections")]
    public Rigidbody playerRb;
    public GameManager gm;

    [Header("Shooting")]
    public GameObject basicProjectilePrefab;
    public GameObject basicProjectileSpawnPoint;
    public float timeBetweenShootingBasicProjectileInSeconds;
    public bool canShootBasicProjectile;
    public bool shieldIsActive;
    public bool shieldIsOnCooldown;
    public float shieldCooldown;
    public float shieldCharge;

    [Header("Other")]
    public bool playerIsAliveInPlayerControl;

    void Start()
    {
        //Get the various components required for the scripts to work
        playerRb = GetComponent<Rigidbody>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        //Start the Game
        StartGame();
    }
    void StartGame()
    {
        canShootBasicProjectile = true;
        playerIsAliveInPlayerControl = true;
        shieldIsActive = false;
        shieldIsOnCooldown = false;
    }
    void Update()
    {
        //Ensure axis and therefore movement are functioning based off of player inputs correctly
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");

        //Shoot a basic projectile when the specific keycode is pressed
        if(Input.GetButton("Shoot"))
        {
            StartCoroutine(ShootBasicProjectile());
        }
        //Activate a shield as long as a specific button is pressed
        if(Input.GetButton("Shield"))
        {
            if (shieldIsOnCooldown == false)
            {
                if (shieldCharge >= 1)
                {
                    shieldIsActive = true;
                }
                if (shieldCharge < 1)
                {
                    shieldIsActive = false;
                    StartCoroutine(ShieldCooldown());
                }
            }
        }
        if (shieldCharge < 1)
        {
            shieldIsActive = false;
        }
    }
    IEnumerator ShieldCooldown()
    {
        shieldIsOnCooldown = true;
        yield return new WaitForSeconds(shieldCooldown);
        shieldIsOnCooldown = false;
    }
    private void FixedUpdate()
    {
        if (playerIsAliveInPlayerControl == true)
        {
            //Rotate based off of Horizontal Axis
            transform.Rotate(Vector3.up * turnSpeed * horizontalAxis * Time.deltaTime);
            //Move based off of Vertical Axis
            playerRb.AddRelativeForce(Vector3.forward * moveSpeed * verticalAxis);
            if(shieldIsActive == false)
            {
                shieldCharge = shieldCharge + 1;
                if(shieldCharge >= 100)
                {
                    shieldCharge = 100;
                }
            }
            if(shieldIsActive == true)
            {
                shieldCharge = shieldCharge - 1;
            }
        }
    }
    IEnumerator ShootBasicProjectile()
    {
        if (playerIsAliveInPlayerControl == true)
        {
            if (shieldIsActive == false)
            {
                if (canShootBasicProjectile == true)
                {
                    //Create a basic projectile
                    Instantiate(basicProjectilePrefab, basicProjectileSpawnPoint.transform.position, gameObject.transform.rotation);
                    canShootBasicProjectile = false;
                    //Ensure there is a delay between each projectile fired
                    yield return new WaitForSeconds(timeBetweenShootingBasicProjectileInSeconds);
                    canShootBasicProjectile = true;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("BasicEnemy"))
        {
            if (shieldIsActive == false)
            {
                gm.playerCurrentLifeCount = gm.playerCurrentLifeCount - 1;
                if (gm.playerCurrentLifeCount <= 0)
                {
                    playerIsAliveInPlayerControl = false;
                    gm.GameOver();
                }
            }
        }
    }
}
