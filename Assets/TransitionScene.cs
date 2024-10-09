using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScene : MonoBehaviour
{
    [SerializeField] TMP_Text m_CountdownText;

    void Update()
    {
        if (m_CountdownText.text == "Boss Killed, Level Completed!")
        {
            StartCoroutine(LoadNextScene());
        }

       
    }
    private IEnumerator LoadNextScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        string nextSceneName;
        switch (currentSceneName)
        {
            case "Level 1 Scene":
                nextSceneName = "Level 2 Scene";
                break;
            case "Level 2 Scene":
                nextSceneName = "Level 3 Scene";
                break;
            case "Level 3 Scene":
                nextSceneName = "Victory Scene";
                break;
            default:
                Debug.LogWarning("No next scene defined for this level.");
                yield break;  
        }

        yield return new WaitForSeconds(2f); 

        SceneManager.LoadScene(nextSceneName);
    }

}
