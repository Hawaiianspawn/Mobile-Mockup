using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RandomizeBump : MonoBehaviour {
    string[] Names = new string[] { "Bill", "Jane", "Bruce", "Gill", "Foster", "James", "Dante", "Leo", "Janice", "Yennefer", "Morrigan", "Geralt", "John", "Jon", "Lilith", "Bison", "Andy" }; 
	// Use this for initialization
	void Start () {
        GameObject.Find("BumpText").GetComponent<Text>().text = "You have been bumped by <b>" + Names[Random.Range(0, Names.Length)] + " </b>\n" + "Press here to bump them back!";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
