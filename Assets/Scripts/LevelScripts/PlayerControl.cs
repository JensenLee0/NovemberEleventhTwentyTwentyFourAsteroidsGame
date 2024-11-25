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
    public GameObject missile;
    public GameObject[] missileSpawn;
    public float timeBetweenShootingBasicProjectileInSeconds;
    public float timeBetweenShootingMissileInSeconds;
    public bool canShootBasicProjectile;
    public bool canShootMissile;
    
    [Header("Shield")]
    public bool shieldIsActive;
    public bool shieldIsOnCooldown;
    public float shieldCooldown;
    public float shieldCharge;
    public GameObject shieldIndicator;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioSource asteroidAudioSource;
    public AudioClip shootBasicProjectileAudio;
    public AudioClip explode;
    public AudioClip asteroidExplode;
    public AudioClip echoScream;

    [Header("Other")]
    public bool playerIsAliveInPlayerControl;
    public GameObject thrusterParticles;

    void Start()
    {
        //Get the various components required for the scripts to work
        playerRb = GetComponent<Rigidbody>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        asteroidAudioSource = GameObject.Find("AsteroidAudio").GetComponent<AudioSource>();
        thrusterParticles.SetActive(false); // for when stopped
        //Start the Game
        StartGame();
    }
    void StartGame()
    {
        canShootBasicProjectile = true;
        canShootMissile = true;
        playerIsAliveInPlayerControl = true;
        shieldIsActive = false;
        shieldIsOnCooldown = false;
        shieldIndicator.gameObject.SetActive(false);
    }
    void Update()
    {
        //Ensure axis and therefore movement are functioning based off of player inputs correctly
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");

        //Shoot a basic projectile when the specific keycode is pressed
        {
            if (Input.GetButton("Shoot"))
            {
                StartCoroutine(ShootBasicProjectile());
            }
        }
        //Shoot a missile when a specific button is pressed
        {
            if (Input.GetButton("Missile"))
            {
                StartCoroutine(ShootMissile());
            }
        }
        //Activate a shield as long as a specific button is pressed
        {
            if (Input.GetButton("Shield"))
            {
                if (playerIsAliveInPlayerControl == true)
                {
                    if (shieldIsOnCooldown == false)
                    {
                        if (shieldCharge >= 1)
                        {
                            shieldIsActive = true;
                            shieldIndicator.gameObject.SetActive(true);
                        }
                        if (shieldCharge < 1)
                        {
                            shieldIsActive = false;
                            StartCoroutine(ShieldCooldown());
                            shieldIndicator.gameObject.SetActive(false);
                        }
                    }
                }
            }
            if (Input.GetButtonUp("Shield"))
            {
                shieldIsActive = false;
            }
            if (shieldCharge < 1)
            {
                shieldIsActive = false;
            }
            if (shieldIsActive == false)
            {
                shieldIndicator.gameObject.SetActive(false);
            }
        }
       if(Input.GetAxisRaw("Vertical") > 0)
        {
            thrusterParticles.SetActive(true);
        }
       else
        {
            thrusterParticles.SetActive(false);
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
                if(shieldCharge >= 250)
                {
                    shieldCharge = 250;
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
                    audioSource.PlayOneShot(shootBasicProjectileAudio, 0.25f);
                    //Ensure there is a delay between each projectile fired
                    yield return new WaitForSeconds(timeBetweenShootingBasicProjectileInSeconds);
                    canShootBasicProjectile = true;
                }
            }
        }
    }
    IEnumerator ShootMissile()
    {
        if(playerIsAliveInPlayerControl == true)
        {
            if(shieldIsActive == false)
            {
                if(canShootMissile == true)
                {
                    //select a random place to spawn the missile between two options
                    int missileSpawnIndex = Random.Range(0, missileSpawn.Length);
                    //create a missile
                    Instantiate(missile, missileSpawn[missileSpawnIndex].transform.position, missileSpawn[missileSpawnIndex].transform.rotation);
                    canShootMissile = false;
                    //go on cooldown
                    yield return new WaitForSeconds(timeBetweenShootingMissileInSeconds);
                    canShootMissile = true;
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("BasicEnemy"))
        {
            if (shieldIsActive == false && playerIsAliveInPlayerControl == true)
            {
                gm.playerCurrentLifeCount = gm.playerCurrentLifeCount - 1;
                asteroidAudioSource.PlayOneShot(asteroidExplode, 0.5f);
                Destroy(collision.gameObject);
                if (gm.playerCurrentLifeCount == 0)
                {
                    playerIsAliveInPlayerControl = false;
                    audioSource.PlayOneShot(explode, 3.0f);
                    audioSource.PlayOneShot(echoScream, 0.5f);
                    gm.GameOver();
                }
            }
        }
    }
}