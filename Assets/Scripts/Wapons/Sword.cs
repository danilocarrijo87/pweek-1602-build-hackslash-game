using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private bool canHit = false;
    public Weapon weaponInfo;

    [HideInInspector]
    public Transform enemy;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwordCanHit(bool _canHit)
    {
        canHit = _canHit;
        if (!canHit)
        {
            enemy = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enemy == null && canHit && other.transform.tag.Equals("Enemy"))
        {
            enemy = other.transform;
            enemy.SendMessage("TakeDamage", weaponInfo.damage);
        }
    }
}
