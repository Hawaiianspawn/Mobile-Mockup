using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour {
    protected Animator Anim;
    protected SpriteRenderer[] Children;
    bool OnOff;
	// Use this for initialization
	void Start () {
      //  DontDestroyOnLoad(this.gameObject);
        Anim = GetComponent<Animator>();
        Children = this.GetComponentsInChildren<SpriteRenderer>();
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
    void startTranisiton()
    {
        OnOff = true;

        foreach (SpriteRenderer Sprite in Children)
            if (!Sprite.enabled)
                Sprite.enabled = true;
        Anim.Play("Close");
        Anim.SetFloat("_speed", 1f);
    }
    void endTransition()
    {

        OnOff = false;
        Anim.SetFloat("_speed", -2.5f);
        StartCoroutine(waitAndHide());
    }

    IEnumerator waitAndHide()
    {
        if(Children[0].enabled)
            yield return new WaitForSeconds(3f);
        foreach (SpriteRenderer Sprite in Children)
            if (Sprite.enabled)
                Sprite.enabled = false;

    }
}
