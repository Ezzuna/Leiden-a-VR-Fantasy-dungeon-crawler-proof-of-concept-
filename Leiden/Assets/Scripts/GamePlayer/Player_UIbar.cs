using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_UIbar : MonoBehaviour
{
    
    public Text m_TxtHp;
    public Slider m_SldHp;
    public Text m_TxtMana;
    public Slider m_SldMana;

    // Start is called before the first frame update
    void Start()
    {
      
        

    }

    // Update is called once per frame
    void Update()
    {
        
     
    }

   
  
    public void RefreshHPStatus(float curHealth, float maxHealth)
    {
        this.m_TxtHp.text = string.Format("{0}/{1}", curHealth, maxHealth);
        if (maxHealth != 0)
        {
            this.m_SldHp.value = curHealth / maxHealth;
        }
      
    }

    public void RefreshManaStatus(float curMana, float maxMana)
    {
        
        this.m_TxtMana.text = string.Format("{0}/{1}", curMana, maxMana);
        if (maxMana != 0)
        {
            this.m_SldMana.value = curMana / maxMana;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("-10");
            //AdjustCurHealth(-10);
            
        }

    }

 

}
