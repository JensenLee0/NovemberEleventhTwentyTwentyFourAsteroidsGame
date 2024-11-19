using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyControl : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody myRb;
    private float xForce;
    private float zForce;
    private float startRotation;
    [Header("Ranges")]
    public float xMaxForce;
    public float xMinForce;
    public float zMaxForce;
    public float zMinForce;
    public float maxRotation;
    public float minRotation;

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody>();
        xForce = Random.Range(xMinForce, xMaxForce);
        zForce = Random.Range(zMinForce, zMaxForce);
        startRotation = Random.Range(minRotation, maxRotation);
        myRb.AddForce(new Vector3(xForce, 0, zForce), ForceMode.Impulse);
        transform.Rotate(0, startRotation, 0);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void FixedUpdate()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("PlayerProjectile"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
