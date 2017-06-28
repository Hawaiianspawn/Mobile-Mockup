using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseDrag : MonoBehaviour {
    public bool Holding;
    protected bool hasAnimator;
	// Use this for initialization
	void Start () {
        if (GetComponent<Animator>())
            hasAnimator = true;

	}
	
	// Update is called once per frame
	void Update () {
        //BEGAN
        if (Input.GetMouseButtonDown(0))
        {
            Holding = true;
        }
        if (Input.GetMouseButtonUp(0))
            Holding = false;

        if (Holding)
        {
            if (hasAnimator)
                GetComponent<Animator>().SetBool("Grabbed", true);
            this.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, this.transform.position.z);
        }
    }
}
