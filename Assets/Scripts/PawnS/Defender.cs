using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : Pawn
{
    public float shieldStrength = 15f; // ���� ����

    public override void UpdateVisual()
    {
        // ���������� ������� ��� ���������
        Debug.Log("�������� ������� ������.");
    }

    public void ProtectAlly(Pawn ally)
    {
        // ������ ������ ��������
        Debug.Log("�������� �������� " + ally.name);
    }
}
