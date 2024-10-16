using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Pawn
{
    public float specialAbilityCooldown = 5f; // Время восстановления специальной способности
    private float specialAbilityTimer;

    public override void Update()
    {
        base.Update(); // Вызываем базовый метод Update

        // Обработка специальной способности
        specialAbilityTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && specialAbilityTimer >= specialAbilityCooldown)
        {
            UseSpecialAbility();
            specialAbilityTimer = 0f; // Сбрасываем таймер
        }
    }

    private void UseSpecialAbility()
    {
        // Реализация специальной способности
        Debug.Log("Рыцарь использует специальную способность!");
        // Здесь вы можете добавить логику для специальной способности
    }

    public override void UpdateVisual()
    {
        // Обновление визуала для рыцаря, например, изменение спрайта
    }
}
