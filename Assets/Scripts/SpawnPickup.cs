using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickup : MonoBehaviour
{

    private int random;

    public GameObject[] pickupPrefabs;
    // Start is called before the first frame update
    void Start()
    {
    
        Instantiate(pickupPrefabs[randIndex()], transform.position, transform.rotation);
    }

    public int randIndex()
    {
        random = Random.Range(0, pickupPrefabs.Length);
        return random;

    }
    
}
