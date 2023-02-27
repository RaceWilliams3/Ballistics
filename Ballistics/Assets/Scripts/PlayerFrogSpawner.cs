using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFrogSpawner : MonoBehaviour
{
    public GameObject frogPrefab;

    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            Instantiate(frogPrefab, GetComponent<Transform>().position + new Vector3(0,3,0), Quaternion.identity);
        }
    }
}
