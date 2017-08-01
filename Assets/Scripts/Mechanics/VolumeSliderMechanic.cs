using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VolumeSliderMechanic : MonoBehaviour {

    public Animator anim;
    public Slider slider;   //Assign the UI slider of your scene in this slot 

    // Use this for initialization
    void Start()
    {
        anim.speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value == 1)
            WinState();
        anim.Play("OperaCurtains", -1, slider.value);
        
    }

    public void WinState()
    {
        FindObjectOfType<LevelManagerScript>().index = 0;
        StartCoroutine(WaitNLoad());
    }
    IEnumerator WaitNLoad()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("TitleScreen");

    }
}

