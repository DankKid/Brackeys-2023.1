using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private Vector2 origin;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Vector2Int size;
    [SerializeField] private Vector2 scale;
    [SerializeField] private Transform cellsTransform;

    private readonly List<(Vector2Int position, Placeable placeable)> cells = new();

    private void Start()
    {
        for (int y = 1; y <= size.y; y++)
        {
            for (int x = 1; x <= size.x; x++)
            {
                GameObject debug = Instantiate(cellPrefab, GetCellCenter(new Vector2Int(x, y)), Quaternion.identity, cellsTransform);
                debug.transform.localScale = new Vector3(scale.x, scale.y, 1);
            }
        }

        Camera.main.transform.position = new Vector3((((size.x * scale.x) + 2) / 2f), (((size.y * scale.y) + 2) / 2f), Camera.main.transform.position.z);
    }

    private void Update()
    {
        print(GetSelectedCell() + " " + GetCellCenter(GetSelectedCell()) + " " + Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    public Vector2Int GetSelectedCell()
    {
        Vector2 mousePositionWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return GetCellAtPosition(mousePositionWorld);
    }
    public Vector2Int GetCellAtPosition(Vector2 position)
    {
        position *= scale;
        return new Vector2Int((int)position.x, (int)position.y);
    }
    public Vector2 GetCellCenter(Vector2Int cell)
    {
        return origin + (new Vector2(cell.x, cell.y) * scale);
    }
    public bool TryPlaceAtPosition(Vector2 position, Placeable placeable, out Vector2Int cell)
    {
        cell = GetCellAtPosition(position);
        if (cell.x <= 0 || cell.x > size.x || cell.y <= 0 || cell.y > size.y)
        {
            return false;
        }
        Vector2Int cellCopy = cell;
        bool canPlace = !cells.Any(c => c.position == cellCopy);
        if (!canPlace)
        {
            return false;
        }
        cells.Add((cell, placeable));
        return true;
    }
}
