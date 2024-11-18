using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyControl : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody myRb;

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void FixedUpdate()
    {
        myRb.AddRelativeForce(Vector3.forward * moveSpeed, ForceMode.Force);
    }
}
