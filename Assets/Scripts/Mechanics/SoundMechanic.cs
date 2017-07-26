using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class SoundMechanic : MonoBehaviour {


	protected float completion;
	public bool muteToWin;
	public Image Background; 
	public float Ratio;
	public Text DebugText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
      //  SendMessage("VolumeUp");
      //   SendMessage("VolumeDown");
	}

	public void volumeChange(float value){
		Background.color = new Color(255 * value, 0, 0, 1);
	}
public void volumeUp(string value){
		//completion  = value;
		print (value);
		if (!muteToWin && completion > .9f)
			print ("win");
}

public void volumeDown(string value){
	//completion = value;
	if(muteToWin && completion < .1f)
		print("win");

}
}
