using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour {

   // private Shadow ShadowRef;
    public GameObject WhiteOutRef;
    public string Level;
    // Use this for initialization
    void Start()
    {
       // ShadowRef = FindObjectOfType<Shadow>();
        //WhiteOutRef = GameObject.Find("WhiteOut");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            print("Level Load");
            StartCoroutine(NextLevel());
        }
    }
    IEnumerator NextLevel()
    {
        print(Time.time);
        WhiteOutRef.SetActive(enabled);
        yield return new WaitForSeconds(4);
        print(Time.time);
        // Application.LoadLevel(1);
    }
}
