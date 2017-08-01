using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagerScript : MonoBehaviour
{
    public string[] Levels;
    public int index; //insert arrays starts at one joke
    public TransitionManager TranisitionRef;

    void Start()
    {
        TranisitionRef = GetComponent<TransitionManager>();
    }
    public void LoadLevel(int i)
    {
        TranisitionRef = GetComponent<TransitionManager>();
        StartCoroutine(WaitNLoad(3f, i));
    }

    public void LoadBumpTransition()
    {
        TranisitionRef = GetComponent<TransitionManager>();
        SceneManager.LoadScene(Levels[0]);
    }
    
    public void LoadNextLevel()
    {
        TranisitionRef = GetComponent<TransitionManager>();
        StartCoroutine(WaitNLoad(3f, ++index));
    }
    public void LoadNextLevel(float _time)
    {
        TranisitionRef = GetComponent<TransitionManager>();
        StartCoroutine(WaitNLoad(_time, ++index));
    }
    public IEnumerator WaitNLoad(float _time, int i)
    {
        TranisitionRef.startTranisiton();
        yield return new WaitForSeconds(_time);
        SceneManager.LoadScene(Levels[i]);
    }
}
