using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeObject : MonoBehaviour
{
    public int ID;

    private GameObject block1;
    private GameObject block2;

    private void Start()
    {
        ID = GetInstanceID();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.CompareTag(collision.gameObject.tag))
        {
            block1 = gameObject;
            block2 = collision.gameObject;
            if (ID < block2.gameObject.GetComponent<MergeObject>().ID) return;
            Vector2 mergedObjPos = new Vector2((block1.transform.position.x + block1.transform.position.x) / 2, (block1.transform.position.y + block1.transform.position.y) / 2);
            Instantiate(Resources.Load("SquareObject"), mergedObjPos, Quaternion.identity);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
