using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPathPusher : MonoBehaviour
{
    static List<MapCell> mapCells = new List<MapCell>();
    static int currentPathID = 1;
    static float t;
    static Vector3 startPosition;
    static Vector3 target;
    float rotation;
    Quaternion targetRotate;
    MapController mapController;
    PlayerState playerCharacterState;
    bool stateReported = false;
    static bool playerInitialised = false;



    float timeToReachTarget;

    static GameObject Camera;

    static GameObject GetCamera()
    {
        Camera = GameObject.Find("OVRCameraRig");
        return Camera;
    }
    // Start is called before the first frame update
    void Start()
    {
        Camera = GetCamera();
        
        mapController = GameObject.Find("MapCreation").GetComponent<MapController>();
        playerCharacterState = GameObject.Find("OVRCameraRig").GetComponent<PlayerState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown) playerInitialised = true;
        if (playerInitialised)
        {
            if (Camera.transform.position == target && stateReported == false)
            {
                if (playerCharacterState.getPlayerState() == pState.playerTravelling) playerCharacterState.setPlayerState(pState.playerTravelEnd);      //checks if there has been an update since the travelling started

                stateReported = true;
            }
            else
            {
                t += Time.deltaTime / timeToReachTarget / 2;

                transform.position = Vector3.Lerp(startPosition, target, t);
                if (currentPathID > 1)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotate, t);
                }
            }
        }





    }

    public void pushPlayer()
    {
        currentPathID++;
        stateReported = false;
        foreach (var item in mapCells)
        {
            if (item.pathID == currentPathID)
            {
                Vector3 cameraPosition = item.mapPosition;
                cameraPosition.y = 0.3f;
                PathPush(cameraPosition, 1.0f);
                rotation = RotationFinder(currentPathID);
                targetRotate = RotatePush(rotation, 1.0f);
                mapController.currentPathID += 1;


            }
        }
        mapController.Invoke("PlayerProgressed",0f);
    } 

    /*
    public static void Path()
    {
        
        mapCells = MapBuilder.mapCells;
        foreach (var item in mapCells)
        {
            Debug.Log(item.mapPosition.x + item.mapPosition.z);
        }
    }
    */

    static float RotationFinder(int currentPathID)  //method that returns the rotation applied to the camera when moving from each cell
    {
        float rotation = 0f;
        int temp = 0;
        foreach (var item in mapCells)
        {
            if (item.pathID == currentPathID - 1)
            {
                temp = item.entryDirection;


                if (temp != 0)
                {

                    int x = temp - item.exitDirection; // - item.exitDirection;

                    switch (x)
                    {
                        case -3:
                            rotation = -90.0f;
                            break;
                        case -2:
                            rotation = 0;
                            break;
                        case -1:
                            rotation = 90.0f;
                            break;
                        case 1:
                            rotation = -90.0f;
                            break;
                        case 2:
                            rotation = 0;
                            break;
                        case 3:
                            rotation = 90.0f;
                            break;

                    }
                }


                else
                {

                    int x = item.exitDirection;
                    x = x * 90 + 90;
                    rotation = x;
                }

            }
        }
        /*foreach (var item in mapCells) { 
            if (item.pathID == currentPathID)
            {
                Debug.Log("Hit on push");
                
                if (temp != 0)
                {
                    Debug.Log("Equation is temp " + temp + " - Item entry direction " + item.entryDirection + " = x");
                    int x = temp - item.entryDirection; // - item.exitDirection;
                    Debug.Log("x is : " + x);
                    switch (x)
                    {
                        case -3:
                            rotation = 270.0f;
                            break;
                        case -2:
                            rotation = 0;
                            break;
                        case -1:
                            rotation = 90.0f;
                            break;
                        case 1:
                            rotation = 270.0f;
                            break;
                        case 2:
                            rotation = 0;
                            break;
                        case 3:
                            rotation = 270.0f;
                            break;

                    }
                }
            

                else {
                    Debug.Log("Hit on default");
                    int x = item.exitDirection;
                    x = x * 90 + 90;
                    rotation = x;
                }
            }
        }*/

        return rotation;
    }
    static Quaternion RotatePush(float destination, float time)
    {
        Vector3 temp = new Vector3(0,1,0);
        Camera = GetCamera(); 
        Quaternion u = Camera.transform.rotation;
        Quaternion v = Quaternion.AngleAxis(destination, temp);

        u *= v;

        return u;

    }
    void PathPush(Vector3 destination, float time)
    {

           
            
                //Camera.transform.position = item.mapPosition;
                t = 0;
                startPosition = transform.position;
                timeToReachTarget = time;
                target = destination;

            
        
    }

    public static void SetupPlayer()
    {
        mapCells = MapBuilder.mapCells;

        GameObject player =GameObject.Find("OVRCameraRig");
        player.GetComponent<PlayerCharacterController>().Invoke("InitializePlayer", 0f);

        int tempEntryID = 0;
        foreach (var item in mapCells)
        {
            if (item.pathID == 2)
            {
                tempEntryID = item.entryDirection; 
            }


        }


        foreach (var item in mapCells)
        {
            if (item.pathID == 1)
            {
                float rotation = 0.0f;
                Vector3 cameraPosition = item.mapPosition;
                cameraPosition.y = 0.5f;
                int x = item.exitDirection;
                switch (x)
                {
                    case 1:
                        rotation = -90.0f;
                        break;
                    case 2:
                        rotation = -180.0f;
                        break;
                    case 3:
                        rotation = 90.0f;
                        break;
                    case 4:
                        rotation = 0.0f;
                        break;

                }
                item.entryDirection = tempEntryID; //hack to make rotate push work for now. Will improve solution if time.
                GameObject Camera = GetCamera();
                Camera.transform.position = cameraPosition;
                Camera.transform.rotation = Quaternion.Euler(0, rotation, 0);
                






            }
        }

        startPosition = target = Camera.transform.position;

    }
}
