using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerCharacterController : MonoBehaviour
{

    MapPathPusher playerPusher;
    Player_UIbar uibar;
    MapController mapController;
    PlayerState playerCharacterState;
    pState playerState;
    bool playerInitialised = false;
    bool isInAction = false;
    bool canProceed = true;
    public float Player_HP = 100;
    private float curHealth;
    public float Player_Mana = 100;
    private float curMana;
 

    // Start is called before the first frame update
    void Start()
    {
        curHealth = Player_HP;
        curMana = Player_Mana;
        uibar = GameObject.Find("ArmGuard").GetComponent<Player_UIbar>();
        uibar.RefreshHPStatus(curHealth, Player_HP);
        uibar.RefreshManaStatus(curMana, Player_Mana);

    }

    // Update is called once per frame
    void Update()
    {
     
        if (playerInitialised)
        {
            //playerState = playerCharacterState.getPlayerState();
            playerState = playerCharacterState.getPlayerState();

            switch (playerState)
            {
                case pState.playerTravelEnd:
                    if (canProceed)                             //If player is allowed to proceed. Basically add in here that after x seconds, even if player is still fighting we push them to prevent softlock
                    {
                        //check for enemies here. If no enemies present then set to idle.
                        
                        playerCharacterState.setPlayerState(pState.playerIdle);
                        isInAction = false;
                    }
                    break;

                case pState.playerInFight:      //Perform check to see if player should still be in fight
                    isInAction = false;
                    break;

                case pState.playerInDialogue:
                    isInAction = false;
                    break;

                case pState.playerInMenu:
                    break;

                case pState.playerInShop:
                    isInAction = false;
                    break;

                case pState.playerIsLoading:
                    break;

                case pState.playerTravelling:
                    break;

                case pState.playerDied:
                    int Socre= GameObject.Find("Invntory").GetComponent<UIInventory>().getScore();
                    StaticClass.SetScore(Socre);
                    SceneManager.LoadScene(0);
                    break;
            }

            
            if (playerState == pState.playerIdle)       //seperate from switch case so can be ran the same update tick 
            {



                playerPusher.Invoke("pushPlayer", 0f);
                isInAction = true;
                playerCharacterState.setPlayerState(pState.playerTravelling);

            }
            OVRInput.Update();
            if (Input.GetKeyDown("space")|| OVRInput.Get(OVRInput.Button.Four))  //debug stuff
            {
                playerCharacterState.setPlayerState(pState.playerInFight);

            }
            else if ((Input.GetKeyDown("c") || OVRInput.Get(OVRInput.Button.Three)) && isInAction!= true)
            {
                playerCharacterState.setPlayerState(pState.playerIdle);
            }
        }



    }


    public void InitializePlayer()
    {
        int[] equippedSpells = StaticClass.GetCarrySpells();

        switch (equippedSpells[0])
        {
            case 0: GameObject.Find("Invntory").GetComponent<UIInventory>().Spells[0] = GameObject.Find("FireBall");
                break;
            case 1: GameObject.Find("Invntory").GetComponent<UIInventory>().Spells[0] = GameObject.Find("ShadowFire");
                break;
        }

        switch (equippedSpells[1])
        {
            case 0: GameObject.Find("Invntory").GetComponent<UIInventory>().Spells[1] = GameObject.Find("Meteor");
                break;
            case 1: GameObject.Find("Invntory").GetComponent<UIInventory>().Spells[1] = GameObject.Find("ShadowFireVolley");
                break;
            case 2: GameObject.Find("Invntory").GetComponent<UIInventory>().Spells[1] = GameObject.Find("Frostbite");
                break;
        }
        playerPusher = GameObject.Find("OVRCameraRig").GetComponent<MapPathPusher>();
        playerCharacterState = GameObject.Find("OVRCameraRig").GetComponent<PlayerState>();
        mapController = GameObject.Find("MapCreation").GetComponent<MapController>();
        playerInitialised = true ;
        playerCharacterState.setPlayerState(pState.playerTravelling);
        

    }

    public void AdjustCurHealth(float adj)
    {
        curHealth += adj;

        if (curHealth <= 0)
        {
            curHealth = 0;
            playerCharacterState.setPlayerState(pState.playerDied);
        }
        if (curHealth > Player_HP)
        {
            curHealth = Player_HP;
        }
        if (Player_HP < 1)
        {
            Player_HP = 1;
        }


        uibar.RefreshHPStatus(curHealth, Player_HP);
    }
    public void AdjustMaxHealth(float adj)
    {
        Player_HP += adj;

        if (curHealth <= 0)
        {
            curHealth = 0;       
        }
        if (curHealth > Player_HP)
        {
            curHealth = Player_HP;
        }
        if (Player_HP < 1)
        {
            Player_HP = 1;
        }


        uibar.RefreshHPStatus(curHealth, Player_HP);
    }

    public void AdjustCurMana(float adj)
    {
        curMana += adj;

        if (curMana <= 0)
        {
            curMana = 0;
        }
        if (curMana > Player_Mana)
        {
            curMana = Player_Mana;
        }
        if (Player_Mana < 1)
        {
            Player_Mana = 1;
        }
        uibar.RefreshManaStatus(curMana, Player_Mana);
    }

    public void AdjustMaxMana(float adj)
    {
        Player_Mana += adj;

        if (curMana <= 0)
        {
            curMana = 0;
        }
        if (curMana > Player_Mana)
        {
            curMana = Player_Mana;
        }
        if (Player_Mana < 1)
        {
            Player_Mana = 1;
        }
        uibar.RefreshManaStatus(curMana, Player_Mana);
    }
    public float getCurHealth()
    {
        return curHealth;
    }

    public float getMaxHealth()
    {
        return Player_HP;
    }
    public float getMaxMana()
    {
        return Player_Mana;
    }

    public float getCurMana()
    {
        return curMana;
    }

}
