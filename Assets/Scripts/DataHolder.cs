using UnityEngine;

[CreateAssetMenu(fileName = "DataHolder", menuName = "ScriptableObjects/DataHolder", order = 1)]
public class DataHolder : ScriptableObject
{
    public int SavedCoins = 0;
    public int CoinSpawnIncrease = 0;
    public int CoinSpawnCost = 10;
}
