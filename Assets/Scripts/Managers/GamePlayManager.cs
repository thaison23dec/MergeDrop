using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GamePlayManager : MonoBehaviour
{

    public static GamePlayManager Instance { get; private set; }
    public GameObject pointer;
    public bool canDrag = true;

    [SerializeField] private List<GameObject> objectList;
    [SerializeField] private GameObject currentFruit;
    [SerializeField] private AudioClip dropSoundClip;

    private BoxCollider2D dragRangeCollider;
    public float dragRange;
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
        if (pointer.GetComponent<Pointer>().pointerIsDragging)
        {
            pointer.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x + offSet.x, pointer.transform.position.y, pointer.transform.position.z);
            if (currentFruit != null)
            {
                if (pointer.transform.position.x >= dragRange / 2 - currentFruit.GetComponent<CircleCollider2D>().radius - 0.5f)
                {
                    pointer.transform.position = new Vector3(dragRange / 2 - currentFruit.GetComponent<CircleCollider2D>().radius - 0.5f, pointer.transform.position.y, pointer.transform.position.z);
                }
                if (pointer.transform.position.x <= -dragRange / 2 + currentFruit.GetComponent<CircleCollider2D>().radius + 0.5f)
                {
                    pointer.transform.position = new Vector3(-dragRange / 2 + currentFruit.GetComponent<CircleCollider2D>().radius + 0.5f, pointer.transform.position.y, pointer.transform.position.z);
                }
            }
        }

        if(currentFruit != null)
        {       
            if (currentFruit.GetComponent<MergeObject>().isDragging == true)
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
        if (canDrag)
        {
            pointer.GetComponent<Pointer>().pointerIsDragging = true;
            pointer.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, pointer.transform.position.y, pointer.transform.position.z);
            offSet = pointer.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (currentFruit != null)
            {
                if (currentFruit.GetComponent<MergeObject>().isDraggable)
                {
                    currentFruit.GetComponent<MergeObject>().isDragging = true;
                }
            }
        }
    }


    private void OnMouseUp()
    {
        if(pointer.GetComponent<Pointer>().pointerIsDragging == true)
        {
            SoundFXManager.instance.PlaySoundFXClip(dropSoundClip, currentFruit.transform, 1f);
            pointer.GetComponent<Pointer>().pointerIsDragging = false;
            currentFruit.GetComponent<MergeObject>().isDragging = false;
            currentFruit.GetComponent<MergeObject>().isDraggable = false;
            currentFruit.GetComponent<CircleCollider2D>().isTrigger = false;
            currentFruit.GetComponent<MergeObject>().rb.bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine("OnOffPointer");
            SpawnObject();
        }
    }
    IEnumerator RandomObject()
    {
        int randomID = Random.Range(0, objectList.Count);
        yield return new WaitForSeconds(0.75f);
        currentFruit = Instantiate(objectList[randomID], pointer.transform.position, Quaternion.identity);
        currentFruit.GetComponent<CircleCollider2D>().isTrigger = true;
    }

    IEnumerator OnOffPointer()
    {
        canDrag = false;
        pointer.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.75f);
        pointer.gameObject.SetActive(true);
        canDrag = true;
        
    }

}
