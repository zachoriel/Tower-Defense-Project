using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserBeamer;
    public TurretBlueprint sniperTurret;
    public TurretBlueprint energyGenerator;

    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardTurret()
    {
        Debug.Log("Standard Turret Selected");
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void SelectMissileLauncher()
    {
        Debug.Log("Missile Launcher Selected");
        buildManager.SelectTurretToBuild(missileLauncher);
    }

    public void SelectLaserBeamer()
    {
        Debug.Log("Laser Beamer Selected");
        buildManager.SelectTurretToBuild(laserBeamer);
    }

    public void SelectSniperTurret()
    {
        Debug.Log("Sniper Turret Selected!");
        buildManager.SelectTurretToBuild(sniperTurret);
    }

    public void SelectEnergyGenerator()
    {
        Debug.Log("Energy Generator selected!");
        buildManager.SelectTurretToBuild(energyGenerator);
    }
}
