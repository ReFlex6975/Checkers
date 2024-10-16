using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    private Transform[] pathPoints;
    private int currentWaypointIndex = 0;
    public float health = 10f;
    public float progress = 0f; // ��������
    private float totalDistance; // ����� ���������� ����


    private void Start()
    {
        CalculateTotalDistance();
        StartCoroutine(MoveAlongPath());
    }

    private void CalculateTotalDistance()
    {
        totalDistance = 0f;
        for (int i = 0; i < pathPoints.Length - 1; i++)
        {
            totalDistance += Vector3.Distance(pathPoints[i].position, pathPoints[i + 1].position);
        }
    }

    public void InitializePath(Transform[] path)
    {
        pathPoints = path;
        if (pathPoints.Length > 0)
        {
            transform.position = pathPoints[0].position; // ������������� ��������� ������� �����
        }
        else
        {
            Debug.LogError("����� ���� �� ������ ��� �����!");
        }
    }

    private IEnumerator MoveAlongPath()
    {
        while (currentWaypointIndex < pathPoints.Length - 1)
        {
            Transform targetPoint = pathPoints[currentWaypointIndex + 1];
            float segmentDistance = Vector3.Distance(pathPoints[currentWaypointIndex].position, targetPoint.position);
            Vector3 startPoint = pathPoints[currentWaypointIndex].position;
            float distanceCovered = 0f;

            while (Vector3.Distance(transform.position, targetPoint.position) > 0.1f)
            {
                Vector3 direction = (targetPoint.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;
                distanceCovered += speed * Time.deltaTime; // ������� ���������� ����������

                // ��������� �������� � ����������� �� �������� ��������
                progress = Mathf.Clamp(((distanceCovered / segmentDistance) * 33f) + (currentWaypointIndex * 33f), 0f, 100f);
                yield return null;
            }

            currentWaypointIndex++;
        }
        // ���� ���� ����� �� ����� ����, �������� ����� PlayerHealth 
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.PlayerHealth();
        }

        // ���� ���� ����� �� ����� ����, ���������� ���
        Destroy(gameObject);
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("���� �������� ����: " + damage + ". ������� ��������: " + health);
        if (health <= 0) Die();
    }

    private void Die()
    {
        Debug.Log("���� ���������!");
        Destroy(gameObject);
        // ����������� ���� �� �������� �����
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.IncreaseMana(10);
        }
    }
}