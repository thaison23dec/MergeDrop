using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeObject : MonoBehaviour
{
    public int ID;
    public bool hasMerged = false;
    public bool isDragging = false;
    public bool isDropped;
    [SerializeField] public bool isDraggable = true;
    public Rigidbody2D rb;

    [SerializeField] private GameObject mergedObj;
    [SerializeField] private AudioClip mergeSoundClip;
    [SerializeField] public int prefabID;
    [SerializeField] public MergeObjectType type;
    [SerializeField] private ParticleSystem mergeParticle;
    [SerializeField] private int mergeScore;

    private ParticleSystem mergeParticleInstance;
    private bool touchedLimitLine = false;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        
    }

    private void Start()
    {
        ID = GetInstanceID();
        isDropped = false;
        if (!isDraggable)
        {
            FruitHolder.instance.AddFruit(gameObject);
        }
    }


    public void ParticleMergeFruit()
    {
        mergeParticleInstance = Instantiate(mergeParticle, transform.position, Quaternion.identity);
    }

    private void OnMouseDown()
    {
        Debug.Log("Clicked: " + gameObject.name);
        if (ItemManager.instance.isChoosingFruitToDestroy == true)
        {
            Destroy(gameObject);
            ParticleMergeFruit();
            ItemManager.instance.EndDestroyChosenFruit();
        }
        GamePlayManager.Instance.ActivatePointerDragRangeCollider();
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
            GameObject newFruit =  Instantiate(mergedObj, mergedObjPos, Quaternion.identity);

            newFruit.GetComponent<MergeObject>().ParticleMergeFruit();
            newFruit.GetComponent<MergeObject>().isDropped = true;
            newFruit.GetComponent<MergeObject>().isDraggable = false;
            newFruit.GetComponent<MergeObject>().rb.bodyType = RigidbodyType2D.Dynamic;

            SoundFXManager.instance.PlaySoundFXClip(mergeSoundClip, mergedObj.transform, 1f);

            ScoreManager.instance.IncreaseScore(mergeScore);
            UIManager.instance.UpdateScore();

            hasMerged = true;
            other.hasMerged = true;

            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        MergeObject other = collision.gameObject.GetComponent<MergeObject>();

        if (other != null)
        {
            if (hasMerged || other.hasMerged) return;
            if (isDraggable || other.isDraggable) return;
        }


        if (gameObject.CompareTag(collision.gameObject.tag) && type == other.type)
        {
            if (ID < other.ID) return;

            Vector2 mergedObjPos = (transform.position + other.transform.position) / 2f;
            GameObject newFruit = Instantiate(mergedObj, mergedObjPos, Quaternion.identity);

            newFruit.GetComponent<MergeObject>().ParticleMergeFruit();
            newFruit.GetComponent<MergeObject>().isDropped = true;
            newFruit.GetComponent<MergeObject>().isDraggable = false;
            newFruit.GetComponent<MergeObject>().isDraggable = false;
            newFruit.GetComponent<MergeObject>().rb.bodyType = RigidbodyType2D.Dynamic;

            SoundFXManager.instance.PlaySoundFXClip(mergeSoundClip, mergedObj.transform, 1f);

            ScoreManager.instance.IncreaseScore(mergeScore);
            UIManager.instance.UpdateScore();

            hasMerged = true;
            other.hasMerged = true;

            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LimitLine"))
        {
            touchedLimitLine = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LimitLine"))
        {
            touchedLimitLine = true;
        }
    }

    private void OnDestroy()
    {
        if (FruitHolder.instance != null)
        {
            FruitHolder.instance.RemoveFruit(this.gameObject);
        }
    }

    public void CheckLimitLine()
    {
        StartCoroutine("CheckLimitLineCoroutine");
    }

    private IEnumerator CheckLimitLineCoroutine()
    {
        yield return new WaitForSeconds(2f);
        if (!touchedLimitLine)
        {
            GamePlayManager.Instance.GameOver();
        }
    }
}
