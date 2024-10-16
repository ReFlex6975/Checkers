using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infantry : Pawn
{
    public float defense = 5f; // Защита пехотинца

    public override void UpdateVisual()
    {
        // Обновление визуала для пехотинца
        Debug.Log("Пехотинец обновил визуал.");
    }
}
