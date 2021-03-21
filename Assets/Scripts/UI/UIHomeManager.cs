// ----------------------------------------------------------------------------
// UIHomeManager.cs
//
// Author: Arturo Serrano
// Date: 20/02/21
//
// Brief: Controls UI elements inside Home Scene
// ----------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

public class UIHomeManager : MonoBehaviour
{
    [SerializeField] Text highScoreText;
    [SerializeField] Button playButton;
    
    void OnEnable()
    {
        playButton.onClick.AddListener(GameManager.Instance.Play);
        
        if (!SaveLoadController.Instance.DoesSaveFileExist())
            return;
        
        highScoreText.text = "Latest HighScore: " + SaveLoadController.Instance.GetLatestHighScore();
    }
}
