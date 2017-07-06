using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVMoveScript : MonoBehaviour {

    public float ScrollSpeed;
    public bool Randomize;
    private float RandomRatio = 1f;
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
        {
            RandomRatio = (Mathf.Sin(Time.time));
            if (RandomRatio < 0)
                RandomRatio = -(RandomRatio);
            RandomRatio += .1f;
        }           
        Offset += ((Time.deltaTime * ScrollSpeed) / 10) * RandomRatio;
        RenderUV.material.SetTextureOffset("_MainTex", new Vector2(0, Offset));
    }
}
