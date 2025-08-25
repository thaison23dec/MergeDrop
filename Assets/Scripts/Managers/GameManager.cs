using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    public enum GameState
    {
        NewGame,
        Continue,
        GameOver
    }

    public GameState currentGameState;

    private void Awake()
    {
        if(instance != null && instance != this )
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        Debug.Log(Application.persistentDataPath);

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            SaveSystem.Save();
        }
        if (Input.GetKey(KeyCode.L))
        {
            SaveSystem.Load();
        }
        if (Input.GetKey(KeyCode.G))
        {
            GamePlayManager.Instance.GameOver();
        }
    }

    private void OnApplicationQuit()
    {
        SaveSystem.Save();
    }

    public ScoreManager ScoreManager { get; set; }
}
