using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : Pawn
{
    public float stealthDuration = 5f; // ������������ �����������
    private bool isStealth = false;

    public override void Update()
    {
        base.Update(); // �������� ������� ����� Update

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ActivateStealth();
        }
    }

    private void ActivateStealth()
    {
        isStealth = true;
        Debug.Log("������ ���������� ����������� �� " + stealthDuration + " ������.");
        // ������ ��������� �����������
        StartCoroutine(DeactivateStealth());
    }

    private IEnumerator DeactivateStealth()
    {
        yield return new WaitForSeconds(stealthDuration);
        isStealth = false;
        Debug.Log("������ ������� �� �����������.");
    }

    public override void UpdateVisual()
    {
        // ���������� ������� ��� ������
        Debug.Log("������ ������� ������.");
    }
}
