using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float Xmin = 8, Xmax = -8;
    float padding = 1;
    healthBar helBar;
    ShipLevel shipLevel;
    public Sprite[] PlayerShipArray;
    public GameObject levelUpPartical ;
    public GameObject laserPrefab;
    public GameObject Explosion, SmallExplosion;
    public float ProjectileSpeedBounes = 5;
    public float fireringRate = 0.2f;
    public float firingRateBounes = 0.005f;
    public float HealthBounes = 50;
    public float moveSpeed;
    public float projectilSpeed = 10;
    public float projectileRepateRate = 0.2f;
    public float health = 100f;
    public float MaxHealth = 250f;
    public AudioClip fireSound;
    public AudioClip LevelUpSound;
    public AudioClip DeathSound;
    public int[] LevelUpScoreRequ;
   
    int currentLevel;
    
    void Start()
    {

        MaxHealth = health;
        helBar = GameObject.FindObjectOfType<healthBar>();
        helBar.setHealth(health);
        shipLevel = GameObject.FindObjectOfType<ShipLevel>();
        CollectTheBoundariesOfScreen();
       
    }

    // Update is called once per frame
    void Update()
    {

        MoveShip();
    }
    void FireLaser()
    {
        Vector3 offset = new Vector3(0, 1, 0);
        GameObject beam = Instantiate(laserPrefab, transform.position+offset, Quaternion.identity) as GameObject; //Instantiate the laser
        beam.GetComponent<projectal>().UpdateLaser(currentLevel);
        beam.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectilSpeed); //shoot the Laser
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }
    public void MoveShip()
    {
     if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x - moveSpeed * Time.deltaTime, Xmin, Xmax), transform.position.y, transform.position.z);
        }
     else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x + moveSpeed * Time.deltaTime, Xmin, Xmax), transform.position.y, transform.position.z);
        }
     //check if keyDown or KeyUp to fire Laser
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("FireLaser", 0.001f, projectileRepateRate);  
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("FireLaser");
        }

    }
    public void CollectTheBoundariesOfScreen()
    {
        Camera Cam = Camera.main;
        float distance = transform.position.z - Cam.transform.position.z;
        Xmin = Cam.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + padding;//CollectTheBoundariesOfScreen from (x,y)
        Xmax = Cam.ViewportToWorldPoint(new Vector3(1, 1, distance)).x - padding;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        projectal missile = collision.gameObject.GetComponent<projectal>();
        if (missile)
        {
            GameObject smallExp = Instantiate(SmallExplosion, transform.position, Quaternion.identity) as GameObject;
            health -= missile.GetDamage();
            missile.Hit();
            helBar.setHealth(health);
            if (health <= 0)
            {
                PlayerDie();
            }
        }
    }
    void PlayerDie()
    {
        AudioSource.PlayClipAtPoint(DeathSound, transform.position);
        GameObject Exp = Instantiate(Explosion, transform.position, Quaternion.identity) as GameObject;
        gameObject.SetActive(false);
        
        Invoke("DeathDelay", 3f);

        
    }
    void DeathDelay()
    {
        levelManger lev = GameObject.Find("LevelManger").GetComponent<levelManger>();
        lev.LoadGame("win");
    }
    public void RecoverHealth(float ToRecover)
    {
        float healthRecovery = ToRecover *= health;
        health += healthRecovery;
        if (health >= MaxHealth) { health = MaxHealth; }
        helBar.setHealth(health); 

    }
    public void CheckForLevelUp(int Score)
    {
        int MaxLevel = LevelUpScoreRequ.Length;
        if (currentLevel!=MaxLevel&&Score>=LevelUpScoreRequ[currentLevel])
        {
            HandelLevelUp();
        }
        else if (currentLevel == MaxLevel) { Debug.Log("This is max Level"); }
    }
    void HandelLevelUp()
    {
        this.GetComponent<SpriteRenderer>().sprite = PlayerShipArray[currentLevel];
        currentLevel++;
        GameObject LevelUpEffect = Instantiate(levelUpPartical, transform.position, Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(LevelUpSound, transform.position);
        shipLevel.UpdateShipLevel(currentLevel);
        projectilSpeed += ProjectileSpeedBounes;
        fireringRate += firingRateBounes;
        MaxHealth += HealthBounes;
        health = MaxHealth;
        helBar.settingMaxHealth = true;
        helBar.setHealth(health);
    }

    }
