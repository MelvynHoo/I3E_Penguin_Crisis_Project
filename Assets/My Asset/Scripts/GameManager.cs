using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// The prefab of the player used for spawning.
    /// </summary>
    public GameObject playerPrefab;

    /// <summary>
    /// Store the active player in the game.
    /// </summary>
    private Player activePlayer;

    /// <summary>
    /// Store the active GameManager.
    /// </summary>
    public static GameManager instance;

    /// <summary>
    /// True when the game is paused.
    /// </summary>
    private bool gamePaused;

    /// <summary>
    /// The pause menu of the game.
    /// </summary>
    public GameObject pauseMenu;

    /// <summary>
    /// The respawn menu of the game.
    /// </summary>
    public GameObject respawnMenu;

    /// <summary>
    /// The end menu of the game.
    /// </summary>
    public GameObject completeMenu;

    #region Objective Related Variables
    /// <summary>
    /// Generator Room Keeping track of many generator is destroy.
    /// </summary>
    public int generatorCount = 0;
    public int pipesCount = 0;
    public int computerCount = 0;
    public TextMeshProUGUI NoGenerator;
    public TextMeshProUGUI NoPipes;
    public TextMeshProUGUI NoComputer;
    #endregion
    private void Awake()
    {
        // Check whether there is an instance
        // Check whether the instance is me
        if(instance != null && instance != this)
        {
            // If true, I'm not needed and can be destroyed.
            Destroy(gameObject);
        }
        // If not, set myself as the instance
        else
        {
            //Set the GameManager to not be destroyed when scenes are loaded.
            DontDestroyOnLoad(gameObject);

            // Subscribe the spawning function to the activeSceneChanged event.
            SceneManager.activeSceneChanged += SpawnPlayerOnLoad;

            // Set myself as the instance
            instance = this;
        }
    }

    /// <summary>
    /// Spawn the player when the scene changes
    /// </summary>
    /// <param name="currentScene"></param>
    /// <param name="nextScene"></param>
    void SpawnPlayerOnLoad(Scene currentScene, Scene nextScene)
    {
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            // Checking if there is any active player in the game.
            if(activePlayer == null)
            {
                // Find the spawn spot
                PlayerSpawnSpot playerSpot = FindObjectOfType<PlayerSpawnSpot>();

                // If there is no player, I should spawn one.
                GameObject newPlayer = Instantiate(playerPrefab, playerSpot.transform.position, playerSpot.transform.rotation);

                // Store the active player.
                activePlayer = newPlayer.GetComponent<Player>();
            }
            // If there is already a player, position the player at the right spot.
            else
            {
                // Find the spawn spot
                PlayerSpawnSpot playerSpot = FindObjectOfType<PlayerSpawnSpot>();

                // Position and rotate the player
                activePlayer.transform.position = playerSpot.transform.position;
                activePlayer.transform.rotation = playerSpot.transform.rotation;
            }
        }
    }

    /// <summary>
    /// Toggles the pause state of the game.
    /// </summary>
    public void TogglePause()
    {
        // Check if the game is not paused
        if(!gamePaused)
        {
            // If the game is not paused, pause it by setting timeScale to 0
            Time.timeScale = 0f;
            gamePaused = true;
            pauseMenu.SetActive(gamePaused);

        }
        else
        {
            // If the game is paused, unpause it
            Time.timeScale = 1f;
            gamePaused = false;
            pauseMenu.SetActive(gamePaused);
        }
    }

    /// <summary>
    /// Starts the game
    /// </summary>
    public void StartGame()
    {
        LoadScene(1);
    }

    /// <summary>
    /// Ends the game
    /// </summary>
    public void EndGame()
    {
        Destroy(activePlayer.gameObject);
        LoadScene(0);
    }

    /// <summary>
    /// Restarts the game
    /// </summary>
    public void RestartGame()
    {
        activePlayer.ResetPlayer();
        ToggleRespawnMenu();
        StartGame();
        generatorCount = 0;
        NoGenerator.text = "Destroyed Generator: " + generatorCount.ToString();
    }

    /// <summary>
    /// Loads the scene specified by <paramref name="sceneIndex"/>
    /// </summary>
    /// <param name="sceneIndex">The index of the scene to load</param>
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    /// <summary>
    /// Toggles the respawn menu
    /// </summary>
    public void ToggleRespawnMenu()
    {
        respawnMenu.SetActive(!respawnMenu.activeInHierarchy);
    }

    /// <summary>
    /// Shows the end menu
    /// </summary>
    public void ShowCompleteMenu()
    {
        completeMenu.SetActive(true);
    }
    public void BossPenguinDialogue()
    {
        Debug.Log("Boss penguin talking");
    }

    public void TrackGenerator(int destroyedGens)
    {
        generatorCount += destroyedGens;
        //Debug.Log("No of Generator Destroyed" + noOfGenerator);
        if (generatorCount < 3)
        {
            NoGenerator.text = "Destroyed Generator: " + generatorCount.ToString();
        }
        else
        {
            NoGenerator.text = "Destroyed Generator: Completed";
        }
    }
    public void TrackComputer(int destroyedComputer)
    {
        computerCount += destroyedComputer;
        //Debug.Log("No of Generator Destroyed" + noOfGenerator);
        if (computerCount < 6)
        {
            NoComputer.text = "Destroyed Computer: " + computerCount.ToString();
        }
        else
        {
            NoComputer.text = "Destroyed Computer: Completed";
        }
    }
    public void TrackPipes(int destroyedPipes)
    {
        pipesCount += destroyedPipes;
        //Debug.Log("No of Generator Destroyed" + noOfGenerator);
        if (pipesCount < 4)
        {
            NoPipes.text = "Destroyed Pipes: " + pipesCount.ToString();
        }
        else
        {
            NoPipes.text = "Destroyed Pipes: Completed";
        }
    }
}
