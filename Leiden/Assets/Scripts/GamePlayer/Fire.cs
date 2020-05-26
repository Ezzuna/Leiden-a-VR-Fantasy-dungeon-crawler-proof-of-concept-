using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    
    private GameObject spell1;
    private GameObject spell2;
    public Transform firePoint;
    public GameObject UI;
    public GameObject Hand;
    private ParticleSystem particle;
    private bool isOpenPack=false;
    private float timeToFire = 0;
    private float timeToOpenPack = 0;
    private float fireRate1 = 1f;
    private float fireRate2 = 1f;
    private UIInventory inventory;
    private PlayerCharacterController playerCharacter;
    public AudioClip fireSound;
    private AudioSource AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        inventory = GameObject.Find("Invntory").GetComponent<UIInventory>();
        spell1 = inventory.GetSpell()[0];
        fireRate1 =  spell1.GetComponent<Spells>().fireRate;
        spell2 = inventory.GetSpell()[1];
        fireRate2 = spell2.GetComponent<Spells>().fireRate;
        playerCharacter = GameObject.Find("OVRCameraRig").GetComponent<PlayerCharacterController>();
    }


    // Update is called once per frame
    
    void Update()
    {
        spell1 = inventory.GetSpell()[0];
        
        spell2 = inventory.GetSpell()[1];
      
        OVRInput.Update();
        if ((Input.GetKey(KeyCode.J)  || (OVRInput.Get(OVRInput.Button.One))))
        {
            fire(spell1);
        }
        if ((Input.GetKey(KeyCode.K) || (OVRInput.Get(OVRInput.Button.Two))))
        {
            if (playerCharacter.getCurMana()>=spell2.GetComponent<Spells>().ManaCost)
            {
                fire(spell2);
                
            }
           
        }
       
          OpenPack();
        

        
    }
    void fire(GameObject spell)
    {

        particle = spell.GetComponent<ParticleSystem>();
        if (timeToFire < Time.time)
        {

            /* GameObject bullet;

             //clone = (Rigidbody)Instantiate(spell, firePoint.position, firePoint.rotation);
            // bullet = BulletsPool.bulletsPoolInstance.GetPooledObject(spell);
             bullet = ObjectPool.current.GetObject(spell);
             if (bullet != null)                  //不为空时执行
             {
                 bullet.SetActive(true);         
                 bullet.transform.position = firePoint.transform.position;
                 bullet.transform.rotation = firePoint.transform.rotation;
             }


             bullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * spell.GetComponent<Spells>().spSpeed);

             // Destroy(clone.gameObject, 3);

             StartCoroutine(ObjectPool.current.Destroy(bullet, 3));*/
            
            particle.Play();
            PlayerSound(fireSound);
            timeToFire = Time.time + 1 / fireRate1;
            playerCharacter.AdjustCurMana(-spell.GetComponent<Spells>().ManaCost);
        }
        if (OVRInput.Get(OVRInput.Button.One) == false)
        {
            //timeToFire = 0;
        }
        
    }

    void OpenPack()
    {
        if ((Input.GetKey(KeyCode.P) && timeToOpenPack < Time.time) || (OVRInput.Get(OVRInput.Button.Three) && timeToOpenPack < Time.time))
        {
            if (isOpenPack)
            {
                isOpenPack = false;
                UI.GetComponent<CanvasGroup>().alpha = 0;
                Debug.Log("ClosePack");
            }
            else if (!isOpenPack)
            {
                //Vector3 temp = ((Hand.transform.forward.normalized + Hand.transform.up.normalized / 2).normalized * 5) + Hand.transform.position;
               
                UI.transform.position = Hand.transform.position;
                UI.transform.rotation = Hand.transform.rotation;
                UI.transform.SetParent(Hand.transform);
                UI.transform.localScale = new Vector3(0.0002365454f, 0.0002365454f, 0.0002365454f);    
                UI.GetComponent<CanvasGroup>().alpha = 1;
                isOpenPack = true;
                Debug.Log("OpenPack");
            }
            timeToOpenPack = Time.time + 1;
        }
        if (OVRInput.Get(OVRInput.Button.Three) == false)
        {
            timeToOpenPack = 0;
        }
    }
    private void PlayerSound(AudioClip Sound)
    {
        AudioSource.clip = Sound;
        AudioSource.Play();
    }
}
