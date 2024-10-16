using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PawnDeck playerDeck = new PawnDeck(); // ������ ������
    public GameObject pawnPrefab; // ������ �����

    private void Start()
    {
        // ������ ������������� ������
        List<Pawn> initialPawns = new List<Pawn>
        {
            // �������� ���� ���� �����
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
            // ������� ����� ������ �����
            Instantiate(randomPawn, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}
