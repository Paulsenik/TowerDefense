using System;
using System.Threading;
using UnityEngine;

public class Turret : MonoBehaviour {

    private Transform target;

    [Header("General")]

    public float range = 15f;

    [Header("Use Bullets")]

    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Use Laser (optional)")]
    public Boolean useLaser = false;
    public LineRenderer lineRenderer;

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 10f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("updateTarget", 0f, 0.5f);
    }

    private void Update() {
        if (target == null) {
            if (useLaser && lineRenderer.enabled) {
                    lineRenderer.enabled = false;
            }
            return;
        }

        lockOnTarget();

        if (useLaser) {
            laser();
        } else {

            if (fireCountdown <= 0f) {
                shoot();
                fireCountdown = 1f / fireRate;
            }
        }

        fireCountdown -= Time.deltaTime;

    }

    void lockOnTarget() {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void laser() {
        if (!lineRenderer.enabled)
            lineRenderer.enabled = true;

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);
    }

    void shoot() {
        GameObject bulletGO = (GameObject) Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if(bullet != null) {
            bullet.Seek(target);
        }
    }

    void updateTarget() {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies) {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance) {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range) {
            target = nearestEnemy.transform;
        } else {
            target = null;
        }

    }


    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
