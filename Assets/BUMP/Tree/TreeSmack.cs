using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSmack : MonoBehaviour {
    public Animator Anim;
    public int Hits;
	// Use this for initialization
	void Start () {
        Anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnMouseDown()
    {
        Hits++;
        Anim.SetInteger("HitType", Random.Range(0,2));
        Anim.Play("Hit");
        if (Hits > 20f)
        {
            Anim.Play("TreeCut");
            Destroy(this);
            FindObjectOfType<LevelManagerScript>().LoadNextLevel(8f);
        }
        
    }
}
