using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform PosA, PosB;
    public float speed;
    Vector3 targetPos;

    MovementController movementController;
    Rigidbody2D rb;
    Vector3 moveDirection;

    Rigidbody2D playerRb;

    private void Awake()
    {
        movementController = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>();
        rb = GetComponent<Rigidbody2D>();
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        targetPos = PosB.position;
        DirectionCalculate();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, PosA.position) < 0.05f)
        {
            targetPos = PosB.position;
            DirectionCalculate();
        }

        if (Vector2.Distance(transform.position, PosB.position) < 0.05f)
        {
            targetPos = PosA.position;
            DirectionCalculate();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * speed;
    }

    void DirectionCalculate()
    {
        moveDirection = (targetPos - transform.position).normalized;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            movementController.isOnPlatform = true;
            movementController.platformRb = rb;
            playerRb.gravityScale *= 50;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            movementController.isOnPlatform = false;
            playerRb.gravityScale /= 50;
        }
    }
}