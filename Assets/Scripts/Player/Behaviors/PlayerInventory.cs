using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour

{
    [Header("Things I Found")]
    public int coins;
    public UpgradeData[] upgrades;

    public void AddCoins(int moreCoins)
    {
        coins += moreCoins;
    }

}
