using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary> Manages the state of the whole application </summary>
public class GameManager : SingletonPersistent<GameManager>
{
    [SerializeField] private string gameScene;
    [SerializeField] private string homeScene;

    public delegate void OnGameFinished();
    public static event OnGameFinished onGameFinished;
    
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