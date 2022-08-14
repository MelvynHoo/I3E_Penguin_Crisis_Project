/*
 * Author: Melvyn Hoo
 * Date: 14 Aug 2022
 * Description: Game Manager script, control the overall game
 */


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

    /// <summary>
    /// Keeping track of many pipes is destroy.
    /// </summary>
    public int pipesCount = 0;

    /// <summary>
    /// Keeping track of many computer is destroy.
    /// </summary>
    public int computerCount = 0;

    /// <summary>
    /// Text for objective generator.
    /// </summary>
    public TextMeshProUGUI NoGenerator;

    /// <summary>
    /// Text for objective pipes.
    /// </summary>
    public TextMeshProUGUI NoPipes;

    /// <summary>
    /// Text for objective computer.
    /// </summary>
    public TextMeshProUGUI NoComputer;

    /// <summary>
    /// Text for hint to the players.
    /// </summary>
    public TextMeshProUGUI Hints;
    #endregion

    /// <summary>
    /// set instance.
    /// </summary>
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

    /// <summary>
    /// Update the value when citeria has been met.
    /// </summary>
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

            // When computer have more the or equal to 6
            if (computerCount >= 6)
            {
                NoPipes.text = "Escape the facility";
                NoGenerator.text = "";
                NoComputer.text = "";
            }

            // When pipes have more the or equal to 4
            else if (pipesCount >= 4)
            {
                NoPipes.text = "Destroyed Pipes: Completed";
                NoGenerator.text = "Destroyed Generator: " + generatorCount.ToString() + "/3";
            }

            // When generator have more the or equal to 3
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

        // When scene is 1, enable the following
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            NoPipes.text = "Talk to the Elder Penguin.";
            Time.timeScale = 1f;
        }
        // When scene is 2, enable the following
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            NoPipes.text = "Destroyed Pipes: Completed";
            NoGenerator.text = "Destroyed Generator: " + generatorCount.ToString() + "/3";
            Hints.text = "";
        }
        // When scene is 3, enable the following
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            NoPipes.text = "Destroyed Pipes: Completed";
            NoGenerator.text = "Destroyed Generator: Completed";
            NoComputer.text = "Destroyed Computer: " + computerCount.ToString() + "/6";
            Hints.text = "Hints: Find the blue light to the next objective.";
        }
        // When scene is 4, enable the following
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            NoPipes.text = "Destroyed Pipes: Completed";
            NoGenerator.text = "Destroyed Generator: Completed";
            NoComputer.text = "Destroyed Computer: " + computerCount.ToString() + "/6";
            Hints.text = "";
        }
        // When scene is 5, enable the following
        else if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            NoPipes.text = "Escape the facility";
            NoGenerator.text = "";
            NoComputer.text = "";
            Hints.text = "Hints: Find and follow the blue light to escape.";
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
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Ends the game
    /// </summary>
    public void EndGame()
    {
        Destroy(activePlayer.gameObject);
        LoadScene(0);
        generatorCount = 0;
        pipesCount = 0;
        computerCount = 0;
        NoGenerator.text = "";
        NoComputer.text = "";
        NoPipes.text = "";
        Hints.text = "";
    }

    /// <summary>
    /// Restarts the game, reset the value
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
        Hints.text = "";
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
        Time.timeScale = 0f;
        completeMenu.SetActive(true);
        Player.instance.StopRotation(true);
        dialogueMessage.text = "";
        NoGenerator.text = "";
        NoComputer.text = "";
        NoPipes.text = "";
        Hints.text = "";
    }

    /// <summary>
    /// Boss penguin dialogue
    /// </summary>
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

    /// <summary>
    /// Polluted water dialogue
    /// </summary>
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

    /// <summary>
    /// Change the objective when reach to a certain area
    /// </summary>
    public void ActivateFindFacility()
    {
        NoPipes.text = "Find a way to enter the facility.";
    }

    /// <summary>
    /// Change the objective when reach to a certain area
    /// </summary>
    public void ActivatePipesObjective()
    {
        NoPipes.text = "Destroyed Pipes: " + pipesCount.ToString() + "/4";
    }

    /// <summary>
    /// Polluted Iceberg dialogue
    /// </summary>
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

    /// <summary>
    /// Ending dialogue
    /// </summary>
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

    /// <summary>
    /// function to track the generator and update the objective counter
    /// </summary>
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

    /// <summary>
    /// function to track the computer and update the objective counter
    /// </summary>
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

    /// <summary>
    /// function to track the pipes and update the objective counter
    /// </summary>
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
            NoGenerator.text = "Destroyed Generator: " + generatorCount.ToString() + "/3";
            MyEventManager.instance.OpenGeneratorRoom();
            await Task.Delay(3000);
            Hints.text = "Hints: Find the blue light to the next objective.";
        }
    }
}
