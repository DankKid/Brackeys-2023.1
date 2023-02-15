using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float velocity;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.velocity = new Vector2(velocity, 0);
    }

    private void Update()
    {
        if (transform.position.x > 32)
        {
            Destroy(gameObject);
        }
    }
}
