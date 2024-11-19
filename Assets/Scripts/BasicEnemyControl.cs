using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyControl : MonoBehaviour
{
    [Header("Movement")]
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
    [Header("Other")]
    public bool canSplit;
    public Splitter splitterScript;

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody>();
        if(canSplit == true)
        {
            splitterScript = GetComponent<Splitter>();
        }
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
            if(canSplit == true)
            {
                splitterScript.SplitIntoTwoThings();
            }
        }
    }
}
