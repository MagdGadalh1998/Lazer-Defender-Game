using System.Collections;   
using System.Collections.Generic;
using UnityEngine;

public class healthPack : MonoBehaviour
{
    public Sprite[] healthPackType;
    [Range(1, 100)] public int ChanceBlue, ChanceRed;
    [Range(0.01f, 1)] public float greenRecovery, blueRecovery, redRecovery;
    float RecoveryPresent;
    PlayerController PlayerControl;
    void Start()
    {
        PlayerControl = GameObject.FindObjectOfType<PlayerController>();
        RondomHelthpack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            PlayerControl.RecoverHealth(RecoveryPresent);
            Destroy(gameObject); 
        }
    }
    void RondomHelthpack()
    {
        int roll = Random.Range(1, 101);
        if (roll<=ChanceRed)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = healthPackType[1];
            RecoveryPresent = redRecovery;
            return;
        }
        if (roll <= ChanceBlue)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = healthPackType[0];
            RecoveryPresent = blueRecovery;
            return;
        }
        RecoveryPresent = greenRecovery;
    }
}
