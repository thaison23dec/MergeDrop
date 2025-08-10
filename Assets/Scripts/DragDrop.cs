using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    public bool isDragging = false;
    public bool isDraggable = true;
    public Rigidbody2D rb;

    private Vector3 offSet;

    private void Update()
    {
        if (isDragging && isDraggable)
        {
            //transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offSet;
            //transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x + offSet.x, transform.position.y, transform.position.z);
        }
    }

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    //private void OnMouseDown()
    //{
    //    if (isDraggable)
    //    {
    //        offSet = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        isDragging = true;
    //    }
    //}

    //private void OnMouseUp()
    //{
    //    isDragging = false;
    //    isDraggable = false;
    //    rb.bodyType = RigidbodyType2D.Dynamic;
    //    GamePlayManager.Instance.SpawnObject();
    //}
}
