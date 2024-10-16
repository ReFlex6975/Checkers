using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Transform[] waypoints;

    private void Start()
    {
        // ������������� ����� ����, ���� ��� ����������
        waypoints = GetComponentsInChildren<Transform>();
        // �������� ������ ������� Path �� �������
        waypoints = waypoints.Skip(1).ToArray(); // ������� ������ �������, ������� �������� ����� �������� Path
    }

    public int WaypointsCount()
    {
        return waypoints.Length;
    }

    public Transform GetWaypoint(int index)
    {
        if (index < 0 || index >= waypoints.Length) return null;
        return waypoints[index];
    }
}

