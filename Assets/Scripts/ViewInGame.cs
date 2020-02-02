using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewInGame : MonoBehaviour
{
    public Text collectableLabel;
    public Text scoreLable;
    public Text maxscoreLable;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame ||
            GameManager.sharedInstance.currentGameState == GameState.gameOver)
        {
            int currentObjects = GameManager.sharedInstance.collectedObjects;
            this.collectableLabel.text = currentObjects.ToString();
        }

        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            float travelledDistance = PlayerController.sharedInstance.GetDistance();
            this.scoreLable.text = "Score\n"+travelledDistance.ToString("f2");

            float maxscore = PlayerPrefs.GetFloat("maxscore",0);
            this.maxscoreLable.text = "Max Score\n" + maxscore.ToString("f2");
        }
    }
}
