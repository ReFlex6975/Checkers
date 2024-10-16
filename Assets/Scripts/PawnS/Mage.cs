using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Pawn
{
    public float spellDamage = 20f; // Урон заклинания
    public float spellCooldown = 3f; // Время восстановления заклинания
    private float spellTimer;

    public override void Update()
    {
        base.Update(); // Вызываем базовый метод Update

        // Обработка восстановления заклинания
        spellTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.F) && spellTimer >= spellCooldown)
        {
            CastSpell();
            spellTimer = 0f; // Сбрасываем таймер
        }
    }

    private void CastSpell()
    {
        // Реализация заклинания
        Debug.Log("Маг кастует заклинание!");
        // Здесь можно добавить логику для кастования заклинания
    }

    public override void UpdateVisual()
    {
        // Обновление визуала для мага
        Debug.Log("Маг обновил визуал.");
    }
}
