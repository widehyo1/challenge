using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : DebuggableMonoBehaviour
{

    [Range(0f, 10f)]
    [SerializeField] private float mSpeed = 5f;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
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
    }
}
