using UnityEngine;
using UnityEngine.Assertions;

public class CollisionDetector : DebuggableMonoBehaviour
{
    // [SerializeField] private SpriteRenderer spriteRenderer;
    // private readonly LogType logType = new(VariableStore.GAME_LOGIC);
    // protected override void Start()
    // {
    //     base.Start();
    //     spriteRenderer = GetComponent<SpriteRenderer>();
    //     Assert.IsNotNull(spriteRenderer, "SpriteRenderer component missing from this GameObject.");
    // }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     Log("OnTriggerEnter2D", logType);
    //     if (other.CompareTag("Enermy"))
    //     {
    //         spriteRenderer.color = Color.red;
    //     }
    //     if (other.CompareTag("Player"))
    //     {
    //         spriteRenderer.color = Color.gray;
    //     }
    // }

    // void OnTriggerExit2D(Collider2D other)
    // {
    //     Log("OnTriggerExit2D", logType);
    //     if (other.CompareTag("Enermy"))
    //     {
    //         spriteRenderer.color = Color.white;
    //     }
    //     if (other.CompareTag("Player"))
    //     {
    //         spriteRenderer.color = Color.white;
    //     }
    // }
}
