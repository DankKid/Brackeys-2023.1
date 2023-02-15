using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private bool randomizeTumbleDirection;
    [SerializeField] private Vector2 randomTumbleRateRangeDegreesPerSecond;

    private Rigidbody2D rb;
    private Placeable instantiator;

    public void SetInstantiator(Placeable instantiator)
    {
        this.instantiator = instantiator;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.velocity = new Vector2(velocity, 0);
        float tumbleDirection = 1f;
        if (randomizeTumbleDirection)
        {
            bool isReversed = Random.value > 0.5f;
            tumbleDirection = isReversed ? -1f : 1f;
        }
        rb.angularVelocity = Random.Range(randomTumbleRateRangeDegreesPerSecond.x, randomTumbleRateRangeDegreesPerSecond.y) * tumbleDirection;
    }

    private void Update()
    {
        if (transform.position.x > 32)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        Placeable placeable = collision.gameObject.GetComponentInParent<Placeable>();
        if (placeable == null || placeable == instantiator)
        {
            return;
        }
        Destroy(gameObject);
        */
    }
}
