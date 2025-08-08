using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeObject : MonoBehaviour
{
    public int ID;

    [SerializeField] private GameObject mergedObj;
    [SerializeField] public MergeObjectType type;
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
            if(type == collision.gameObject.GetComponent<MergeObject>().type)
            {
                block1 = gameObject;
                block2 = collision.gameObject;
                if (ID < block2.gameObject.GetComponent<MergeObject>().ID) return;
                Vector2 mergedObjPos = new Vector2((block1.transform.position.x + block2.transform.position.x) / 2, (block1.transform.position.y + block2.transform.position.y) / 2);
                Instantiate(mergedObj, mergedObjPos, Quaternion.identity);
                Destroy(gameObject);
                Destroy(collision.gameObject);
            }
        }
    }
}
