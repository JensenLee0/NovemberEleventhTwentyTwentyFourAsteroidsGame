using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadPowerupScript : MonoBehaviour
{
    public int amountToReload;
    public PlayerControl playerScript;

    private void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerControl>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerScript.missileAmmo += amountToReload;
            Destroy(gameObject);
        }
    }
}
