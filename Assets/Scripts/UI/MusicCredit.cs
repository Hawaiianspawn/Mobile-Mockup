using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicCredit : MonoBehaviour {
    public Text CreditText;
	// Use this for initialization
	void Start () {
        CreditText.text = "Music By: " +  GetComponent<AudioSource>().clip.name;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
