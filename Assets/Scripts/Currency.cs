using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    [SerializeField] private int value;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float timeToFadeIn;
    [SerializeField] private float rotationSpeedDegreesPerSecond;
    [SerializeField] private float movementDistance;
    [SerializeField] private GameObject clickCircle;
    PlayerManager pMan;

    private float timeSinceInstantiation = 0;
    private float rotationDirection;
    private Vector2 movementVector;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private float clickRadius;
    
    private void Awake()
    {
        pMan = GameObject.Find("Scripts").GetComponent<PlayerManager>();
        bool isReversed = Random.value > 0.5f;
        rotationDirection = isReversed ? -1f : 1f;

        float randomAngle = Random.Range(0, 2f * Mathf.PI);
        movementVector = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));

        startPosition = transform.position;
        endPosition = startPosition + (movementVector * movementDistance);

        clickRadius = (clickCircle.transform.localScale.x + clickCircle.transform.localScale.y) / 4f;

        clickCircle.SetActive(false);
    }

    private void Update()
    {
        timeSinceInstantiation += Time.deltaTime;

        float fadeInPercent = Mathf.Clamp01(timeSinceInstantiation / timeToFadeIn);

        // Fade In
        Color color = spriteRenderer.color;
        color.a = Mathf.SmoothStep(0, 1, fadeInPercent);
        spriteRenderer.color = color;

        // Move
        transform.position = Vector2.Lerp(startPosition, endPosition, Mathf.Sqrt(fadeInPercent));

        // Rotate
        Vector3 rotation = transform.localEulerAngles;
        rotation.z += rotationDirection * rotationSpeedDegreesPerSecond * Time.deltaTime;
        transform.localEulerAngles = rotation;

        if (IsClicking())
        {
            // TODO Currency picked up code here
            // use "value" int possibly?
            pMan.getMoney(value);
            Destroy(gameObject);
        }
    }

    private bool IsClicking()
    {
        if (timeToFadeIn > timeSinceInstantiation)
        {
            return false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePositionWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float distance = Vector2.Distance(mousePositionWorld, transform.position);
            if (distance < clickRadius)
            {
                return true;
            }
        }
        return false;
    }
}
