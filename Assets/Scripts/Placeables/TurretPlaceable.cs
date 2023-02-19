using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlaceable : Placeable
{
    [SerializeField] private Transform projectileSpawnTransform;
    [SerializeField] private Transform projectilesTransform;
    [SerializeField] private Projectile projectilePrefab;

    [SerializeField] private Vector2 wiggleRange;
    [SerializeField] private float wiggleFrequency;
    [SerializeField] AudioSource audioSource;
    SoundManager soundMan;
    private int projectilesShot = 0;

    protected override void ProtectedStart()
    {
        base.ProtectedStart();
        soundMan = FindObjectOfType<SoundManager>();
    }


    protected override void PlacedUpdate()
    {
        float zRotation = Mathf.LerpAngle(wiggleRange.x, wiggleRange.y, (Mathf.Sin(TimeSincePlacement * wiggleFrequency * Mathf.PI * 2f) + 1f) / 2f);
        spriteRenderer.transform.localEulerAngles = new Vector3(0, 0, zRotation);

        int expectedProjectilesShot = (int)(TimeSincePlacement * wiggleFrequency); // + 1; // shoot when placed down
        if (expectedProjectilesShot > projectilesShot)
        {
            Projectile projectile = Instantiate(projectilePrefab, projectileSpawnTransform.position, projectilePrefab.transform.rotation, projectilesTransform);
            soundMan.PlayShoot(audioSource);
            projectile.SetInstantiator(this);
            projectilesShot++;
            
        }
    }
}