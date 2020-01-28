using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    menu,
    inGame,
    gameOver
}
public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;
    public GameState currentGameState = GameState.menu;
    public Canvas menuCanvas, gameCanvas, gameOverCanvas;
    public int collectedObjects = 0;
    
    void Awake()
    {
        sharedInstance = this;
    }

    void Start()
    {
        BackToMenu();
    }

    void Update()
    {
        if(currentGameState == GameState.menu){
            if (Input.GetButtonDown("Start")){
                StartGame();
            }
        }else {
            if (Input.GetButtonDown("Start")){
                    BackToMenu();
             }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
    }

    public void StartGame()
    {
        SetGameState(GameState.inGame);

        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        CameraFollow cameraFollow = camera.GetComponent<CameraFollow>();
        cameraFollow.ResetCameraPosition();
        

        if(PlayerController.sharedInstance.transform.position.x > 10) {
        LevelGenerator.sharedInstance.RemoveAllTheBlocks();
        LevelGenerator.sharedInstance.GenerateInitialBlocks();
        }

        PlayerController.sharedInstance.StartGame();

        collectedObjects = 0;
    }

    public void GameOver()
    {
        SetGameState(GameState.gameOver);
        

    }

    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    void SetGameState(GameState newGameState)
    {
        this.currentGameState = newGameState;

        if(newGameState == GameState.menu)
        {
            menuCanvas.enabled = true;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = false;

        }else if(newGameState == GameState.inGame)
        {
            menuCanvas.enabled = false;
            gameCanvas.enabled = true;
            gameOverCanvas.enabled = false;

        }
        else if(newGameState == GameState.gameOver)
        {
            menuCanvas.enabled = false;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = true;

        }

    }

    public void CollectObject(int objectValue)
    {
        this.collectedObjects += objectValue;
        Debug.Log("Llevamos recogidos "+this.collectedObjects);
    }
}
