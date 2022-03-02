using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WavesCleaerd : MonoBehaviour
{
    Text WavesClear;
    void Start()
    {
        WavesClear = GetComponent<Text>();
    }

    // Update is called once per frame
    public void UpdateWavesCleared(int NumWavesClear)
    {
        WavesClear.text = "Waves Cleared : " + NumWavesClear;
    }
}
