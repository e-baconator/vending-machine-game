using System.Collections.Generic;
using UnityEngine;

public class InteractableShelves : MonoBehaviour
{
    private List<Transform> shelves = new List<Transform>();
    public bool isFull;

    void PopulateShelfList()
    {
        for (int row = 0; row < transform.childCount; row++)
        {
            for (int shelf = 0; shelf < transform.GetChild(row).childCount; shelf++)
            {
                shelves.Add(transform.GetChild(row).GetChild(shelf));
            }
        }
    }

    void Start()
    {
        isFull = false;
        PopulateShelfList();
    }

    void Update()
    {
        foreach (Transform shelf in shelves)
        {
            if (shelf.childCount < 1)
            {
                return;
            }
        }

        isFull = true;
    }
}
