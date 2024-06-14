using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

public class SwingPoint : MonoBehaviour
{
    public string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var playerObject = other.GetComponent<Player>(); 
            playerObject.changeSwingPosition(this.transform.position);
            playerObject.canHang = true;
           
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var playerObject = other.GetComponent<Player>();
            if (!playerObject.isHanging)
                playerObject.canHang = false;
            Debug.Log("Saliendo");
        }
    }
}
