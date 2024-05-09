using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public float damage;
    public int rate;
    public string weaponName;
    public int durability;
    public Transform hitEffect;
}
