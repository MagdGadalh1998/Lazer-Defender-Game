using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShipLevel : MonoBehaviour
{
    Text myText;
    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<Text>();
    }

    // Update is called once per frame
   public void UpdateShipLevel(int currentLevel)
    {
        myText.text = "Level : " + currentLevel + " /6";
    }
}
