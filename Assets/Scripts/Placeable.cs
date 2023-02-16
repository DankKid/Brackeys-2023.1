using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Placeable : MonoBehaviour
{
    [SerializeField] private float normalScale;
    [SerializeField] private float hoverScale;
    [SerializeField] private float draggingScale;

    [SerializeField] private Transform colliderTransform;
    [SerializeField] private GameObject dragBox;
    
    [SerializeField] private Transform spriteTransform;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Transform projectileSpawnTransform;
    [SerializeField] private Transform projectilesTransform;
    [SerializeField] private Projectile projectilePrefab;

    [SerializeField] private Vector2 wiggleRange;
    [SerializeField] private float wiggleFrequency;

    [SerializeField] private bool isDragFromClickPointEnabled;

    private PlaceableGrid grid;
    private PlaceableManager manager;

    private Vector2 defaultPosition;
    private bool IsDragging { get; private set; } = false;
    private Vector2 dragOffset = Vector2.zero;

    private float placementTime;
    public bool IsPlaced { get; private set; } = false;
    public Vector2Int? CellPosition { get; private set; } = null;

    private int projectilesShot = 0;

    // 0 is transparent, 1 is opaque
    public void SetAlpha(float a)
    {
        Color color = spriteRenderer.color;
        color.a = a;
        spriteRenderer.color = color;
    }

    private void Awake()
    {
        grid = FindObjectOfType<PlaceableGrid>();
        manager = FindObjectOfType<PlaceableManager>();

        defaultPosition = transform.position;
        dragBox.SetActive(false);

        ProtectedAwake();
    }

    private void Start()
    {
        ProtectedStart();
    }

    private void Update()
    {
        if (IsPlaced)
        {
            transform.localScale = Vector3.one * normalScale;
            float timeSincePlacement = Time.time - placementTime;
            Placed();
            PlacedUpdate();
        }
        else
        {
            Unplaced();
            UnplacedUpdate();
        }
        ProtectedUpdate();
    }

    private void Placed()
    {
        float zRotation = Mathf.LerpAngle(wiggleRange.x, wiggleRange.y, (Mathf.Sin(timeSincePlacement * wiggleFrequency * Mathf.PI * 2f) + 1f) / 2f);
        spriteTransform.localEulerAngles = new Vector3(0, 0, zRotation);

        int expectedProjectilesShot = (int)(timeSincePlacement * wiggleFrequency); // + 1; // shoot when placed down
        if (expectedProjectilesShot > projectilesShot)
        {
            Projectile projectile = Instantiate(projectilePrefab, projectileSpawnTransform.position, projectilePrefab.transform.rotation, projectilesTransform);
            projectile.SetInstantiator(this);
            projectilesShot++;
        }
    }

    private void Unplaced()
    {
        Vector2 mousePositionWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (IsHovering(mousePositionWorld) && !IsDragging)
            {
                IsDragging = true;
                if (isDragFromClickPointEnabled)
                {
                    dragOffset = (Vector2)transform.position - mousePositionWorld;
                }
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
                    transform.position = (Vector3)cellCenter - colliderTransform.localPosition;
                    placementTime = Time.time;
                    OnPlace();
                }
                else
                {
                    transform.position = defaultPosition;
                    OnTryPlace();
                }
            }
            IsDragging = false;
        }

        if (IsDragging)
        {
            transform.localScale = Vector3.one * draggingScale;
            transform.position = mousePositionWorld + dragOffset;
            UnplacedDragUpdate();
        }
        else if (IsHovering(mousePositionWorld))
        {
            transform.localScale = Vector3.one * hoverScale;
            UnplacedHoverUpdate();
        }
        else
        {
            transform.localScale = Vector3.one * normalScale;
            UnplacedNormalUpdate();
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

    protected virtual void ProtectedAwake()
    {

    }
    protected virtual void ProtectedStart()
    {
        
    }
    protected virtual void ProtectedUpdate()
    {
        
    }

    protected virtual void OnTryPlace()
    {

    }
    protected virtual void OnPlace()
    {

    }

    protected virtual void UnplacedUpdate()
    {

    }
    protected virtual void PlacedUpdate()
    {

    }

    protected virtual void UnplacedDragUpdate()
    {

    }
    protected virtual void UnplacedHoverUpdate()
    {

    }
    protected virtual void UnplacedNormalUpdate()
    {

    }
}
