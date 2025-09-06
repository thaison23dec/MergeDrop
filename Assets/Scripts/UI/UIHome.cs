using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHome : MonoBehaviour
{
    public void NewGame()
    {
        Loader.Load(Loader.Scene.InGame);
        GameManager.instance.currentGameState = GameManager.GameState.NewGame;
    }

    public void Continue()
    {
        if (GameManager.instance.currentGameState == GameManager.GameState.GameOver)
        {
            NewGame();
        }
        else
        {
            Loader.Load(Loader.Scene.InGame);
            GameManager.instance.currentGameState = GameManager.GameState.Continue;
        }
    }
}
