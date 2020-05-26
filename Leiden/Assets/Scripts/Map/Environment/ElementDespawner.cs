using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementDespawner : MonoBehaviour
{
    public int spawnedID;
    private MapController mapController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetSpawnDetails(int ID, MapController tempMC)
    {
        this.spawnedID = ID;
        this.mapController = tempMC;
    }

    // Update is called once per frame
    void Update()
    {
        if (mapController.GetCurrentPathID()-10 > spawnedID) Destroy(this.gameObject);
    }
}
