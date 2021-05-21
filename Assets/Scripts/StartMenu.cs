using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{

    [SerializeField] Button playButton;

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(LoadPlayScene);
    }

    private void LoadPlayScene()
    {
        SceneManager.LoadScene("Play");
    }

}
