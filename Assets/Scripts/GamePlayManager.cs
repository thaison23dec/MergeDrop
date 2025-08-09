using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GamePlayManager : MonoBehaviour
{

    public static GamePlayManager Instance { get; private set; }

    [SerializeField] private List<GameObject> objectList;
    private Vector3 dropPoint;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        dropPoint = new Vector3(0f, 4f, 0f);
    }

    private void Start()
    {
        SpawnObject();
    }

    public void SpawnObject()
    {
        StartCoroutine("RandomObject");
    }

    IEnumerator RandomObject()
    {
        int randomID = Random.Range(0, objectList.Count);
        yield return new WaitForSeconds(0.75f);
        Instantiate(objectList[randomID], dropPoint, Quaternion.identity);
    }
}
