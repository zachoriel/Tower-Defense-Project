using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one build manager in scene, dummy!");
            return;
        }
        instance = this;
    }

    public GameObject buildEffect;

    private TurretBlueprint turretToBuild;
    private Node selectedTurret;

    public TurretUI turretUI;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }
    public bool HasEnergy { get { return PlayerStats.Energy >= turretToBuild.energyCost; } }

    public void SelectTurret(Node node)
    {
        if (selectedTurret == node)
        {
            DeselectNode();
            return;
        }

        selectedTurret = node;
        turretToBuild = null;

        turretUI.SetTarget(node);
    }
     
    public void DeselectNode()
    {
        selectedTurret = null;
        turretUI.Hide();
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;

        DeselectNode();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }

}
