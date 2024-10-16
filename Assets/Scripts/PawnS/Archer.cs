using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Pawn
{
    public float range = 10f; // Дальность атаки

    public override void UpdateVisual()
    {
        // Обновление визуала для лучника
        Debug.Log("Лучник обновил визуал.");
    }

    public override void Attack()
    {
        // Специальная логика для атаки лучника
        Enemy target = FindEnemyWithHighestProgress();
        if (target != null)
        {
            Debug.Log("Лучник атакует врага с прогрессом: " + target.progress);
            // Здесь можно добавить реализацию атаки на расстоянии
        }
    }
}
