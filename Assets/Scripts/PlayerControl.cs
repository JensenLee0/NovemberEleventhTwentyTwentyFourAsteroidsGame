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

    [Header("Shooting")]
    public GameObject basicProjectilePrefab;
    public GameObject basicProjectileSpawnPoint;
    public float timeBetweenShootingBasicProjectileInSeconds;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");

        if(Input.GetKeyDown(shootBasicProjectileKeyCode))
        {
            StartCoroutine(ShootBasicProjectile());
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up * turnSpeed * horizontalAxis);
        playerRb.AddForce(Vector3.forward * moveSpeed * verticalAxis);
    }
    IEnumerator ShootBasicProjectile()
    {
        yield return new WaitForSeconds(timeBetweenShootingBasicProjectileInSeconds); 
        Instantiate(basicProjectilePrefab, basicProjectileSpawnPoint.transform.position, gameObject.transform.rotation);
        
    }
}
