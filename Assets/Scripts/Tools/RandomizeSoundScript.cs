using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeSoundScript : MonoBehaviour {
    public AudioSource SoundPlayer;
    public float lowPitchRange, highPitchRange;
    public AudioClip[] Clips;

	// Use this for initialization
	void Start () {
        //Generate a random number between 0 and the length of our array of clips passed in.
        //int randomIndex = Random.Range(0, Sounds.Length);


        //Choose a random pitch to play back our clip at between our high and low pitch ranges.
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        //Set the pitch of the audio source to the randomly chosen pitch.
        SoundPlayer.pitch = randomPitch;
        if (Clips.Length > 0)
            SoundPlayer.clip = Clips[Random.Range(0, Clips.Length - 1)];
        SoundPlayer.Play();

    }

    // Update is called once per frame
    void Update () {
		
	}
}
