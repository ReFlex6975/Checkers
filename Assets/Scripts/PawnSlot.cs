using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnSlot : MonoBehaviour
{
    public bool isOccupied = false; // Указывает, занята клетка или нет

    public void Occupy()
    {
        isOccupied = true; // Занять слот
    }

    public void Free()
    {
        isOccupied = false; // Освободить слот
    }
}
