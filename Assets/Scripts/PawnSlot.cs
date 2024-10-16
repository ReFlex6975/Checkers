using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnSlot : MonoBehaviour
{
    public bool isOccupied = false; // ���������, ������ ������ ��� ���

    public void Occupy()
    {
        isOccupied = true; // ������ ����
    }

    public void Free()
    {
        isOccupied = false; // ���������� ����
    }
}
