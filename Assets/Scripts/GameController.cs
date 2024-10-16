using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PawnDeck playerDeck = new PawnDeck(); // Колода игрока
    public GameObject pawnPrefab; // Префаб пешки

    private void Start()
    {
        // Пример инициализации колоды
        List<Pawn> initialPawns = new List<Pawn>
        {
            // Добавьте сюда свои пешки
            Resources.Load<Pawn>("PawnPrefabs/Archer"),
            Resources.Load<Pawn>("PawnPrefabs/Knight"),
            Resources.Load<Pawn>("PawnPrefabs/Warrior"),
            Resources.Load<Pawn>("PawnPrefabs/Mage"),
            Resources.Load<Pawn>("PawnPrefabs/Rogue")
        };

        playerDeck.InitializeDeck(initialPawns);
    }

    public void BuyPawn()
    {
        Pawn randomPawn = playerDeck.GetRandomPawn();
        if (randomPawn != null)
        {
            // Создаем новый объект пешки
            Instantiate(randomPawn, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}
