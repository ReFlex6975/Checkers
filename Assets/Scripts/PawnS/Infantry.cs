using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infantry : Pawn
{
    public float defense = 5f; // ������ ���������

    public override void UpdateVisual()
    {
        // ���������� ������� ��� ���������
        Debug.Log("��������� ������� ������.");
    }
}
