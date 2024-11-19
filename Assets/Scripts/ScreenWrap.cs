using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    [Header("Limits")]
    public float xMin = -23;
    public float xMax = 23;
    public float zMin = -13;
    public float zMax = 13;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //no passing through xMax
        if(transform.position.x > xMax)
        {
            transform.position = new Vector3(
                xMin,
                transform.position.y,
                transform.position.z    
                );
        }
        //no passing through xMin
        if (transform.position.x < xMin)
        {
            transform.position = new Vector3(
                xMax,
                transform.position.y,
                transform.position.z
                );
        }
        //no passing through zMax
        if (transform.position.z > zMax)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                zMin
                );
        }
        //no passing through zMin
        if (transform.position.z < zMin)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                zMax
                );
        }
    }
}
