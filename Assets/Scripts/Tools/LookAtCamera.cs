﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {
    public Transform target;
	// Use this for initialization
	void Start () {
        if (!target)
            target = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(target);
    }
}
