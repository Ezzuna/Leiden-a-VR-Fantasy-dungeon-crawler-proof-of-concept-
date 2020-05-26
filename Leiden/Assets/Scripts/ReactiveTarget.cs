using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour {
    public GameObject wall;

    private string healthString;
    public int healthnum;
    public int damage;
    public GameObject[] item;
    public AudioClip dieSound;
    private AudioSource AudioSource;

    public void Reacttohit()
    {
        numAdjust(healthnum, damage);
        
    }
    private IEnumerator Die()
    {
        
        Instantiate(wall, this.transform.position, Quaternion.identity);
        Instantiate(item[Random.Range(0, item.Length)], new Vector3(this.transform.position.x, this.transform.position.y+1, this.transform.position.z), Quaternion.identity);
        yield return new WaitForSeconds(0.0f);
        Destroy(this.gameObject);
        PlayDieSound();

    }
    // Use this for initialization
    void Start()
    {
        
        healthString = " ";
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void numAdjust(int health, int numDecrease)
    {
        if (health > 0)
        {
          
            healthString = "Health ：" + healthnum.ToString ();
            healthnum -= numDecrease;

        }
        else
        {
            PlayDieSound();
            healthString = "die";
            StartCoroutine(Die());
        }
    }

   public string gethealthString()
    {
        return healthString;
    }

    private void PlayDieSound()
    {
        AudioSource.clip = dieSound;
        AudioSource.Play();
    }

}
