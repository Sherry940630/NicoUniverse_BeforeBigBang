using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    [SerializeField] private Transform chairTopPoint;

    public Transform GetChairTopPoint()
    {
        return chairTopPoint;
    }

    public virtual void Interact(PlayerController player)
    {
    }

    //public bool HasPersonOnChair(){}
}
