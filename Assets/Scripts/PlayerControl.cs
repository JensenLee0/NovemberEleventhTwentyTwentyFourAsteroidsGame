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
    public KeyCode shootBasicProjectileKeyCode;

    [Header("Connections")]
    public Rigidbody playerRb;
    public GameManager gm;

    [Header("Shooting")]
    public GameObject basicProjectilePrefab;
    public GameObject basicProjectileSpawnPoint;
    public float timeBetweenShootingBasicProjectileInSeconds;
    public bool canShootBasicProjectile;


    void Start()
    {
        //Get the various components required for the scripts to work
        playerRb = GetComponent<Rigidbody>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        //Do this upon game start
        canShootBasicProjectile = true;

    }
    void Update()
    {
        //Ensure axis and therefore movement are functioning based off of player inputs correctly
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");

        //Shoot a basic projectile when the specific keycode is pressed
        if(Input.GetKey(shootBasicProjectileKeyCode))
        {
            StartCoroutine(ShootBasicProjectile());
        }
    }

    private void FixedUpdate()
    {
        //Rotate based off of Horizontal Axis
        transform.Rotate(Vector3.up * turnSpeed * horizontalAxis);
        //Move based off of Vertical Axis
        playerRb.AddRelativeForce(Vector3.forward * moveSpeed * verticalAxis);
    }
    IEnumerator ShootBasicProjectile()
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("BasicEnemy"))
        {
            gm.playerCurrentLifeCount = gm.playerCurrentLifeCount - 1;
        }
    }
}
