using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretUI : MonoBehaviour
{
    public GameObject ui;

    public Text upgradeCost;
    public Button upgradeButton;

    public Text sellAmount;

    public AudioSource sellAudio;

    private Node target;

    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
            sellAmount.text = "$" + target.turretBlueprint.GetSellAmount();
        }
        else if (target.isUpgraded)
        {
            target.isSold = false;
            upgradeCost.text = "$" + target.turretBlueprint.secondUpgradeCost;
            upgradeButton.interactable = true;
            sellAmount.text = "$" + target.turretBlueprint.GetUpgradedSellAmount();
        }
        if (target.isUpgradedTwo)
        {
            target.isUpgraded = false;
            upgradeCost.text = "DONE";
            upgradeButton.interactable = false;
            sellAmount.text = "$" + target.turretBlueprint.GetSecondUpgradeSellAmount(); 
        }

        if (target.isSold)
        {
            target.isUpgraded = false;
            target.isUpgradedTwo = false;
        }

        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        if (target.isUpgraded == false)
        {
            target.UpgradeTurret();
        }
        else if (target.isUpgraded == true)
        {
            target.UpgradeTurretAgain();
        }
        
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        if (target.isUpgraded == false && target.isUpgradedTwo == false)
        {
            sellAudio.Play();
            target.SellTurret();
            BuildManager.instance.DeselectNode();
        }
        else if (target.isUpgraded == true && target.isUpgradedTwo == false)
        {
            sellAudio.Play();
            target.SellTurretAgain();
            BuildManager.instance.DeselectNode();
        }
        if (target.isUpgradedTwo == true && target.isUpgraded == false)
        {
            sellAudio.Play();
            target.SellTurretAgainTwo();
            BuildManager.instance.DeselectNode();
        }
        
    }
}
