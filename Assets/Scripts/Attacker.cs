using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private int startingHealth;
    [SerializeField] private float lurchConstant;
    [SerializeField] private float walkConstant;
    [SerializeField] private float speed;
    [SerializeField] private float movementCooldown;

    private PlaceableGrid grid;
    private int health;
    private float offset;
    private Placeable target = null;
    private float attackingX = 0;
    private float attackTime = 0;
    private int hits = 0;
    private float slipTime = 0;
    private float slipEndpoint = 0;

    // https://www.desmos.com/calculator/ixjzuo7i4g

    // 1-5
    public void SetPosition(float x, int y)
    {
        float rowY = grid.GetCellCenter(new Vector2Int(0, y)).y;

        Vector2 position = transform.position;
        position.x = x;
        position.y = rowY;
        transform.position = position;
    }

    public void Damage(int damage)
    {
        health -= damage;
    }

    private void Awake()
    {
        grid = FindObjectOfType<PlaceableGrid>();

        SetPosition(12f, 1);

        health = startingHealth;
        offset = Random.Range(1000f, 10000f);
    }

    private void Update()
    {
        if (slipTime > 0)
        {
            slipTime -= Time.deltaTime;
            if (slipTime < 0)
            {
                slipTime = 0;
            }
            Vector3 position = transform.position;
            position.x = Mathf.Lerp(slipEndpoint, slipEndpoint - 5f, Mathf.Pow(slipTime, 2f));
            transform.position = position;
        }
        else
        {
            if (target == null)
            {
                Vector3 position = transform.position;
                position.x -= Time.deltaTime * speed * (walkConstant + Mathf.Pow((transform.position.x + offset) % 1f, 1f / lurchConstant));
                transform.position = position;

                attackingX = position.x;
                attackTime = 0;
                hits = 0;
            }
            else
            {
                // attack
                attackTime += Time.deltaTime;
                Vector3 position = transform.position;
                float t = (-Mathf.Pow(2 * ((attackTime % 1f) - 0.5f), 4f)) + 1f;
                position.x = Mathf.Lerp(attackingX, attackingX + 0.5f, t);
                transform.position = position;

                int expectedHits = (int)(attackTime + 1f);
                if (expectedHits > hits)
                {
                    hits++;
                    target.Damage(damage);
                }
            }
        }

        if (health <= 0)
        {
            // TODO DIE PARTICLES HERE
            FindObjectOfType<ParticlesManager>().PlayDie(transform.position);
            FindObjectOfType<PlayerManager>().getMoney(2);
            Destroy(gameObject);
        }

        if (transform.position.x < 1f)
        {
            FindObjectOfType<PlayerManager>().NextPhase();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Placeable placeable = collision.gameObject.GetComponentInParent<Placeable>();
        if (placeable == null || !placeable.IsPlaced)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Slip"))
        {
            //Push Back
            slipTime = 1;
            slipEndpoint = transform.position.x + 5f;

            placeable.Damage(100000);
            Damage(placeable.damage);
        }
        else if (collision.gameObject.CompareTag("Explode"))
        {
            placeable.Damage(100000);
            Damage(100000);
        }




        target = placeable;
    }
}
