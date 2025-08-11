using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GamePlayManager : MonoBehaviour
{

    public static GamePlayManager Instance { get; private set; }
    public GameObject pointer;

    [SerializeField] private List<GameObject> objectList;
    [SerializeField] private GameObject currentFruit;

    private BoxCollider2D dragRangeCollider;
    public float dragRange;
    private bool pointerIsDragging = false;
    private Vector3 offSet;



    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        pointer.transform.position = new Vector3(0f, 4f, 0f);
        dragRangeCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        SpawnObject();
        dragRange = dragRangeCollider.size.x;
    }

    private void Update()
    {
        if (pointerIsDragging)
        {
            pointer.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x + offSet.x, pointer.transform.position.y, pointer.transform.position.z);
            if(pointer.transform.position.x >= dragRange / 2 - currentFruit.GetComponent<CircleCollider2D>().radius / 2 - 0.5f)
            {
                pointer.transform.position = new Vector3(dragRange / 2 - currentFruit.GetComponent<CircleCollider2D>().radius / 2 - 0.5f, pointer.transform.position.y, pointer.transform.position.z);
            }
            if (pointer.transform.position.x <= -dragRange / 2 + currentFruit.GetComponent<CircleCollider2D>().radius / 2 + 0.5f)
            {
                pointer.transform.position = new Vector3(-dragRange / 2 + currentFruit.GetComponent<CircleCollider2D>().radius / 2 + 0.5f, pointer.transform.position.y, pointer.transform.position.z);
            }
        }
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



    private void OnMouseDown()
    {
        pointerIsDragging = true;
        pointer.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, pointer.transform.position.y, pointer.transform.position.z);
        offSet = pointer.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (currentFruit.GetComponent<DragDrop>().isDraggable)
        {
            currentFruit.GetComponent<DragDrop>().isDragging = true;
        }
    }


    private void OnMouseUp()
    {
        pointerIsDragging = false;
        currentFruit.GetComponent<DragDrop>().isDragging = false;
        currentFruit.GetComponent<DragDrop>().isDraggable = false;
        currentFruit.GetComponent<DragDrop>().rb.bodyType = RigidbodyType2D.Dynamic;
        StartCoroutine("OnOffPointer");
        SpawnObject();
    }
    IEnumerator RandomObject()
    {
        int randomID = Random.Range(0, objectList.Count);
        yield return new WaitForSeconds(0.75f);
        currentFruit = Instantiate(objectList[randomID], pointer.transform.position, Quaternion.identity);
    }

    IEnumerator OnOffPointer()
    {
        pointer.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.75f);
        pointer.gameObject.SetActive(true);
    }

}
