using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    //Player State machine - class meant to provide gameplay loop information on what the player is doing and to in the future support data for animations for the player. Currently only has support
    //for single states, but expanding to multiple states is quite simple but beyond the scope of this prototype.

    private pState playerState;

    private void Start()
    {
        Debug.Log("Playerstate initialized");
        InitPlayerState();
    }


    private void InitPlayerState()
    {
        this.playerState=pState.playerIsLoading;
    }

    public void setPlayerState(pState newPState)
    {
        this.playerState = newPState;
    }

    public pState getPlayerState()
    {
        return this.playerState;
    }


}

public enum pState
{
    playerIsLoading,
    playerTravelling,
    playerTravelEnd,
    playerInMenu,
    playerInDialogue,
    playerInFight,
    playerDied,
    playerInShop,
    playerIdle

}
