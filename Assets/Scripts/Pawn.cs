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
        Color.green,  // ���� 1 - �������
        Color.blue,   // ���� 2 - �����
        Color.red,    // ���� 3 - �������
        Color.yellow,  // ���� 4 - ������
        Color.magenta, // ���� 5 - ���������
        Color.cyan,     // ���� 6 - �������
        Color.black, // ���� 7 - ������
        Color.white // ���� 8 - �����
    };

    public void Initialize(int initialRank, float initialDamage, Transform[] pathPoints, PawnSlot slot)
    {
        rank = initialRank;
        damage = initialDamage;
        currentSlot = slot;
        UpdateVisual();
    }

    // ������� ����� Update �����������
    public virtual void Update()
    {
        // ������ ��� �����
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackRate)
        {
            Attack();
            attackTimer = 0f; // ���������� ������
        }

        // ��������� ��������������
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, transform.position.z);
        }
    }

    // ������� ����� UpdateVisual �����������
    public virtual void UpdateVisual()
    {
        // �������� ���� ����� � ����������� �� �����
        if (rank - 1 < rankColors.Length) // ���������, ����� �������� ������ �� ������� �������
        {
            GetComponent<SpriteRenderer>().color = rankColors[rank - 1]; // ������������� ����
        }
    }

    private void OnMouseDown()
    {
        // �������� �������������� � ��������� ��������� �������
        isDragging = true;
        originalPosition = transform.position;
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag()
    {
        // ������������� ����� ������ �� �����
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, transform.position.z);
        }
    }

    private void OnMouseUp()
    {
        // ����������� ��������������
        isDragging = false;

        // ��������� ����������� ������� � �������� ������
        bool merged = TryMergeWithPawn();

        // ���� �� ������� �������, ���������� ����� �� �������� �������
        if (!merged)
        {
            transform.position = originalPosition;
        }
    }

    float mergeRadius = 0.1f; // ������ ������� �����

    private bool TryMergeWithPawn() // ������� ����� ��� ��������������
    {   
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, mergeRadius);
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent<Pawn>(out Pawn otherPawn) && otherPawn != this && otherPawn.rank == this.rank)
            {
                // ���� ����� ����� � ��� �� ������, ������� ����� ����� � ���������� ������
                MergeWith(otherPawn);
                return true; // ���������� true, ���� ������� �������
            }
        }
        return false; // ���������� false, ���� ������� �� �������
    }
    private void OnDrawGizmos() // Gizmo ����� ��� ��������� �������
    {
        // ���������� ���� Gizmo �� �������
        Gizmos.color = Color.red;

        // ��������� ����� ������� mergeRadius ������ ������� �������
        Gizmos.DrawWireSphere(transform.position, mergeRadius);
    }


    private void MergeWith(Pawn otherPawn)
    {
        // ������� ����� ����� � ����������� ������ �� ����� ������ �����
        GameObject newPawn = Instantiate(gameObject, otherPawn.transform.position, Quaternion.identity);
        Pawn newPawnComponent = newPawn.GetComponent<Pawn>();

        // ������������� ���� � ���� ����� �����
        newPawnComponent.rank = this.rank + 1;
        newPawnComponent.damage = this.damage + 10f;
        newPawnComponent.UpdateVisual();

        // ����������� ���� ��� ������ �����
        if (this.currentSlot != null)
        {
            this.currentSlot.Free(); // ����������� ����
        }

        // ������������� ������� ���� ��� ����� �����
        newPawnComponent.SetCurrentSlot(otherPawn.currentSlot); // �������� ���� ������ �����

        // ���������� ��� ������ �����
        Destroy(this.gameObject);
        Destroy(otherPawn.gameObject);
    }

    // ���������� �������� �����
    public void SetCurrentSlot(PawnSlot slot)
    {
        currentSlot = slot; // ��������� ������� ����
    }

    // ���������� �����
    public virtual void Attack()
    {
        Enemy target = FindEnemyWithHighestProgress(); // ���� ����� � ���������� ����������
        if (target != null)
        {
            // �������� ������� ����� � ����������� ��� �� �����
            GameObject circle = Instantiate(circlePrefab, transform.position, Quaternion.identity);
            CircleProjectile projectile = circle.GetComponent<CircleProjectile>();
            projectile.Initialize(target.transform, damage); // �������� ���� � ���� ��� �����

            // ������� ���������� �� �����
            Debug.Log("����� �� ����� � ����������: " + target.progress + ", ����: " + damage);
        }
    }

    protected Enemy FindEnemyWithHighestProgress() // ������� FindEnemyWithHighestProgress ����������
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>(); // ������� ���� ������
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
