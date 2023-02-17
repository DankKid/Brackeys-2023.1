using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private float lurchConstant;
    [SerializeField] private float walkSpeed;

    // lurch forward, move forward in bursts

    // damage indicator could be slowly fading placeable to black based off of percent health

    private PlaceableGrid grid;

    // 1-5
    public void SetY(int y)
    {
        float rowY = grid.GetCellCenter(new Vector2Int(0, y)).y;

        Vector2 position = transform.position;
        position.y = rowY;
        transform.position = position;
    }

    private void Awake()
    {
        grid = FindObjectOfType<PlaceableGrid>();

        SetY(1);
    }

    private void Update()
    {
        Vector3 position = transform.position;
        position.x -= Time.deltaTime * (walkSpeed + Mathf.Pow(transform.position.x % 1f, 1f / lurchConstant));
        transform.position = position;
    }
}
