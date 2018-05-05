using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    public GameObject prefab;
    public int cost;
    public int energyCost;
    public int energyGeneration;

    public GameObject upgradedPrefab;
    public int upgradeCost;
    public int upgradedEnergyGeneration;

    public GameObject secondUpgradePrefab;
    public int secondUpgradeCost;
    public int secondUpgradeEnergyGeneration;

    public int GetSellAmount()
    {
        return cost / 2; 
    }

    public int GetUpgradedSellAmount()
    {
        return upgradeCost / 2;
    }

    public int GetSecondUpgradeSellAmount()
    {
        return secondUpgradeCost / 2;
    }

    public int GetEnergyReturnAmount()
    {
        return energyCost;
    }

}
