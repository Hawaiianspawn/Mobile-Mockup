﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSmack : MonoBehaviour {
    public Animator Anim;
	public GameObject ObjectToBeHit;
	public string ObjectName;
    public int Hits;
    public GameObject SmashParticle;


	private float dist;
	private bool dragging = false;
	private Vector3 offset;


	// Use this for initialization
	void Start () {
        Anim = GetComponent<Animator>();
		ObjectToBeHit = this.gameObject.transform.GetChild(0).gameObject;
		ObjectName = ObjectToBeHit.name;
	}
	
	// Update is called once per frame
	void Update () {

        // Code for OnMouseDown in the iPhone. Unquote to test.
        RaycastHit hit = new RaycastHit();
        for (int i = 0; i < Input.touchCount; ++i) {

            if (Input.GetTouch(i).phase.Equals(TouchPhase.Began)) {
					// Construct a ray from the current touch coordinates
					Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
					if (Physics.Raycast(ray, out hit)) {
					Vector3 direction = hit.transform.position - hit.point;
                    if (hit.collider.gameObject)
                    {
                        hit.rigidbody.AddForceAtPosition(direction.normalized * 500f, hit.point);
                        Instantiate(SmashParticle, hit.point, Quaternion.LookRotation(hit.point.normalized));   //hit.transform.gameObject.SendMessage("OnMouseDown");
                        if (++Hits > 20f)
                        {
                            Destroy(this.transform.GetChild(0).gameObject.GetComponent<CharacterJoint>());
                            FindObjectOfType<LevelManagerScript>().LoadLevel(0);
                            Destroy(this.GetComponent<TreeSmack>());

                        }
                    }

                }
				}
			}
		}
    void OnMouseDown()
    {
        Hits++;

        //Anim.SetInteger("HitType", Random.Range(0,2));
        //Anim.Play("Hit");
        if (Hits > 20f)
        {
            Anim.Play("TreeCut");

        }


    }
    IEnumerator WaitNLoad()
    {
        yield return new WaitForSeconds(.2f);
        Destroy(this.GetComponent<TreeSmack>());

    }
}
