using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text ManaText;
    public Text CostText;
    public int mana = 100;
    public GameObject pawnPrefab;
    public Transform board;
    private List<PawnSlot> availableSlots = new List<PawnSlot>();
    public int cost = 10;
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public Path path;
    public int gamerHealth = 3;
    public GameObject panel;

    private void Start()
    {
        InitializeBoardSlots();
        StartCoroutine(SpawnEnemies());
        UpdateUI();
    }
    public void IncreaseMana(int amount)
    {
        mana += amount;
        UpdateUI();
    }
    private void UpdateUI()
    {
        ManaText.text = "Мана: " + mana;
        CostText.text = "Стоимость: " + cost;
    }

    public void PlayerHealth()
    {
        gamerHealth -= 1;
        UpdateUI();
        if (gamerHealth <= 0) 
        {
            //Time.timeScale = 0;
            pause();
        }
    }

    public void pause()
    {
        panel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void InitializeBoardSlots()
    {
        foreach (Transform child in board)
        {
            PawnSlot slot = child.GetComponentInChildren<PawnSlot>();
            if (slot != null)
            {
                availableSlots.Add(slot);
            }
        }
        Debug.Log("Количество слотов: " + availableSlots.Count);
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        if (path.WaypointsCount() > 0)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, path.GetWaypoint(0).position, Quaternion.identity);
            Enemy enemy = newEnemy.GetComponent<Enemy>();
            enemy.InitializePath(path.waypoints);
        }
    }

    public void BuyPawn()
    {
        if (mana >= cost && availableSlots.Any(slot => !slot.isOccupied))
        {
            mana -= cost;
            cost += 10;
            SpawnRandomPawn();
        }
        else
        {
            Debug.LogWarning("Недостаточно маны или нет свободных слотов!");
        }
        UpdateUI();
    }

    private void SpawnRandomPawn()
    {
        Vector3 randomPosition = FindRandomEmptySlot();
        if (randomPosition != Vector3.zero)
        {
            GameObject newPawn = Instantiate(pawnPrefab, randomPosition, Quaternion.identity);
            Pawn pawn = newPawn.GetComponent<Pawn>();

            // Передаем текущий слот в метод инициализации
            PawnSlot currentSlot = availableSlots.Find(slot => slot.transform.position == randomPosition);
            pawn.Initialize(1, 10f, path.waypoints, currentSlot);
        }
    }

    private Vector3 FindRandomEmptySlot()
    {
        List<PawnSlot> freeSlots = availableSlots.FindAll(slot => !slot.isOccupied);
        if (freeSlots.Count == 0) return Vector3.zero;

        int randomIndex = Random.Range(0, freeSlots.Count);
        PawnSlot chosenSlot = freeSlots[randomIndex];
        chosenSlot.isOccupied = true; // Отметим слот как занятый
        return chosenSlot.transform.position;
    }
}
