using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Pawn
{
    public float range = 10f; // ��������� �����

    public override void UpdateVisual()
    {
        // ���������� ������� ��� �������
        Debug.Log("������ ������� ������.");
    }

    public override void Attack()
    {
        // ����������� ������ ��� ����� �������
        Enemy target = FindEnemyWithHighestProgress();
        if (target != null)
        {
            Debug.Log("������ ������� ����� � ����������: " + target.progress);
            // ����� ����� �������� ���������� ����� �� ����������
        }
    }
}
