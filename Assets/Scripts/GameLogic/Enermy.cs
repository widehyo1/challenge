using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enermy : DebuggableMonoBehaviour
{

    // deactivate after delay
    [SerializeField] private float timeoutDelay = 3f;

    [Range(0f, 10f)]
    [SerializeField] private float speed = 5f;
    private IObjectPool<Enermy> enermyPool;

    // public property to give the enermy a referenct to its EnermyPool
    public IObjectPool<Enermy> EnermyPool { set => enermyPool = value; }

    public void Deactivate()
    {
        StartCoroutine(DeactivateRoutine(timeoutDelay));
    }

    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        // reset the moving Rigidbody
        Rigidbody rBody = GetComponent<Rigidbody>();
        rBody.velocity = new Vector3(0f, 0f, 0f);
        rBody.angularVelocity = new Vector3(0f, 0f, 0f);

        // release the enermy back to the pool
        enermyPool.Release(this);
    }

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
        Vector3 direction = new(0, 1, 0);
        Vector3 velocity = speed * Time.deltaTime * direction;

        transform.position += velocity;
    }

}
