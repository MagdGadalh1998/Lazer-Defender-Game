using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectal : MonoBehaviour
{
    public float damage = 100;
    public Sprite[] LaserColorArray;
    public float frindelyDamgeBonus = 50f;
    public float enemyDamageBounes = 50f;

    public float GetDamage()
    {
        return damage;
    }
    public void Hit()
    {
        Destroy(gameObject);
    }
    public void UpdateLaser(int ShipLevel)
    {
        if (ShipLevel != 0) { this.GetComponent<SpriteRenderer>().sprite = LaserColorArray[ShipLevel - 1]; }
        if (CompareTag("EnemyLaser")) { damage += enemyDamageBounes * ShipLevel; }
        if (CompareTag("PlayerLaser")) { damage += frindelyDamgeBonus * ShipLevel; }
    }
 }
