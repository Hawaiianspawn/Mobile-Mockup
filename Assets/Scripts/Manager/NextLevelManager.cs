using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelManager : MonoBehaviour {

public void CallNextLevel()
    {
        FindObjectOfType<LevelManagerScript>().LoadNextLevel();
    }
}
