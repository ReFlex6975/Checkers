using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public int rank = 1;
    public float damage = 10f;
    public float attackRate = 1f;

    private float attackTimer;
    public GameObject circlePrefab;

    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 originalPosition;
    private PawnSlot currentSlot;

    private Color[] rankColors = new Color[]
    {
        Color.green,  // Ранг 1 - зеленый
        Color.blue,   // Ранг 2 - синий
        Color.red,    // Ранг 3 - красный
        Color.yellow,  // Ранг 4 - желтый
        Color.magenta, // Ранг 5 - пурпурный
        Color.cyan,     // Ранг 6 - голубой
        Color.black, // Ранг 7 - черный
        Color.white // Ранг 8 - белый
    };

    public void Initialize(int initialRank, float initialDamage, Transform[] pathPoints, PawnSlot slot)
    {
        rank = initialRank;
        damage = initialDamage;
        currentSlot = slot;
        UpdateVisual();
    }

    // Сделаем метод Update виртуальным
    public virtual void Update()
    {
        // Логика для атаки
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackRate)
        {
            Attack();
            attackTimer = 0f; // Сбрасываем таймер
        }

        // Обработка перетаскивания
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, transform.position.z);
        }
    }

    // Сделаем метод UpdateVisual виртуальным
    public virtual void UpdateVisual()
    {
        // Изменяем цвет пешки в зависимости от ранга
        if (rank - 1 < rankColors.Length) // Проверяем, чтобы избежать выхода за границы массива
        {
            GetComponent<SpriteRenderer>().color = rankColors[rank - 1]; // Устанавливаем цвет
        }
    }

    private void OnMouseDown()
    {
        // Начинаем перетаскивание и сохраняем начальную позицию
        isDragging = true;
        originalPosition = transform.position;
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag()
    {
        // Перетаскиваем пешку только на слоты
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, transform.position.z);
        }
    }

    private void OnMouseUp()
    {
        // Заканчиваем перетаскивание
        isDragging = false;

        // Проверяем возможность слияния с соседней пешкой
        bool merged = TryMergeWithPawn();

        // Если не удалось слиться, возвращаем пешку на исходную позицию
        if (!merged)
        {
            transform.position = originalPosition;
        }
    }

    float mergeRadius = 0.1f; // Радиус слияния пешки

    private bool TryMergeWithPawn() // Слияние пешек при перетаскивание
    {   
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, mergeRadius);
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent<Pawn>(out Pawn otherPawn) && otherPawn != this && otherPawn.rank == this.rank)
            {
                // Если нашли пешку с тем же рангом, создаем новую пешку и уничтожаем старые
                MergeWith(otherPawn);
                return true; // Возвращаем true, если удалось слиться
            }
        }
        return false; // Возвращаем false, если слияние не удалось
    }
    private void OnDrawGizmos() // Gizmo нужен для отрисовки бекенда
    {
        // Установите цвет Gizmo на красный
        Gizmos.color = Color.red;

        // Нарисуйте сферу радиуса mergeRadius вокруг позиции объекта
        Gizmos.DrawWireSphere(transform.position, mergeRadius);
    }


    private void MergeWith(Pawn otherPawn)
    {
        // Создаем новую пешку с увеличенным рангом на месте второй пешки
        GameObject newPawn = Instantiate(gameObject, otherPawn.transform.position, Quaternion.identity);
        Pawn newPawnComponent = newPawn.GetComponent<Pawn>();

        // Устанавливаем ранг и урон новой пешки
        newPawnComponent.rank = this.rank + 1;
        newPawnComponent.damage = this.damage + 10f;
        newPawnComponent.UpdateVisual();

        // Освобождаем слот для первой пешки
        if (this.currentSlot != null)
        {
            this.currentSlot.Free(); // Освобождаем слот
        }

        // Устанавливаем текущий слот для новой пешки
        newPawnComponent.SetCurrentSlot(otherPawn.currentSlot); // Передаем слот второй пешки

        // Уничтожаем обе старые пешки
        Destroy(this.gameObject);
        Destroy(otherPawn.gameObject);
    }

    // Обновление текущего слота
    public void SetCurrentSlot(PawnSlot slot)
    {
        currentSlot = slot; // Обновляем текущий слот
    }

    // Обработчик атаки
    public virtual void Attack()
    {
        Enemy target = FindEnemyWithHighestProgress(); // Ищем врага с наибольшим прогрессом
        if (target != null)
        {
            // Создание спрайта круга и направление его на врага
            GameObject circle = Instantiate(circlePrefab, transform.position, Quaternion.identity);
            CircleProjectile projectile = circle.GetComponent<CircleProjectile>();
            projectile.Initialize(target.transform, damage); // Передаем цель и урон для круга

            // Выводим информацию об атаке
            Debug.Log("Атака на врага с прогрессом: " + target.progress + ", Урон: " + damage);
        }
    }

    protected Enemy FindEnemyWithHighestProgress() // Сделаем FindEnemyWithHighestProgress защищенным
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>(); // Находим всех врагов
        Enemy highestProgressEnemy = null;
        float highestProgress = -1f;

        foreach (Enemy enemy in enemies)
        {
            if (enemy.progress > highestProgress)
            {
                highestProgress = enemy.progress;
                highestProgressEnemy = enemy;
            }
        }

        return highestProgressEnemy;
    }
}
