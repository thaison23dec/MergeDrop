using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeObject : MonoBehaviour
{
    public int ID;
    public bool hasMerged = false;

    [SerializeField] private GameObject mergedObj;
    [SerializeField] public MergeObjectType type;


    private void Start()
    {
        ID = GetInstanceID();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        MergeObject other = collision.gameObject.GetComponent<MergeObject>();

        if(other != null)
        {
            if (hasMerged || other.hasMerged) return;
        }


        if (gameObject.CompareTag(collision.gameObject.tag) && type == other.type)
        {
            if (ID < other.ID) return;

            Vector2 mergedObjPos = (transform.position + other.transform.position) / 2f;
            Instantiate(mergedObj, mergedObjPos, Quaternion.identity);

            hasMerged = true;
            other.hasMerged = true;

            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
