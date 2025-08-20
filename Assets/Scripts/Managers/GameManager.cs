using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
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
    }

    public ScoreManager ScoreManager { get; set; }
}
