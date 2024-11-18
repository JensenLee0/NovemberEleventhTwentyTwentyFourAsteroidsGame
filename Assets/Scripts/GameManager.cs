using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int playerCurrentLifeCount;
    public int playerMaxLifeCount;
    // Start is called before the first frame update
    void Start()
    {
        playerCurrentLifeCount = playerMaxLifeCount;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
