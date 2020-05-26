using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCollision : MonoBehaviour
{
    private ParticleSystem ps;


    private void Start()
    {
        ps = this.GetComponent<ParticleSystem>();
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag=="Enemy")
        {
            other.gameObject.GetComponent<Enemy_Stats>().AdjustCurHealth(-this.GetComponent<Spells>().spDamage);
        }
    }
}
