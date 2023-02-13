using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    [SerializeField] private float normalScale;
    [SerializeField] private float hoverScale;
    [SerializeField] private float draggingScale;
    [SerializeField] private float dragBox;

    private Grid grid;
    private PlaceableManager placeableManager;

    private Vector2 defaultPosition;

    public bool IsDragging { get; private set; } = false;
    private Vector2 dragOffset = Vector2.zero;

    public bool IsPlaced { get; private set; } = false;
    public Vector2Int? CellPosition { get; private set; } = null;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
        placeableManager = FindObjectOfType<PlaceableManager>();

        defaultPosition = transform.position;
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
        float halfDragBox = dragBox / 2f;
        Vector2 diff = transform.position - mousePositionWorld;
        return Mathf.Abs(diff.x) <= halfDragBox && Mathf.Abs(diff.y) <= halfDragBox;
    }
}
