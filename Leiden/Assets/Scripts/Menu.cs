using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public Text startText;
    public Text Scoretext;
    private int Score;

    void Start()
    {
        
        


    }

    // Update is called once per frame
    void Update()
    {
        Score = StaticClass.GetScore();
        Scoretext.text = "Score: " + Score.ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void OnClickBtn_Play()
    {
        if (GameObject.Find("LoadOut").GetComponent<ToggleGroup>().AnyTogglesOn())
        {
            GameObject.Find("LoadOut").SetActive(false);
            this.GetComponent<CanvasGroup>().alpha = 0;
            SceneManager.LoadScene(1);
        }
        
    }

    public void OnClickBtn_Guide()
    {
        
    }

    public void OnClickBtn_Exit()
    {
        Application.Quit();
    }

    public void OnToggleLoadout()
    {
        /*
          GameObject.Find("Invntory").GetComponent<UIInventory>().Spells[0] = FireBall;
          GameObject.Find("Invntory").GetComponent<UIInventory>().Spells[1] = Meteor;
          */
        int[] temp = new int[] { 0, 0 };
        StaticClass.SetCarrySpells(temp);
        
    }

    public void OnToggleLoadout2()
    {
        /*
        GameObject.Find("Invntory").GetComponent<UIInventory>().Spells[0] = ShadowFire;
        GameObject.Find("Invntory").GetComponent<UIInventory>().Spells[1] = ShadowFireVolley;
        */
        int[] temp = new int[] { 1, 1 };
        StaticClass.SetCarrySpells(temp);
    }

    public void OnToggleLoadout3()
    {
        /*
        GameObject.Find("Invntory").GetComponent<UIInventory>().Spells[0] = ShadowFire;
        GameObject.Find("Invntory").GetComponent<UIInventory>().Spells[1] = ShadowFireVolley;
        */
        int[] temp = new int[] { 0, 2 };
        StaticClass.SetCarrySpells(temp);
    }

    void Pause()
    {
        Time.timeScale = 0;
        this.GetComponent<CanvasGroup>().alpha = 1;
        startText.text = "Loadout";
    }
}
