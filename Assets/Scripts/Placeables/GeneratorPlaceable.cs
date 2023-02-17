using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorPlaceable : Placeable
{
    [SerializeField] private Vector2 wiggleRange;
    [SerializeField] private float wiggleFrequency;

    protected override void PlacedUpdate()
    {
        float zRotation = Mathf.LerpAngle(wiggleRange.x, wiggleRange.y, (Mathf.Sin(TimeSincePlacement * wiggleFrequency * Mathf.PI * 2f) + 1f) / 2f);
        spriteRenderer.transform.localEulerAngles = new Vector3(0, 0, zRotation);
    }
}