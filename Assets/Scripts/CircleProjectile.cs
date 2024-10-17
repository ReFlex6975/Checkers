using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleProjectile : MonoBehaviour
{
    public float speed = 7f; // Скорость круга
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
            // Двигаемся к цели
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // Проверяем расстояние до цели
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                // Наносим урон врагу и уничтожаем круг
                Enemy enemy = target.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
                Destroy(gameObject); // Уничтожаем круг
            }
        }
        else
        {
            Destroy(gameObject); // Уничтожаем круг, если цель недоступна
        }
    }
}
