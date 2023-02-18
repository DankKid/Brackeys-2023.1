using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private float lurchConstant;
    [SerializeField] private float walkConstant;
    [SerializeField] private float speed;
    [SerializeField] private float movementCooldown;

    private PlaceableGrid grid;
    private float timeUntilMovement = 0;

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

    private void Awake()
    {
        grid = FindObjectOfType<PlaceableGrid>();

        SetPosition(10f, 1);
    }

    private void Update()
    {
        if (timeUntilMovement <= 0)
        {
            Vector3 position = transform.position;
            position.x -= Time.deltaTime * speed * (walkConstant + Mathf.Pow((transform.position.x + 1024f) % 1f, 1f / lurchConstant));
            transform.position = position;
        }

        timeUntilMovement -= Time.deltaTime;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Placeable placeable = collision.gameObject.GetComponentInParent<Placeable>();
        if (placeable == null)
        {
            return;
        }

        timeUntilMovement = movementCooldown;
    }
}
