﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadHelpPicture : MonoBehaviour {

    public GameObject panel;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Help()
    {
        panel.SetActive(true);
    }
}
