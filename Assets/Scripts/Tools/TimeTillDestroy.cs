//Destroy object based on a timer with range of 10 seconds.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTillDestroy : MonoBehaviour {
    [Range(0, 10f)]
    public float Time;
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, Time);
	}
    
}
