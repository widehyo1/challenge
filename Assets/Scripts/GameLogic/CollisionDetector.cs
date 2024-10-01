using UnityEngine;

public class CollisionDetector : DebuggableMonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private readonly LogType logType = new("GameLogic");
    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Log("SpriteRenderer component missing from this GameObject.", logType);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Log("OnTriggerEnter2D", logType);
        if (other.CompareTag("Enermy"))
        {
            spriteRenderer.color = Color.red;
        }
        if (other.CompareTag("Player"))
        {
            spriteRenderer.color = Color.gray;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Log("OnTriggerExit2D", logType);
        if (other.CompareTag("Enermy"))
        {
            spriteRenderer.color = Color.white;
        }
        if (other.CompareTag("Player"))
        {
            spriteRenderer.color = Color.white;
        }
    }
}
