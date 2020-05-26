using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathEncounterGenerator : MonoBehaviour
{

    public static int[] generatePathEvents(int pathLength)
    {
        /*Sets the array length to be the length of the path and changes
         * the value of each element to represent the type of room that will
         * be in that path.
         * 0 = Transit (no enemies)
         * 1 = Hostile
         * 2 = Encounter
         * 3 = Treasure
         * 4 = Mini Boss
         * 8 = Start
         * 9 = Finish
         * 10 = Shop
         * 11 = Boss
         * 
         * 
         * Heuristics intended: 
         * First few cells be transit cells to get player comfortable.
         * Hostile rooms have a 33% chance of being having an attached hostile room for double enemies. This can only happen once per group.
         * Hostile rooms (or groups) are followed by an expected 4 rooms of transit/treasure rooms to give the player a small breather.
         * Enounters have 3 transit/treasure rooms after them to prevent enemies running into the encounter.
         * Stage difficulty will affect the frequency of certain rooms. For the prototype version difficulty is locked to 1.0 so rates will be standard.
         */


        int[] pathArray = new int[pathLength];
        pathArray[0] = 8;
        pathArray[pathLength-1] = 9;


        Random.InitState(GameManager.seedForRNG);

        //Establishing bias to influence dynamic generation
        
        float difficultyBias = 1.0f;
        float hostileBias = 1.0f;
        float treasureBias = 1.0f;
        float encounterBias = 1.0f;
        float travelBias = 1.0f;
        float miniBossBias = 1.0f;

        float hostileThreshold;
        float treasureThreshold;
        float encounterThreshold;
        float travelThreshold;
        float miniBossThreshold;


        //Set limits


        int travelTimer = 0;

        //Set the room roll
        float roomRandomRoll;

        //Additional values

        bool hostilePreviousRoom = false;
        bool doubleHostilePevious = false;


        for (int i = 1; i < pathLength-1; i++)
        {
            if (i < 5)
            {
                pathArray[i] = 0;
            }
            else
            {
                /* System constructs thresholds for each roll. The room rolls a number and is set to the corresponding room type. Bias changes based on what is selected.
 * 
 * This system is a simple implementation that modifies odds based on selection. It is a proof of concept and in no way balanced for gameplay.
 */
                if (travelTimer == 0)
                {
                    hostileThreshold = 60 * (hostileBias - (0.66f * System.Convert.ToInt32(hostilePreviousRoom))) * difficultyBias;
                    treasureThreshold = 1 * treasureBias;
                    encounterThreshold = 3 * encounterBias;
                    travelThreshold = 62 * travelBias;
                    miniBossThreshold = 2 * miniBossBias;
                }
                else if (travelTimer == 3 && hostilePreviousRoom == true && doubleHostilePevious == false)        //Checks for a double enemy room
                {
                    hostileThreshold = 20 * hostileBias * difficultyBias;
                    treasureThreshold = 1 * treasureBias;
                    encounterThreshold = 0 * encounterBias;
                    travelThreshold = 62 * travelBias;
                    miniBossThreshold = 0;
                    doubleHostilePevious = true;
                }

                else
                {
                    hostileThreshold = 0;
                    treasureThreshold = 0 * treasureBias;
                    encounterThreshold = 0;
                    travelThreshold = 97 * travelBias;
                    miniBossThreshold = 0;
                    doubleHostilePevious = false;
                }



                roomRandomRoll = Random.Range(0.0f, (hostileThreshold + treasureThreshold + encounterThreshold + travelThreshold + miniBossThreshold));
                //Debug.Log("Room " + i + " has rolled: " + roomRandomRoll );
                //Debug.Log("sum is: " + (hostileThreshold + treasureThreshold + encounterThreshold + travelThreshold + miniBossThreshold));
                if (roomRandomRoll <= hostileThreshold)
                {
                    pathArray[i] = 1;

                    hostileBias *= 0.9f;
                    treasureBias *= 1.1f;
                    encounterBias *= 1.1f;
                    travelBias *= 1.1f;
                    miniBossBias *= 1.1f;
                    hostilePreviousRoom = true;
                    travelTimer = 3;
                }

                else if (roomRandomRoll > hostileThreshold && roomRandomRoll <= hostileThreshold + treasureThreshold)
                {
                    pathArray[i] = 3;
                    hostileBias *= 1.1f;
                    treasureBias *= 0.9f;
                    encounterBias *= 1.1f;
                    travelBias *= 1.1f;
                    miniBossBias *= 1.1f;
                    hostilePreviousRoom = false;
                    if (!(travelTimer < 1)) travelTimer -= 1;
                }
                else if (roomRandomRoll > hostileThreshold + treasureThreshold && roomRandomRoll <= hostileThreshold + treasureThreshold + encounterThreshold)
                {
                    pathArray[i] = 2;
                    hostileBias *= 1.1f;
                    treasureBias *= 1.1f;
                    encounterBias *= 0.9f;
                    travelBias *= 1.1f;
                    miniBossBias *= 1.1f;
                    hostilePreviousRoom = false;
                    travelTimer = 3;
                }
                else if (roomRandomRoll > hostileThreshold + treasureThreshold + encounterBias && roomRandomRoll <= hostileThreshold + treasureThreshold + encounterThreshold + miniBossThreshold)
                {
                    pathArray[i] = 4;
                    hostileBias *= 0.9f;
                    treasureBias *= 1.1f;
                    encounterBias *= 1.1f;
                    travelBias *= 1.1f;
                    miniBossBias *= 0;  //Unique Bias, only 1 miniBoss per level
                    hostilePreviousRoom = true;
                    travelTimer = 3;
                }
                else
                {
                    pathArray[i] = 0;
                    hostileBias *= 1.1f;
                    treasureBias *= 1.1f;
                    encounterBias *= 1.1f;
                    travelBias *= 0.9f;
                    miniBossBias *= 1.1f;
                    hostilePreviousRoom = false;
                    if (!(travelTimer < 1)) travelTimer -= 1;
                }
            }

            






        }
        return pathArray;
    }


}
