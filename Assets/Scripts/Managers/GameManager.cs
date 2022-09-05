using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    static GameManager _instance = null;

    public static GameManager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }


    public int _lives = 3;

    public int lives
    {
        get { return _lives; }
        set 
        {
            if (_lives > value )
            {
                Respawn();
            }

            _lives = value;

            if (_lives > maxLives)
            {
                _lives = maxLives;
            }
        }
    }

    public int maxLives = 3;

    public PlayerController playerPrefab;
    [HideInInspector] public PlayerController playerInstance;
    [HideInInspector] public Transform currentSpawnPoint;
    [HideInInspector] public Level currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "Level")
            {
                SceneManager.LoadScene("Title");
            }
            else
            {
                SceneManager.LoadScene("Level");
            }
        }
    }

    void GameOver()
    {

    }

    public void SpawnPlayer(Transform spawnLocation)
    {
        playerInstance = Instantiate(playerPrefab, spawnLocation.position, spawnLocation.rotation);
        currentSpawnPoint = spawnLocation;
    }

    void Respawn()
    {
        playerInstance.transform.position = currentSpawnPoint.position;
    }
}
