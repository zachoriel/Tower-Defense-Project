using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public AudioSource notEnoughMoney;
    public AudioSource notEnoughEnergy;
    public AudioSource notEnoughResources;

    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;
    public bool isUpgradedTwo = false;
    [HideInInspector]
    public bool isSold = false;
    public bool isSoldTwo = false;
    public bool isSoldThree = false;

    private Renderer rend;

    private Color startColor;

    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (turret != null)
        {
            buildManager.SelectTurret(this);
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;           
        }

        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost && PlayerStats.Energy < blueprint.energyCost)
        {
            Debug.Log("Not enough money or energy to build that!");
            notEnoughResources.Play();
            return;
        }
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to build that!");
            notEnoughMoney.Play();
            return;
        }
        if (PlayerStats.Energy < blueprint.energyCost)
        {
            Debug.Log("Not enough energy to build that!");
            notEnoughEnergy.Play();
            return;
        }

        PlayerStats.Money -= blueprint.cost;
        PlayerStats.Energy -= blueprint.energyCost;
        PlayerStats.Energy += blueprint.energyGeneration;

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Debug.Log("Turret built!");
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade that!");
            notEnoughMoney.Play();
            return;
        }

        PlayerStats.Money -= turretBlueprint.upgradeCost;
        PlayerStats.Energy += turretBlueprint.upgradedEnergyGeneration;

        // Get rid of old turret
        Destroy(turret);

        // Build new turret
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;

        Debug.Log("Turret upgraded!");
    }

    public void UpgradeTurretAgain()
    {
        if (PlayerStats.Money < turretBlueprint.secondUpgradeCost)
        {
            Debug.Log("Not enough money to upgrade that!");
            notEnoughMoney.Play();
            return;
        }

        PlayerStats.Money -= turretBlueprint.secondUpgradeCost;
        PlayerStats.Energy += turretBlueprint.secondUpgradeEnergyGeneration;

        // Get rid of old turret
        Destroy(turret);

        // Build new turret
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.secondUpgradePrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = false;
        isUpgradedTwo = true;

        Debug.Log("Turret upgraded!");
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();
        PlayerStats.Energy += turretBlueprint.GetEnergyReturnAmount();
        PlayerStats.Energy -= turretBlueprint.energyGeneration;

        //Spawn effect

        Destroy(turret);
        turretBlueprint = null;

        isSold = true;

        Debug.Log("Turret sold!");
    }
    public void SellTurretAgain()
    {
        PlayerStats.Money += turretBlueprint.GetUpgradedSellAmount();
        PlayerStats.Energy += turretBlueprint.GetEnergyReturnAmount();
        PlayerStats.Energy -= turretBlueprint.energyGeneration + turretBlueprint.upgradedEnergyGeneration;

        //Spawn effect

        Destroy(turret);
        turretBlueprint = null;

        isSold = true;

        Debug.Log("Turret sold!");
    }
    public void SellTurretAgainTwo()
    {
        PlayerStats.Money += turretBlueprint.GetSecondUpgradeSellAmount();
        PlayerStats.Energy += turretBlueprint.GetEnergyReturnAmount();
        PlayerStats.Energy -= turretBlueprint.energyGeneration + turretBlueprint.upgradedEnergyGeneration + turretBlueprint.secondUpgradeEnergyGeneration;

        //Spawn effect

        Destroy(turret);
        turretBlueprint = null;

        isSold = true;

        Debug.Log("Turret sold!");
    }

    void OnMouseEnter ()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }
        
        if (buildManager.HasMoney && buildManager.HasEnergy)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
        
    }

    void OnMouseExit ()
    {
        rend.material.color = startColor;
    }

}
