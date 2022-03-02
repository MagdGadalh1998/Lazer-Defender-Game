 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public float healthBounes = 50f;
    public float shootPerSecond = 0.5f;
    public float ShootPerSconedBounes = -0.005f;
    public float Projectalspeed = 10;
    public float ProjectalspeedBounes = 5;
    public int scoreValue = 150;
    public float projectileRepateRate = 0.2f;
    
    
    
    public GameObject EnemyLaser;
    public GameObject Explosion, smallEsplosion, PlayerHealth;
    private scoreKepeer ScoreKepeer;
    public AudioClip EnemyFireSound, EnemyDeathSound;
    [Range(1, 100)] public float chanceToDropPowerUp;
    public int[] WavesRequried;
    public Sprite[] enemyShipType;
    int ShipLevel;
    FormationController formationConlrol;
    private void Start()
    {
        formationConlrol = GameObject.FindObjectOfType<FormationController>();
        ScoreKepeer = GameObject.Find("Score").GetComponent<scoreKepeer>();
        CheckWavesCleared();
    }
    private void Update()
    {
        float Prop = Time.deltaTime * shootPerSecond;
        if (Random.value < Prop) { 
        fire();
        }
    }
    void fire()
    {    
        GameObject laser = Instantiate(EnemyLaser, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<projectal>().UpdateLaser(ShipLevel);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -Projectalspeed);
        AudioSource.PlayClipAtPoint(EnemyFireSound, transform.position);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        projectal missile = collision.gameObject.GetComponent<projectal>();
        if (missile)
        {
            health -= missile.GetDamage();
            GameObject newExpl = Instantiate(smallEsplosion, transform.position, Quaternion.identity);
            missile.Hit();
            if (health<=0)
            {
                EnemyDie();
            }
        }
    }
    void EnemyDie()
    {
        RollForPowerUp();
        AudioSource.PlayClipAtPoint(EnemyDeathSound, transform.position);
        GameObject newExpl = Instantiate(Explosion, transform.position, Quaternion.identity)as GameObject;
        ScoreKepeer.ScorePoint(scoreValue);
        Destroy(gameObject);
    }
    void RollForPowerUp()
    {
        int roll = Random.Range(1,101);
        if(roll<=chanceToDropPowerUp)
        {
            GameObject Health = Instantiate(PlayerHealth, transform.position, Quaternion.identity) as GameObject;
            return;
        }

    }
    void CheckWavesCleared()
    {
        int numWavesClear = formationConlrol.numWavesCleared;
        int maxWavesClear = WavesRequried[WavesRequried.Length - 1];
        if (numWavesClear >= maxWavesClear)
        {
            ShipLevel = WavesRequried.Length;
        }
        else { 
        for (int i =0; numWavesClear>=WavesRequried[i]; i++)
        {
            ShipLevel = i + 1;
        }
        }
        HandelShipLevel();
    }
    void HandelShipLevel()
    {
        if (ShipLevel != 0) { 
        Projectalspeed += ProjectalspeedBounes * ShipLevel;
        shootPerSecond += ShootPerSconedBounes * ShipLevel;
        health += healthBounes * ShipLevel;
            this.GetComponent<SpriteRenderer>().sprite = enemyShipType[ShipLevel - 1];
        }
       
    }
}
