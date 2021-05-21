using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static event Action GameOver;

    [SerializeField] GameObject[] enemyPieces;
    
    [SerializeField] GameObject pauseMenu, gameOverMenu;
    [SerializeField] float spawnPeriod = 2.0f;
    [SerializeField] float spawnPeriodMin = 0.9f;
    [SerializeField] float spawnPeriodDecrement = 0.05f;
    [SerializeField] float decreasePeriod = 2.0f;
    [SerializeField] AudioClip gameOverAudio;
    AudioSource audioSource;
    float[] xPositions = { -3.5f, -2.5f, -1.5f, -0.5f, 0.5f, 1.5f, 2.5f, 3.5f };
    Vector3 position = new Vector3(0.5f, 0.0f, 3.5f);


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Invoke("SpawnEnemy", spawnPeriod);
        InvokeRepeating("LowerSpawnPeriod", decreasePeriod, decreasePeriod);
        GameOver += StopSpawning;
        GameOver += InvokeGameOverMenu;
    }

    // Update is called once per frame
    void Update()
    {
        // Pause or unpause when ESC is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

    }

    private void OnDestroy()
    {
        GameOver -= StopSpawning;
        GameOver -= InvokeGameOverMenu;
    }

    #region Custom Methods

    // notify all subscribers when the game is over
    public static void CallGameOver()
    {
        GameOver?.Invoke();
    }

    // Pause and unpause the game
    public void TogglePause()
    {
        if (Time.timeScale > 0.0f)
        {
            Time.timeScale = 0.0f;
        } else
        {
            Time.timeScale = 1.0f;
        }

        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }


    // Spawn random enemy pieces at a random X position
    private void SpawnEnemy()
    {
        position.x = xPositions[UnityEngine.Random.Range(0, xPositions.Length)];
        Instantiate(enemyPieces[UnityEngine.Random.Range(0, enemyPieces.Length)], position, Quaternion.identity);
        Invoke("SpawnEnemy", spawnPeriod);
    }

    // Stop spawning enemies
    private void StopSpawning()
    {
        CancelInvoke("SpawnEnemy");
    }

    // Increase difficulty by spawning enemies more frequently
    private void LowerSpawnPeriod()
    {
        if (spawnPeriod > spawnPeriodMin)
        {
            spawnPeriod -= spawnPeriodDecrement;
            Debug.Log("Difficulty increased. New spawn period:" + spawnPeriod);
        }
    }

    void InvokeGameOverMenu ()
    {
        Invoke("ShowGameOverMenu", 1.5f);
    }

    void ShowGameOverMenu()
    {
        gameOverMenu.gameObject.SetActive(true);
    }

    

    #endregion Custom Methods

}
