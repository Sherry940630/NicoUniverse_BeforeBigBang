using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*
       Singleton Pattern確保一個類別只有一個實體的同時，能讓外部存取指定的實例。
       You can do that by using an field or a property, in this case we use a property. 
    */
    public static PlayerController Instance { get; private set; }

    /* SerializeField makes it appear in inspector while remaining private within this code */
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private GameInputs gameInputs;

    private bool isWalking;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        HandleMovement();
        //HandleInteraction();
    }

    private void HandleMovement()
    {
        /* GetMovementVectorNormalized() is in GameInputs.cs */
        Vector2 inputVector = gameInputs.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = Time.deltaTime * moveSpeed;
        float playerRadius = .8f;
        float playerHeight = 2f;
        /*
           Physics.Raycast fires a ray detecting what's in the way,
           and it returns true if it collides with something.
        */
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove) /* Will try to go around the wall instead of stopping completely. */
        {
            /* Attempt to only move in X direction. */
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
                moveDir = moveDirX;
            else /* Can't move in X direction. */
            {
                /* Attempt to only move in Z direction. */
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                    moveDir = moveDirZ;
                else { /* Nothing. */ }
            }
        }
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        /* transform.forward = moveDir makes the player face its moving direction. */
        float rotateSpeed = 10f;
        /* slerp helps smooth out the rotation. */
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        isWalking = moveDir != Vector3.zero;
    }
    public bool IsWalking()
    {
        return isWalking;
    }

    /*
    private void HandleInteraction()
    {
        transform.position = GetChairTopPoint();
    }
    */
}
