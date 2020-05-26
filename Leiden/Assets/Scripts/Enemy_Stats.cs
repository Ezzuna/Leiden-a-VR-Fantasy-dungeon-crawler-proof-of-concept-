using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Stats : MonoBehaviour
{
    public float MaxHp = 100;
    public float CurHp = 100;
    public float damage = 10;
    public int Score = 100;
    private UIInventory inventory;
    // Start is called before the first frame update

    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
    public void AdjustCurHealth(float adj)
    {
        inventory = GameObject.Find("Invntory").GetComponent<UIInventory>();
        this.GetComponent<MeshRenderer>().material.color = Color.red;
        CurHp += adj;

        if (CurHp <= 0)
        {
            CurHp = 0;
            Destroy(this.gameObject);
            inventory.alterScore(Score);
        }
        if (CurHp > MaxHp)
        {
            CurHp = MaxHp;
        }
        if (MaxHp < 1)
        {
            MaxHp = 1;
        }

        StartCoroutine("SetColor");
    }
    IEnumerator SetColor()
    {
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<MeshRenderer>().material.color = Color.white;
    }
}
