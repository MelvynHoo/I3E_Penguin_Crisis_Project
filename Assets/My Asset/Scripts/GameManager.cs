using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading.Tasks;


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

    #region Dialogue Related Bariables
    bool talkOnce = false;
    public TextMeshProUGUI dialogueMessage;
    public GameObject backgroundDialogue;
    #endregion

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
    public TextMeshProUGUI Hints;
    #endregion
    private void Awake()
    {
        dialogueMessage.text = "";
        // Check whether there is an instance
        // Check whether the instance is me
        if (instance != null && instance != this)
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

    private void Update()
    {


        if (pipesCount >= 4 && generatorCount >= 3 && computerCount >= 6)
        {
            //Debug.Log("Update escape");
            NoPipes.text = "Escape the facility";
            NoGenerator.text = "";
            NoComputer.text = "";
        }
        else if (pipesCount >= 4)
        {
            NoPipes.text = "Destroy Pipes: Completed";
        }
        else if (generatorCount >= 3)
        {
            //NoGenerator.text = "Destroy Generator: Completed";
        }
        else if (computerCount >= 6)
        {
            NoComputer.text = "Destroy Computer: Completed";
        }

    }

    /// <summary>
    /// Spawn the player when the scene changes
    /// </summary>
    /// <param name="currentScene"></param>
    /// <param name="nextScene"></param>
    void SpawnPlayerOnLoad(Scene currentScene, Scene nextScene)
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            // Checking if there is any active player in the game.
            if (activePlayer == null)
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

            if (computerCount >= 6)
            {
                NoPipes.text = "Escape the facility";
                NoGenerator.text = "";
                NoComputer.text = "";
            }
            else if (pipesCount >= 4)
            {
                NoPipes.text = "Destroyed Pipes: Completed";
                NoGenerator.text = "Destroyed Generator: " + generatorCount.ToString() + "/3";
            }
            else if (generatorCount >= 3)
            {
                //NoGenerator.text = "Destroyed Generator: Completed";
                NoComputer.text = "Destroyed Computer: " + computerCount.ToString() + "/6";
            }
            else
            {
                NoComputer.text = "";
                NoGenerator.text = "";
                NoPipes.text = "";
                Hints.text = "";
            }
            Debug.Log("Number of Generator " + generatorCount);
            Debug.Log("Number of Pipes " + pipesCount);
            Debug.Log("Number of Computer " + computerCount);
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            NoPipes.text = "Destroyed Pipes: Completed";
            NoGenerator.text = "Destroyed Generator: " + generatorCount.ToString() + "/3";
            Hints.text = "";
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3 || SceneManager.GetActiveScene().buildIndex == 4)
        {
            NoPipes.text = "Destroyed Pipes: Completed";
            NoGenerator.text = "Destroyed Generator: Completed";
            NoComputer.text = "Destroyed Computer: " + computerCount.ToString() + "/6";
            Hints.text = "";
        }
        else if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            NoPipes.text = "Escape the facility";
            NoGenerator.text = "";
            NoComputer.text = "";
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
            Player.instance.StopRotation(gamePaused);
        }
        else
        {
            // If the game is paused, unpause it
            Time.timeScale = 1f;
            gamePaused = false;
            pauseMenu.SetActive(gamePaused);
            Player.instance.StopRotation(gamePaused);
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
        StartGame();
        
        ToggleRespawnMenu();
        generatorCount = 0;
        pipesCount = 0;
        computerCount = 0;
        NoGenerator.text = "";
        NoComputer.text = "";
        NoPipes.text = "";
        //activePlayer.ResetPlayer();
        Player.instance.ResetPlayer();
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
    public async void BossPenguinDialogue()
    {
        //Debug.Log("Boss penguin talking");
        if (!talkOnce)
        {
            //backgroundDialogue.SetActive(true);
            dialogueMessage.text = "Elder Penguin: The humans have come to take away our land.";
            await Task.Delay(5000);
            dialogueMessage.text = "Elder Penguin: They have set up their facility in our home! They even changed the colour of our water!";
            await Task.Delay(5000);
            dialogueMessage.text = "Elder Penguin: Please help us and do something to stop the humans.";
            await Task.Delay(3000);
            //NoGenerator.text = "Destroy Generator: 0";
            //NoComputer.text = "Destroy Computer: 0";
            NoPipes.text = "Cross over the polluted river.";
            dialogueMessage.text = "";
            talkOnce = true;
        }
        else
        {
            talkOnce = false;

        }
    }

    public async void PollutedWaterDialogue()
    {
        //Debug.Log("Boss penguin talking");
        if (!talkOnce)
        {
            //backgroundDialogue.SetActive(true);
            dialogueMessage.text = "Fellow Penguin: The polluted water is caused by humans dumping toxic waste into the water. This harms the wildlife that live here.";
            await Task.Delay(4000);
            //backgroundDialogue.SetActive(false);
            dialogueMessage.text = "";
            talkOnce = true;
        }
        else
        {
            talkOnce = false;

        }
    }
    public void ActivateFindFacility()
    {
        NoPipes.text = "Find a way to enter the facility.";
    }
    public void ActivatePipesObjective()
    {
        NoPipes.text = "Destroyed Pipes: " + pipesCount.ToString() + "/4";
    }
    public async void IcebergDialogue()
    {
        //Debug.Log("Boss penguin talking");
        if (!talkOnce)
        {
            //backgroundDialogue.SetActive(true);
            dialogueMessage.text = "Me: Humans have been dumping their trash into the water, it’s sad to see this.";
            await Task.Delay(4000);
            //backgroundDialogue.SetActive(false);
            dialogueMessage.text = "";
            talkOnce = true;
        }
        else
        {
            talkOnce = false;

        }
    }
    public async void EndingDialogue()
    {
        //Debug.Log("Boss penguin talking");
        if (!talkOnce)
        {
            //backgroundDialogue.SetActive(true);
            dialogueMessage.text = "Elder Penguin: Thank you! You have saved us from further harm from the humans and their irresponsible behaviors!";
            await Task.Delay(4000);
            //backgroundDialogue.SetActive(false);
            dialogueMessage.text = "";
            talkOnce = true;
        }
        else
        {
            talkOnce = false;

        }
    }

    public async void TrackGenerator(int destroyedGens)
    {
        generatorCount += destroyedGens;
        //Debug.Log("No of Generator Destroyed" + noOfGenerator);
        if (generatorCount < 3)
        {
            NoGenerator.text = "Destroyed Generator: " + generatorCount.ToString() + "/3";
        }
        
        else
        {
            NoGenerator.text = "Destroyed Generator: Completed";
            NoComputer.text = "Destroyed Computer: " + computerCount.ToString() + "/6";
            MyEventManager.instance.OpenToOutsideTwo();
            await Task.Delay(3000);
            Hints.text = "Hints: Find the blue light to the next objective.";
        }
    }
    public async void TrackComputer(int destroyedComputer)
    {
        computerCount += destroyedComputer;
        //Debug.Log("No of Generator Destroyed" + noOfGenerator);
        if (computerCount < 6)
        {
            NoComputer.text = "Destroyed Computer: " + computerCount.ToString() + "/6";
        }
        
        else
        {
            NoComputer.text = "Destroyed Computer: Completed";
            MyEventManager.instance.OpenToOutsideThree();
            await Task.Delay(3000);
            Hints.text = "Hints: Find the blue light to the next objective.";
        }
    }
    public async void TrackPipes(int destroyedPipes)
    {
        pipesCount += destroyedPipes;
        //Debug.Log("No of Generator Destroyed" + noOfGenerator);
        if (pipesCount < 4)
        {
            NoPipes.text = "Destroyed Pipes: " + pipesCount.ToString() + "/4";
        }
        

        else
        {
            NoPipes.text = "Destroyed Pipes: Completed";
            NoGenerator.text = "Destroyed Generator:" + generatorCount.ToString() + "/3";
            MyEventManager.instance.OpenGeneratorRoom();
            await Task.Delay(3000);
            Hints.text = "Hints: Find the blue light to the next objective.";
        }
    }
}
