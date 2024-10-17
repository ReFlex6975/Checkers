using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleProjectile : MonoBehaviour
{
    public float speed = 7f; // �������� �����
    private Transform target;
    private float damage;

    public void Initialize(Transform target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }

    private void FixedUpdate()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        if (target != null)
        {
            // ��������� � ����
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // ��������� ���������� �� ����
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                // ������� ���� ����� � ���������� ����
                Enemy enemy = target.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
                Destroy(gameObject); // ���������� ����
            }
        }
        else
        {
            Destroy(gameObject); // ���������� ����, ���� ���� ����������
        }
    }
}
