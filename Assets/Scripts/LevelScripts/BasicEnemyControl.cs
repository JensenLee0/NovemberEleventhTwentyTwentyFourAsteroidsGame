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
    public int pointsAwardedforKilling;
    public GameManager gm;
    public AudioSource audioSource;
    public AudioClip explode;

    public bool canOnlyBeDestroyedByMissiles;

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GameObject.Find("AsteroidAudio").GetComponent<AudioSource>();
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
            gm.addScore(pointsAwardedforKilling);
            audioSource.PlayOneShot(explode, 0.5f);
            Destroy(gameObject);
            if(canSplit == true)
            {
                splitterScript.SplitIntoTwoThings();
            }
        }
    }
}
