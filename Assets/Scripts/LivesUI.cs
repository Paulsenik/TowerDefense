﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{

    public Text livesText;

    void Start()
    {
        
    }

    void Update()
    {
        livesText.text = PlayerStats.lives + " LIVES";
    }
}
