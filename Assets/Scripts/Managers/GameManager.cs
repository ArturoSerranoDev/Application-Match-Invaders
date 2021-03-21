using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine;

/// <summary> Manages the state of the whole application </summary>
public class GameManager : UnitySingletonPersistent<GameManager>
{
    [SerializeField] private string gameScene;
    [SerializeField] private string homeScene;
    
    public void Play()
    {
        StartCoroutine(LoadScene(gameScene));
    }

    public void GoToHomeMenu()
    {
        StartCoroutine(LoadScene(homeScene));
    }

    private IEnumerator LoadScene(string sceneName)
    {
        Debug.Log("Loading game!");
        yield return new WaitForSeconds(.4f);
        EditorSceneManager.LoadScene(sceneName);
    }
}