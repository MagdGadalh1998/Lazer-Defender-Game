﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestract : MonoBehaviour
{
    public float destroyAfter = 3f;
    void Start()
    {
        Destroy(gameObject, destroyAfter);
    }
}
