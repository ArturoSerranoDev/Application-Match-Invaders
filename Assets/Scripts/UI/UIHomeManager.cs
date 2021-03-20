using System.Collections;
using System.Collections.Generic;
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
