using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionControl : MonoBehaviour
{
    public ParticleSystem explosionParticles;
    // Start is called before the first frame update
    void Start()
    {
        explosionParticles.Play();
        Destroy(gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
