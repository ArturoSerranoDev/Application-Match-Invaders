using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHomeManager : MonoBehaviour
{
    [SerializeField] Text highScoreText;
    // Start is called before the first frame update
    void Start()
    {
        if (!SaveLoadController.Instance.DoesSaveFileExist())
            return;
        
        highScoreText.text = "Latest HighScore: " + SaveLoadController.Instance.GetLatestHighScore();
    }
}
