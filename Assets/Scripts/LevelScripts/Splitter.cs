using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splitter : MonoBehaviour
{
    public GameObject thingToSplitInto;

    public void SplitIntoTwoThings()
    {
       
        Instantiate(thingToSplitInto, gameObject.transform.position, gameObject.transform.rotation);
        Instantiate(thingToSplitInto, gameObject.transform.position, gameObject.transform.rotation);
    }
}
