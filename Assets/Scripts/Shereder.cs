using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shereder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);//to destroy laser when cross the boundry of the screen
    }
}
