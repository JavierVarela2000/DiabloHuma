using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

public class SwingPoint : MonoBehaviour
{
    public string playerTag = "Player";

    private CircleCollider2D circleCollider;
    private LineRenderer lineRenderer;
    private int segments = 50;
    private bool renderLine = false;

    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.loop = true;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount = segments + 1;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.blue;
        lineRenderer.endColor = Color.blue;
        lineRenderer.enabled = false; // Initially disable the LineRenderer
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            renderLine = false;

        if (renderLine)
        {
            DrawCircle();
            lineRenderer.enabled = true; // Enable the LineRenderer when player is inside
        }
        else
        {
            lineRenderer.enabled = false; // Disable the LineRenderer when player is outside
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var playerObject = other.GetComponent<Player>(); 
            playerObject.changeSwingPosition(this.transform.position);
            playerObject.canHang = true;

            renderLine = true;
            
           
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var playerObject = other.GetComponent<Player>();
            if (!playerObject.isHanging) { 
                playerObject.canHang = false;   
            }
            renderLine = false;
        }
    }

    private void DrawCircle()
    {
        float angle = 0f;
        float x;
        float y;
        float z = 0f;
        float radius = circleCollider.radius;

        for (int i = 0; i < segments + 1; i++)
        {
            x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            y = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;

            lineRenderer.SetPosition(i, new Vector3(x, y, z));

            angle += 360f / segments;
        }
    }
}
