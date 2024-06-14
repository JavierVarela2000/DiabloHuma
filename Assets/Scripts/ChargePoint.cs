using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargePoint : MonoBehaviour
{
    public string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var playerObject = other.GetComponent<Player>();
            if(!playerObject.dashCharge)
                playerObject.canCharge = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var playerObject = other.GetComponent<Player>();
            if (!playerObject.isHanging)
                playerObject.canCharge = false;
        }
    }
}
