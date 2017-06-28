using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderWin : MonoBehaviour {
    public Vector3 LockOffset; //The location of the object when its won and locked down
    void OnCollisionEnter2D(Collision2D WinArea)
    {

        if(WinArea.collider.gameObject.tag == "WinState")
        {
            print("hit");
            if (GetComponent<mouseDrag>())
            {
                GetComponent<mouseDrag>().Holding = false;
                Destroy(GetComponent<mouseDrag>());
            }
            transform.position = LockOffset;
            if(GetComponent<Rigidbody2D>())
                Destroy(GetComponent<Rigidbody2D>());
            if(GetComponent<BoxCollider2D>())
                Destroy(GetComponent<BoxCollider2D>());
            if (GetComponent<Animator>())
                GetComponent<Animator>().SetBool("Win", true);
        }
    }
}
