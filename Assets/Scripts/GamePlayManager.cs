using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GamePlayManager : MonoBehaviour
{

    public static GamePlayManager Instance { get; private set; }
    public GameObject pointer;

    [SerializeField] private List<GameObject> objectList;
    [SerializeField] private GameObject currentFruit;
    private Vector3 offSet;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        pointer.transform.position = new Vector3(0f, 4f, 0f);
    }

    private void Start()
    {
        SpawnObject();
    }

    private void Update()
    {
        pointer.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x + offSet.x, pointer.transform.position.y, pointer.transform.position.z);
        if(currentFruit != null)
        {
            if(currentFruit.GetComponent<DragDrop>().isDragging == true)
            {
                currentFruit.transform.position = pointer.transform.position;
            }
        }
    }

    public void SpawnObject()
    {
        StartCoroutine("RandomObject");
    }

    IEnumerator RandomObject()
    {
        int randomID = Random.Range(0, objectList.Count);
        yield return new WaitForSeconds(0.75f);
        currentFruit = Instantiate(objectList[randomID], pointer.transform.position, Quaternion.identity);
    }

    private void OnMouseDown()
    {
        if (currentFruit.GetComponent<DragDrop>().isDraggable)
        {
            offSet = pointer.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentFruit.GetComponent<DragDrop>().isDragging = true;
        }
    }

    private void OnMouseUp()
    {
        currentFruit.GetComponent<DragDrop>().isDragging = false;
        currentFruit.GetComponent<DragDrop>().isDraggable = false;
        currentFruit.GetComponent<DragDrop>().rb.bodyType = RigidbodyType2D.Dynamic;
        SpawnObject();
    }
}
