using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<Player>();
            player.checkPoint = this.transform.position;
            Debug.Log("New CheckPoint" + player.checkPoint.ToString());
            Debug.Log("Posicion directa" + this.transform.position.ToString());
        }
    }
}
