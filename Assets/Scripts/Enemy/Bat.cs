using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public void AttackEnd()
    {
        transform.parent.GetComponent<EnemyController>().AttackEnd();
    }
    
    public void Hit()
    {
        transform.parent.GetComponent<EnemyController>().OnHit();
    }
}
