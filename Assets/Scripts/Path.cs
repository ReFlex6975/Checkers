using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Transform[] waypoints;

    private void Start()
    {
        // Инициализация точек пути, если это необходимо
        waypoints = GetComponentsInChildren<Transform>();
        // Удаление самого объекта Path из массива
        waypoints = waypoints.Skip(1).ToArray(); // Удаляем первый элемент, который является самим объектом Path
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

