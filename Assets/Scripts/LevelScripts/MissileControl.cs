using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileControl : MonoBehaviour
{
    public float moveSpeed;
    public string bulletType;
    public float timeBeforeDeathinSeconds;
    public GameObject missileExplosion;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BasicEnemy"))
        {
            Explode();
        }
    }
    void Explode()
    {
        Instantiate(missileExplosion, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }
  }