using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi3 : Enemy_Stats
{
    public Transform target;
    public int moveSpeed;
    public int rotateSpeed;


    

    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        target = go.transform;
    }

    void Update()
    {
        Debug.DrawLine(target.transform.position, this.transform.position, Color.yellow);

        //lock at target Player
        this.transform.rotation = Quaternion.Slerp(
             this.transform.rotation,
             Quaternion.LookRotation(target.position - this.transform.position),
             rotateSpeed * Time.deltaTime
        );

        //Move towards target
        this.transform.position += this.transform.forward * moveSpeed * Time.deltaTime;

    }
}
