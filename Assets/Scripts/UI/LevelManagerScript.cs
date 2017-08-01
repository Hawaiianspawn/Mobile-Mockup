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
        TranisitionRef.startTranisiton();
        StartCoroutine(WaitNLoad(2f, i));
    }

    public void LoadBumpTransition()
    {
        SceneManager.LoadScene(Levels[0]);
    }
    
    public void LoadNextLevel()
    {
        StartCoroutine(WaitNLoad(2f, ++index));
    }
    public void LoadNextLevel(float _time)
    {
        StartCoroutine(WaitNLoad(_time, ++index));
    }
    public IEnumerator WaitNLoad(float _time, int i)
    {
        TranisitionRef.startTranisiton();
        yield return new WaitForSeconds(_time);
        SceneManager.LoadScene(Levels[i]);
        TranisitionRef.startTranisiton();
    }
}
