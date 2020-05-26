using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    public int spellID;
    public string spName;
    public int spSlot;
    public float spDamage;
    public string spDescription;
    public int spStoreCost;
    public int spItemPool;          //Selects which item pool the item is in
    public float fireRate;
    public float ManaCost;
      

    public void AdjustDamage(float adj)
    {
        spDamage += adj;

        if (spDamage <= 0)
        {
            spDamage = 0;
        }
              
    }

    private void OnCollisionEnter(Collision collision)
    {
        ObjectPool.current.PoolObject(this.gameObject);
    }
   private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            ObjectPool.current.PoolObject(this.gameObject);
            other.gameObject.GetComponent<PlayerCharacterController>().AdjustCurHealth(-10);
        }
    }
}
