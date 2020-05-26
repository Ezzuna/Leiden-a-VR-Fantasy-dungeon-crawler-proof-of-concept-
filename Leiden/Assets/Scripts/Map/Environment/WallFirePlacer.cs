using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFirePlacer : MonoBehaviour
{

    public int spawnedID;
    private MapController mapController;
    public GameObject[] wallFires;
    ParticleSystem brazier1;
    ParticleSystem brazier2;
    ParticleSystem brazier3;
    ParticleSystem brazier4;
    GameObject[] brazierArray; 
    bool hasSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        mapController = GameObject.Find("MapCreation").GetComponent<MapController>();
        wallFires = GameManager.fetchWallLightsPrefabs();
        //SetSpawnDetails();
        brazierArray = new GameObject[4];

    }

    public void SetSpawnDetails()
    {
        //this.spawnedID = ID;
        // this.mapController = tempMC;
        hasSpawned = true;
        int k = Random.Range(0, wallFires.Length);

        GameObject child = this.transform.GetChild(0).gameObject;
        brazier1 = wallFires[k].GetComponent<ParticleSystem>();
        brazier1 = Instantiate(brazier1, this.transform.GetChild(0).transform);
        brazier1.transform.SetParent(child.transform);
        brazier1.Play();

        child = this.transform.GetChild(1).gameObject;
        brazier2 = wallFires[k].GetComponent<ParticleSystem>();
        brazier2 = Instantiate(brazier2, this.transform.GetChild(1).transform);
        brazier2.transform.position = child.transform.position;
        Debug.Log("Child x: " + child.transform.position.x + " Child y:" + child.transform.position.y);

        brazier2.Play();

        child = this.transform.GetChild(2).gameObject;
        brazier3 = wallFires[k].GetComponent<ParticleSystem>();
        brazier3 = Instantiate(brazier3, this.transform.GetChild(2).transform);
        brazier3.transform.SetParent(child.transform);
        brazier3.Play();

        child = this.transform.GetChild(3).gameObject;
        brazier4 = wallFires[k].GetComponent<ParticleSystem>();
        brazier4 = Instantiate(brazier4, this.transform.GetChild(3).transform);
        brazier4.transform.SetParent(child.transform);
        brazier4.Play();
    }

    public void DestroyFires()
    {
        //hasSpawned = false;
        brazier1.Stop();
        Destroy(brazier1.transform.GetChild(0).gameObject);
        brazier2.Stop();
        Destroy(brazier2.transform.GetChild(0).gameObject);
        brazier3.Stop();
        Destroy(brazier3.transform.GetChild(0).gameObject);
        brazier4.Stop();
        Destroy(brazier4.transform.GetChild(0).gameObject);

    }

    // Update is called once per frame
    void Update()
    {

        if ((transform.parent.GetComponent<MapCellAttachment>().pathID - mapController.GetCurrentPathID()) < 10 && hasSpawned == false) SetSpawnDetails();
        else if ((mapController.GetCurrentPathID() - transform.parent.GetComponent<MapCellAttachment>().pathID) > 10 && hasSpawned == true) DestroyFires();
    }
}

