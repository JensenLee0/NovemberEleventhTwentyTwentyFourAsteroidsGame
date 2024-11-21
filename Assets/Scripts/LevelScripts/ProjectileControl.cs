using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileControl : MonoBehaviour
{
    public float moveSpeed;
    public string bulletType;
    public float timeBeforeDeathinSeconds;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeBeforeDeathinSeconds);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
