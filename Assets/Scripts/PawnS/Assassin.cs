using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : Pawn
{
    public float stealthDuration = 5f; // Длительность невидимости
    private bool isStealth = false;

    public override void Update()
    {
        base.Update(); // Вызываем базовый метод Update

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ActivateStealth();
        }
    }

    private void ActivateStealth()
    {
        isStealth = true;
        Debug.Log("Убийца активирует невидимость на " + stealthDuration + " секунд.");
        // Логика активации невидимости
        StartCoroutine(DeactivateStealth());
    }

    private IEnumerator DeactivateStealth()
    {
        yield return new WaitForSeconds(stealthDuration);
        isStealth = false;
        Debug.Log("Убийца выходит из невидимости.");
    }

    public override void UpdateVisual()
    {
        // Обновление визуала для убийцы
        Debug.Log("Убийца обновил визуал.");
    }
}
