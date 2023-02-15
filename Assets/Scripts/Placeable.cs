using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    [SerializeField] private float normalScale;
    [SerializeField] private float hoverScale;
    [SerializeField] private float draggingScale;
    [SerializeField] private GameObject dragBox;
    [SerializeField] private Transform projectileSpawn;
    [SerializeField] private Transform projectilesTransform;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Vector2 wiggleRange;
    [SerializeField] private float wiggleFrequency;

    private Grid grid;
    private PlaceableManager placeableManager;

    private Vector2 defaultPosition;

    public bool IsDragging { get; private set; } = false;
    private Vector2 dragOffset = Vector2.zero;

    public bool IsPlaced { get; private set; } = false;
    public Vector2Int? CellPosition { get; private set; } = null;

    private float placementTime;
    private int projectilesShot = 0;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
        placeableManager = FindObjectOfType<PlaceableManager>();

        defaultPosition = transform.position;

        dragBox.SetActive(false);
    }

    private void Update()
    {
        if (IsPlaced)
        {
            Placed();
        }
        else
        {
            NotPlaced();
        }
    }

    private void Placed()
    {
        transform.localScale = Vector3.one * normalScale;

        float timeSincePlacement = Time.time - placementTime;

        float zRotation = Mathf.LerpAngle(wiggleRange.x, wiggleRange.y, (Mathf.Sin(timeSincePlacement * wiggleFrequency * Mathf.PI * 2f) + 1f) / 2f);
        transform.localEulerAngles = new Vector3(0, 0, zRotation);

        int expectedProjectilesShot = 1 + (int)(timeSincePlacement / wiggleFrequency);
        if (expectedProjectilesShot > projectilesShot)
        {
            Instantiate(projectilePrefab, projectileSpawn.position, Quaternion.identity, projectilesTransform);
            projectilesShot++;
        }
    }

    private void NotPlaced()
    {
        Vector2 mousePositionWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (IsHovering(mousePositionWorld) && !IsDragging)
            {
                IsDragging = true;
                // dragOffset = (Vector2)transform.position - mousePositionWorld;
            }
        }
        else if (Input.GetMouseButton(0))
        {

        }
        else
        {
            if (IsDragging)
            {
                if (grid.TryPlaceAtPosition(transform.position, this, out Vector2Int cell))
                {
                    IsPlaced = true;
                    CellPosition = cell;
                    Vector2 cellCenter = grid.GetCellCenter(cell);
                    transform.position = cellCenter;
                    placementTime = Time.time;
                }
                else
                {
                    transform.position = defaultPosition;
                }
            }
            IsDragging = false;
        }

        if (IsDragging)
        {
            transform.position = mousePositionWorld + dragOffset;
            transform.localScale = Vector3.one * draggingScale;
        }
        else if (IsHovering(mousePositionWorld))
        {
            transform.localScale = Vector3.one * hoverScale;
        }
        else
        {
            transform.localScale = Vector3.one * normalScale;
        }
    }

    private bool IsHovering(Vector3 mousePositionWorld)
    {
        mousePositionWorld -= dragBox.transform.localPosition;
        float halfDragBoxX = dragBox.transform.localScale.x / 2f;
        float halfDragBoxY = dragBox.transform.localScale.y / 2f;
        Vector2 diff = transform.position - mousePositionWorld;
        return Mathf.Abs(diff.x) <= halfDragBoxX && Mathf.Abs(diff.y) <= halfDragBoxY;
    }
}
