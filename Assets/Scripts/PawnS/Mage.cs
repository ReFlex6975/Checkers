using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Pawn
{
    public float spellDamage = 20f; // ���� ����������
    public float spellCooldown = 3f; // ����� �������������� ����������
    private float spellTimer;

    public override void Update()
    {
        base.Update(); // �������� ������� ����� Update

        // ��������� �������������� ����������
        spellTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.F) && spellTimer >= spellCooldown)
        {
            CastSpell();
            spellTimer = 0f; // ���������� ������
        }
    }

    private void CastSpell()
    {
        // ���������� ����������
        Debug.Log("��� ������� ����������!");
        // ����� ����� �������� ������ ��� ���������� ����������
    }

    public override void UpdateVisual()
    {
        // ���������� ������� ��� ����
        Debug.Log("��� ������� ������.");
    }
}
