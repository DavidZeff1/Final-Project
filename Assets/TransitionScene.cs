using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScene : MonoBehaviour
{
    [SerializeField] TMP_Text m_CountdownText;

    private void Start()
    {
        GameEventSystem.OnEnemyBossDeath += HandleLoadNextScene;
    }

    private void OnDestroy()
    {
        GameEventSystem.OnEnemyBossDeath -= HandleLoadNextScene;
    }

    private void HandleLoadNextScene()
    {
        StartCoroutine(LoadNextScene());
    }

    private IEnumerator LoadNextScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        string nextSceneName;
        
        switch (currentSceneName)
        {
            case "Level 1 Scene":
                nextSceneName = "Level 2 scene";
                break;
            case "Level 2 scene":
                nextSceneName = "Level 3 Scene";
                break;
            case "Level 2 Scene":
                nextSceneName = "Level 3 Scene";
                break;
            case "Level 3 Scene":
                nextSceneName = "Victory Scene";
                break;
            default:
                yield break;  
        }

        yield return new WaitForSeconds(2f); 

        SceneManager.LoadScene(nextSceneName);
    }

}
