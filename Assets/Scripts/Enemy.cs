using UnityEngine;
using UnityEngine.Assertions.Must;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;

    public int health = 100;

    public int value = 50;
    public GameObject deathEffect;

    private Transform target;
    private int wavepointIndex = 0;

    private void Start() {
        target = Waypoints.points[0];
    }

    private void Update() {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, target.position) <= 0.4f) {
            GetNextWaypoint();
        }
    }

    public void takeDamage(int amount) {
        health -= amount;

        if(health <= 0) {
            Debug.Log("dead");
            die();
        }
    }

    void die() {
        GameObject effect = (GameObject) Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        PlayerStats.money += value;
        Destroy(gameObject);
    }

    void GetNextWaypoint() {
        if(wavepointIndex >= Waypoints.points.Length - 1) {
            endPath();
            return;
        } else { 
            wavepointIndex++;
            target = Waypoints.points[wavepointIndex];
        }
    }

    void endPath() {
        PlayerStats.lives--;
        Destroy(gameObject);
    }

}
