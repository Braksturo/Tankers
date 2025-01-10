using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPlaceMine : MonoBehaviour
{
    public GameObject minePrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlaceMine();
        }
    }

    void PlaceMine()
    {
        Instantiate(minePrefab, transform.position, Quaternion.identity);
    }
}


