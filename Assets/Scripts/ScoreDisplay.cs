using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
     Text myText = GetComponent<Text>();
     myText.text = scoreKepeer.score.ToString();
        scoreKepeer.reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
