using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorPlaceable : Placeable
{
    [SerializeField] private float generationCooldown;
    [SerializeField] private Transform currencySpawnTransform;
    [SerializeField] private Transform currenciesTransform;
    [SerializeField] private Currency currencyPrefab;

    [SerializeField] private Vector2 wiggleRange;
    [SerializeField] private float wiggleFrequency;

    private float timeUntilGeneration;

    protected override void ProtectedAwake()
    {
        timeUntilGeneration = generationCooldown;
    }

    protected override void PlacedUpdate()
    {
        timeUntilGeneration -= Time.deltaTime;

        // Wiggle
        float zRotation = Mathf.LerpAngle(wiggleRange.x, wiggleRange.y, (Mathf.Sin(TimeSincePlacement * wiggleFrequency * Mathf.PI * 2f) + 1f) / 2f);
        spriteRenderer.transform.localEulerAngles = new Vector3(0, 0, zRotation);

        // Generation
        if (timeUntilGeneration <= 0)
        {
            timeUntilGeneration += generationCooldown;
            Instantiate(currencyPrefab, currencySpawnTransform.position, Quaternion.identity, currenciesTransform);
        }
    }
}