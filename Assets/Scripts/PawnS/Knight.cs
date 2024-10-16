using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Pawn
{
    public float specialAbilityCooldown = 5f; // ����� �������������� ����������� �����������
    private float specialAbilityTimer;

    public override void Update()
    {
        base.Update(); // �������� ������� ����� Update

        // ��������� ����������� �����������
        specialAbilityTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && specialAbilityTimer >= specialAbilityCooldown)
        {
            UseSpecialAbility();
            specialAbilityTimer = 0f; // ���������� ������
        }
    }

    private void UseSpecialAbility()
    {
        // ���������� ����������� �����������
        Debug.Log("������ ���������� ����������� �����������!");
        // ����� �� ������ �������� ������ ��� ����������� �����������
    }

    public override void UpdateVisual()
    {
        // ���������� ������� ��� ������, ��������, ��������� �������
    }
}
