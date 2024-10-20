using UnityEngine;
using UnityEngine.Pool;

public class Enermy : MonoBehaviour
{

    // private IObjectPool<Enermy> enermyPool;

    // private GameObject player;
    // private readonly LogType logType = new(VariableStore.GAME_LOGIC);

    // // public property to give the enermy a referenct to its EnermyPool
    // public IObjectPool<Enermy> EnermyPool { set => enermyPool = value; }

    // void Awake()
    // {
    //     player = GameObject.FindWithTag("Player");
    // }

    // void Start()
    // {

    // }

    // private Vector3 ToPlayerDirection()
    // {
    //     Vector3 direction = player.transform.position - transform.position;
    //     direction.Normalize();
    //     return direction;
    // }

    // void Update()
    // {
    //     Vector3 direction = ToPlayerDirection();
    //     Vector3 velocity = VariableStore.enermySpeed * Time.deltaTime * direction;

    //     transform.position += velocity;
    //     if (Mathf.Abs(transform.position.x) > 10 || Mathf.Abs(transform.position.y) > 10)
    //     {
    //         enermyPool.Release(this);
    //         Enermy[] enermies = (Enermy[]) FindObjectsByType(GetType(), FindObjectsSortMode.None);
    //         Debug.Log($"enermies.Length is: {enermies.Length}");
    //         if (enermies.Length < 10) 
    //         {
    //             Enermy enermy = enermyPool.Get();
    //             enermy.transform.SetPositionAndRotation(new Vector3(transform.position.x, -6, 0), Quaternion.identity);
    //         }
    //     }
    // }

    // void OnDrawGizmos()
    // {
    //     // Draw a red sphere at the transform's position if collider is a trigger
    //     Gizmos.color = Color.red;
    //     if (GetComponent<Collider2D>().isTrigger)
    //     {
    //         Gizmos.DrawWireSphere(transform.position, 0.5f);
    //     }
    // }

}
