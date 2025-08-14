using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeObject : MonoBehaviour
{
    public int ID;
    public bool hasMerged = false;
    public bool isDragging = false;
    [SerializeField] public bool isDraggable = true;
    public Rigidbody2D rb;

    [SerializeField] private bool isMergedObject;
    [SerializeField] private GameObject mergedObj;
    [SerializeField] private AudioClip mergeSoundClip;
    [SerializeField] public MergeObjectType type;
    [SerializeField] private ParticleSystem mergeParticle;

    private ParticleSystem mergeParticleInstance;

    private void Start()
    {
        ID = GetInstanceID();
        rb = gameObject.GetComponent<Rigidbody2D>();
        if (isMergedObject)
        {
            mergeParticleInstance = Instantiate(mergeParticle, transform.position, Quaternion.identity);
        }

    }

    private void SpawnMergeParticle(Transform pos)
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    
        MergeObject other = collision.gameObject.GetComponent<MergeObject>();

        if(other != null)
        {
            if (hasMerged || other.hasMerged) return;
            if (isDraggable || other.isDraggable) return;
        }


        if (gameObject.CompareTag(collision.gameObject.tag) && type == other.type)
        {
            if (ID < other.ID) return;

            Vector2 mergedObjPos = (transform.position + other.transform.position) / 2f;
            Instantiate(mergedObj, mergedObjPos, Quaternion.identity);

            SoundFXManager.instance.PlaySoundFXClip(mergeSoundClip, mergedObj.transform, 1f);
 

            hasMerged = true;
            other.hasMerged = true;

            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
