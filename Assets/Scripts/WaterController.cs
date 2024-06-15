using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    public int points = 100; // Número de puntos en la superficie del agua
    public float width = 10f; // Ancho de la superficie del agua
    public float amplitude = 1f; // Amplitud de las ondas
    public float speed = 2f; // Velocidad de las ondas
    public float damping = 0.98f; // Amortiguación de las ondas

    private LineRenderer lineRenderer;
    private Vector3[] vertices;
    private float[] velocities;
    private float[] accelerations;

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = points;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        vertices = new Vector3[points];
        velocities = new float[points];
        accelerations = new float[points];

        for (int i = 0; i < points; i++)
        {
            float x = i * width / (points - 1);
            vertices[i] = new Vector3(x, 0, 0);
        }
    }

    void Update()
    {
        for (int i = 0; i < points; i++)
        {
            float force = -amplitude * vertices[i].y - velocities[i] * damping;
            accelerations[i] = force;
            velocities[i] += accelerations[i] * Time.deltaTime;
            vertices[i].y += velocities[i] * Time.deltaTime;
        }

        lineRenderer.SetPositions(vertices);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Aplicar fuerzas de arrastre a los objetos que entran en el agua
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Ajusta estos valores según sea necesario
            float dragForce = 5f;
            rb.velocity *= 0.5f;
            rb.AddForce(Vector2.up * dragForce, ForceMode2D.Force);
        }
    }
}
