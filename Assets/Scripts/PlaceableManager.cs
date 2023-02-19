using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlaceableManager : MonoBehaviour
{
    [SerializeField] private Transform placeablesTransform;
    [SerializeField] private Transform placeablePrefabsTransform;

    private readonly List<Placeable> placeablePrefabs = new();
    private readonly List<Placeable> unplaced = new();
    private readonly List<Placeable> placed = new();

    private void Awake()
    {
        for (int i = 0; i < placeablesTransform.childCount; i++)
        {
            Placeable placeable = placeablesTransform.GetChild(i).GetComponent<Placeable>();
            unplaced.Add(placeable);

            Placeable placeablePrefab = Instantiate(placeable, placeable.transform.position, Quaternion.identity, placeablePrefabsTransform);
            FindObjectOfType<PlayerManager>().currentDefenderSprites.Add(placeablePrefab.spriteRenderer);
            placeablePrefab.gameObject.SetActive(false);
            placeablePrefabs.Add(placeablePrefab);
        }
    }

    private void Update()
    {
        placed.RemoveAll(p => p == null);

        for (int i = 0; i < unplaced.Count; i++)
        {
            if (unplaced[i] == null || unplaced[i].IsPlaced)
            {
                Placeable old = unplaced[i];
                unplaced[i] = ClonePlaceablePrefab(placeablePrefabs[i]);
                placed.Add(old);
            }
        }
    }

    private Placeable ClonePlaceablePrefab(Placeable placeablePrefab)
    {
        Placeable placeable = Instantiate(placeablePrefab, placeablePrefab.transform.position, Quaternion.identity, placeablesTransform);
        placeable.gameObject.SetActive(true);
        return placeable;
    }
}
