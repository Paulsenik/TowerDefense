using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

    public Color hoverColor, notEnoughMoneyColor;
    public Vector3 positionOffset;

    [Header("Optional")]
    public GameObject turret;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    private void Start() {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 getBuildPosition() {
        return transform.position + positionOffset;
    }

    private void OnMouseDown() {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (!buildManager.canBuild)
            return;

        if (turret != null) {
            Debug.Log("Can't build here! - TODO");
            return;
        }

            buildManager.buildTurretOn(this);

    }

    private void OnMouseEnter() {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.canBuild)
            return;

        if (buildManager.hasMoney) {
            rend.material.color = hoverColor;
        } else {
            rend.material.color = notEnoughMoneyColor;
        }

    }

    private void OnMouseExit() {
        rend.material.color = startColor;
    }

}
