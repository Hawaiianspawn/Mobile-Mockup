using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendToSoundManager : MonoBehaviour {
    public AudioClip Sound;
	// Use this for initialization
	void Awake () {
        FindObjectOfType<SoundManager>().PlaySingle(Sound, true);
	}
}
