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

    // TODO Gray out placeables you cant afford

    private void Awake()
    {
        for (int i = 0; i < placeablesTransform.childCount; i++)
        {
            Placeable placeable = placeablesTransform.GetChild(i).GetComponent<Placeable>();
            unplaced.Add(placeable);

            Placeable placeablePrefab = Instantiate(placeable, placeable.transform.position, Quaternion.identity, placeablePrefabsTransform);
            placeablePrefab.gameObject.SetActive(false);
            placeablePrefabs.Add(placeablePrefab);
        }
    }

    private void Update()
    {
        placed.RemoveAll(p => p == null);

        for (int i = 0; i < unplaced.Count; i++)
        {
            if (unplaced[i].IsPlaced)
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
