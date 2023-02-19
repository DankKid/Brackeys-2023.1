using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float destroyX = 16;
    [SerializeField] private float destroyTime = 4;
    [SerializeField] private float velocity;
    [SerializeField] private bool randomizeTumbleDirection;
    [SerializeField] private Vector2 randomTumbleRateRangeDegreesPerSecond;

    private Rigidbody2D rb;
    private Placeable instantiator;

    private float timeSinceInstantiation = 0;

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
        timeSinceInstantiation += Time.deltaTime;

        if (Mathf.Abs(transform.position.x) > destroyX || timeSinceInstantiation > destroyTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out Attacker attacker))
        {
            return;
        }

        attacker.Damage(instantiator.damage);
        Destroy(gameObject);
    }
}
