using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour{

    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserBeamer;

    BuildManager buildManager;

    private void Start() {
        buildManager = BuildManager.instance;
    }

    public void selectStandardTurret() {
        Debug.Log("Standard Turret Selected");
        buildManager.selectTurretToBuild(standardTurret);
    }
    
    public void selectMissileLauncher() {
        Debug.Log("Missile Launcher Selected");
        buildManager.selectTurretToBuild(missileLauncher);
    }
    public void selectLaserBeamer() {
        Debug.Log("Laser Beamer Selected");
        buildManager.selectTurretToBuild(laserBeamer);
    }

}
