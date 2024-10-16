using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PawnDeck
{
    public List<Pawn> availablePawns = new List<Pawn>();

    public void InitializeDeck(List<Pawn> initialPawns)
    {
        availablePawns.Clear();
        availablePawns.AddRange(initialPawns);
    }

    public Pawn GetRandomPawn()
    {
        if (availablePawns.Count > 0)
        {
            int randomIndex = Random.Range(0, availablePawns.Count);
            return availablePawns[randomIndex];
        }
        return null; // Если колода пуста
    }
}
