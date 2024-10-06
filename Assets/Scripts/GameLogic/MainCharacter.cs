using UnityEngine;

public class MainCharacter : DebuggableMonoBehaviour
{
    public static Vector3 position;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {

        float moveX = Input.GetAxis(VariableStore.HORIZONTAL);
        float moveY = Input.GetAxis(VariableStore.VERTICAL);

        Vector3 movement = VariableStore.playerSpeed * Time.deltaTime * new Vector3(moveX, moveY, 0);
        transform.position += movement;
        position = transform.position;
    }
    
    void OnDrawGizmos()
    {
        // Draw a red sphere at the transform's position if collider is a trigger
        Gizmos.color = Color.red;
        if (GetComponent<Collider2D>().isTrigger)
        {
            Gizmos.DrawWireSphere(transform.position, 0.5f);
        }
    }    
}
