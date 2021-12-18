using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private GameObject breakVFX;
    [SerializeField] private Sprite[] hitSprites;
    
    private Level level;
    private int timesHit = 0;
    private void Start()
    {
        level = FindObjectOfType<Level>();
        if (CompareTag("Breakable"))
        {
            level.AddBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            int spriteIndex = timesHit - 1;
            Debug.Log(spriteIndex);
            if (hitSprites[spriteIndex] != null)
            {
                GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
            }
            else
            {
                Debug.LogError("Block missing sprite" + gameObject.name);
            }
        }
    }

    private void DestroyBlock()
    {
        Destroy(gameObject);
        level.RemoveBlock();
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        TriggerBreakVFX();
    }

    private void TriggerBreakVFX()
    {
        GameObject breakVFXObj = Instantiate(breakVFX, transform.position, transform.rotation);
        Destroy(breakVFXObj, 3f);
    }
}
