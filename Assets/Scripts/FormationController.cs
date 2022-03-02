using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour
{
    public  GameObject enemyPrefab;
    public float width = 10;
    public float hight = 5;
    public float padding = 1;
    public float direction=1;
    public float speed = 8;
    public float spawnDelaySeconds =1;
    float boundaryRightEdge, boundaryLeftEdge;
    [HideInInspector] public int numWavesCleared;
    WavesCleaerd wavesCleard;
    Enemy enemy; 


    // Start is called before the first frame update
    void Start()
    {
        CollectTheBoundariesOfScreen();
        SpwanEnemy();
        enemy = GameObject.FindObjectOfType<Enemy>();
        wavesCleard=GameObject.FindObjectOfType<WavesCleaerd>();
    }
    void CollectTheBoundariesOfScreen()
    {
        Camera Cam = Camera.main;
        float distance = transform.position.z - Cam.transform.position.z;
        boundaryLeftEdge = Cam.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + padding;//CollectTheBoundariesOfScreen from (x,y)
        boundaryRightEdge = Cam.ViewportToWorldPoint(new Vector3(1, 1, distance)).x - padding;

    }
    public void OnDrawGizmos()
    {
        float Xmin, Xmax, Ymin, Ymax;
        Xmin = transform.position.x - 0.5f * width;
        Xmax = transform.position.x + 0.5f * width;
        Ymin = transform.position.x - 0.5f * hight;
        Ymax = transform.position.x + 0.5f * hight;
        Gizmos.DrawLine(new Vector3(Xmin, Ymin, 0), new Vector3(Xmin, Ymax));
        Gizmos.DrawLine(new Vector3(Xmin, Ymax, 0), new Vector3(Xmax, Ymax));
        Gizmos.DrawLine(new Vector3(Xmax, Ymax, 0), new Vector3(Xmax, Ymin));
        Gizmos.DrawLine(new Vector3(Xmax, Ymin, 0), new Vector3(Xmin, Ymin));
    }

    // Update is called once per frame
    void Update()
    {
        float formationRightEdge = transform.position.x + 0.5f * width;
        float formatioLeftEdge = transform.position.x - 0.5f * width;
        if (formationRightEdge>boundaryRightEdge)
        {
            direction = -1;
        }
        if (formatioLeftEdge<boundaryLeftEdge)
        {
            direction = 1;
        }
        transform.position += new Vector3(direction * speed * Time.deltaTime, 0, 0);
        if (AllEnemeisAreDead())
        {
            numWavesCleared++;
            wavesCleard.UpdateWavesCleared(numWavesCleared);
            spwanUntilFull();
        }
    } 
    
    void SpwanEnemy()
    {
        foreach (Transform position in transform)
        {
            GameObject Enemy = Instantiate(enemyPrefab, position.transform.position, Quaternion.identity) as GameObject;
            Enemy.transform.parent = position;
        }
    }
    void spwanUntilFull()
    {
        Transform freePos = NextFreePosition();
        GameObject Enemy = Instantiate(enemyPrefab, freePos.position, Quaternion.identity) as GameObject;
        Enemy.transform.parent = freePos;
        if (freePositionExists())
        {
            Invoke("spwanUntilFull", spawnDelaySeconds);
        }

    }
    bool freePositionExists()
    {
        foreach (Transform position in transform)
        {
            if (position.childCount <= 0)
            {
                return true;
            }
        }
        return false;
    }
    Transform NextFreePosition()
    {
        foreach (Transform position in transform)
        {
            if (position.childCount <= 0)
            {
                return position;
            }
        }
        return null;
    }
    bool AllEnemeisAreDead()
    {
        foreach(Transform position in transform)
        {
            if (position.childCount>0)
            {
                return false;
            }
        }
        return true;
    }
}
