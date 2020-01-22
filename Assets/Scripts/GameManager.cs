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

        
    }

    public void StartGame()
    {
        SetGameState(GameState.inGame);
    }

    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }

    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }

    void SetGameState(GameState newGameState)
    {
        this.currentGameState = newGameState;

        if(newGameState == GameState.menu)
        {

        }else if(newGameState == GameState.inGame)
        {

        }else if(newGameState == GameState.gameOver)
        {

        }
        
       
    }
}
