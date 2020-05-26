using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int fireSpeed = 15;
    public GameObject spell;
    public Transform firePoint;
    public AudioClip ShieldSound;
    private AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        OVRInput.Update();
        if (Input.GetKey(KeyCode.K) || OVRInput.Get(OVRInput.Button.Four))
        {
            if (this.gameObject.GetComponent<BoxCollider>()==null)
            {
                this.gameObject.AddComponent<BoxCollider>();
            }
           transform.localScale = new Vector3(10, 10, 10);
            

        }
        else 
        {
           
            transform.localScale = new Vector3(2, 2, 2);
        }
    }

    void fire(GameObject spell)
    {
        GameObject bullet;


        //bullet = BulletsPool.bulletsPoolInstance.GetPooledObject(spell);
        bullet = ObjectPool.current.GetObject(spell);
        if (bullet != null)
        {
            bullet.SetActive(true);
            bullet.transform.position = firePoint.transform.position;
            bullet.transform.rotation = firePoint.transform.rotation;
        }


        bullet.GetComponent<Rigidbody>().velocity = firePoint.transform.forward * fireSpeed;

        StartCoroutine(ObjectPool.current.Destroy(bullet, 3));
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="EnemySpell")
        {
            PlayerSound(ShieldSound);
            fire(spell);
        }
    }

    private void PlayerSound(AudioClip Sound)
    {
        AudioSource.clip = Sound;
        AudioSource.Play();
    }
}
