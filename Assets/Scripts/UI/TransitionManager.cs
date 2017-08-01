using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour {
    protected Animator Anim;
    protected SpriteRenderer[] Children;
    public GameObject TransitionLocation;
    public bool Transitioning;
    bool OnOff;
	// Use this for initialization
	void Start () {
      //  DontDestroyOnLoad(this.gameObject);
        Anim = TransitionLocation.GetComponent<Animator>();
        Children = TransitionLocation.GetComponentsInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump")) {
            if (OnOff)
                endTransition();
            else
                startTranisiton();
                }
	}
    public void startTranisiton()
    {
        Instantiate(TransitionLocation);
  //
        //foreach (SpriteRenderer Sprite in Children)
        //    if (!Sprite.enabled)
        //        Sprite.enabled = true;
       // Anim.Play("CurtainTransition");
      //  Anim.SetFloat("_speed", 1f);
    }
    public void endTransition()
    {

       OnOff = false;
        
        //Anim.SetFloat("_speed", -1.5f);
       // Anim.Play("CurtainTransition", 0, 1f);
    }

    
}
