using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : Pawn
{
    public float shieldStrength = 15f; // Сила щита

    public override void UpdateVisual()
    {
        // Обновление визуала для защитника
        Debug.Log("Защитник обновил визуал.");
    }

    public void ProtectAlly(Pawn ally)
    {
        // Логика защиты союзника
        Debug.Log("Защитник защищает " + ally.name);
    }
}
