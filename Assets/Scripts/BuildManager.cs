using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;

    private void Awake() {
        if(instance != null) {
            Debug.Log("More than one BuildManager in scene");
        }
        instance = this;
    }

    public GameObject buildEffect;

    private TurretBlueprint turretToBuild;

    public bool canBuild { get { return turretToBuild != null; } }
    public bool hasMoney { get { return PlayerStats.money >= turretToBuild.cost; } }

    public void buildTurretOn(Node node) {

        if (PlayerStats.money < turretToBuild.cost) {
            Debug.Log("Not Enough money");
            return;
        }

        PlayerStats.money -= turretToBuild.cost;

        GameObject turret = (GameObject) Instantiate(turretToBuild.prefab, node.getBuildPosition(), Quaternion.identity);
        node.turret = turret;

        GameObject effect = (GameObject) Instantiate(buildEffect, node.getBuildPosition(), Quaternion.identity);
        Destroy(effect, 5);
    }

    public void selectTurretToBuild(TurretBlueprint turret) {
        turretToBuild = turret;
    }

}
