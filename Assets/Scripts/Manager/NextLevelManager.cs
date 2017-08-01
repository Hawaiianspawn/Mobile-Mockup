using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelManager : MonoBehaviour {

void CallNextLevel()
    {
        FindObjectOfType<LevelManagerScript>().LoadNextLevel();
    }
}
