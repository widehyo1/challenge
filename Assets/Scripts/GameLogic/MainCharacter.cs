using UnityEngine;

public class MainCharacter : DebuggableMonoBehaviour
{

    [Range(0f, 10f)]
    [SerializeField] private float mSpeed = 5f;

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

        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 movement = mSpeed * Time.deltaTime * new Vector3(moveX, moveY, 0);
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
