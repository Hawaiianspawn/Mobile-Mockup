using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagerScript : MonoBehaviour {
    public string[] Levels;
    public int index;
    public void LoadLevel(int i)
    {
        SceneManager.LoadScene(Levels[i]);
    }

    public void LoadNextLevel()
    {
        StartCoroutine(WaitNLoad(3f));
    }
    public void LoadNextLevel(float _time)
    {
        StartCoroutine(WaitNLoad(_time));
    }
    public IEnumerator WaitNLoad(float _time)
    {
        yield return new WaitForSeconds(_time);
        SceneManager.LoadScene(Levels[index]);
    }
}
