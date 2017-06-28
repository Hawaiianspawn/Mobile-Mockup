using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVMoveScript : MonoBehaviour {

    public float ScrollSpeed;
    public bool Randomize;
    private float Offset;
    private float waveMath;
    private Renderer RenderUV;
    void Start()
    {
        RenderUV = GetComponent<Renderer>();
    }
	// Update is called once per frame
	void Update ()
    {
        //float RandomRatio = 1;
        if (Randomize)
            //RandomRatio = Random.Range(.5f,1.5f);            
        Offset += (Time.deltaTime * ScrollSpeed) / 10;
        RenderUV.material.SetTextureOffset("_MainTex", new Vector2(0, Offset));
    }
}
