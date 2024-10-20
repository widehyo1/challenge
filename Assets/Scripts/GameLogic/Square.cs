using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class Square : MonoBehaviour
{

    private SquareHealth squareHealth;
    // private SquareWeapon squareWeapon;
    private bool _isDead = false;
    private bool isAfterStart = false;
    private GameObject playerObj;
    private Player player;
    private EnermyData _squareData;
    private Rigidbody2D squareRigidbody;
    private float speed;

    [Inject]
    public void Construct(EnermyData squareData)
    {
        Debug.Log("Construct[Square] start");
        _squareData = squareData;
        speed = _squareData.speed;
        Debug.Log("Construct[Square] end");
    }

    public void Awake()
    {
        Debug.Log("Awake[Square] start");
        squareHealth = GetComponent<SquareHealth>();
        // squareWeapon = GetComponent<SquareWeapon>();
        squareRigidbody = GetComponent<Rigidbody2D>();
        Debug.Log("Awake[Square] end");
    }

    void Start()
    {
        Debug.Log("Start[Square] start");
        playerObj = GameObject.FindWithTag("Player");
        player = playerObj.GetComponent<Player>();
        isAfterStart = true;
        Debug.Log("Start[Square] end");
    } 

    void OnEnable()
    {
        if (!isAfterStart) return;
        Debug.Log($"OnEnabled[Square] start");
        Debug.Log($"speed: {speed}");
        Debug.Log($"OnEnabled[Square] ended");
    }

    Vector2 ToPlayerDirection()
    {
        Vector2 direction = playerObj.transform.position - transform.position;
        return direction.normalized;
    }

    void FixedUpdate()
    {
        Vector2 move = ToPlayerDirection();
        Vector2 movement = speed * Time.deltaTime * move;
        squareRigidbody.MovePosition(squareRigidbody.position + movement);
    }

    public override string ToString()
    {
        // return $"({squareHealth.HP}/{squareHealth.maxHP}, damage: {squareWeapon.damage}, speed: {speed}, timeBetAttack: {squareWeapon.timeBetAttack})";
        return $"({squareHealth.HP}/{squareHealth.maxHP}, speed: {speed})";
    }

    public void OnDie()
    {
        Debug.Log("OnDie[Square]");
        _isDead = true;
    }
}
