using UnityEngine;

public class Bullet : MonoBehaviour {

    private Transform target;

    public int damagePoints = 50;
    public float speed = 70f;
    public float effectDuration = 3f;
    public float explosionRadius = 0;
    public GameObject impactEffect;

    public void Seek(Transform _target) {
        target = _target;
    }

    // Update is called once per frame
    void Update(){
        if(target == null) {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame) {
            hitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);

    }

    void hitTarget() {
        GameObject effectIns = (GameObject) Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, effectDuration);

        if(explosionRadius > 0f) {
            explode();
        } else {
            damage(target);
        }

        Destroy(gameObject);
    }

    void explode() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders) {
            if(collider.tag == "Enemy") {
                damage(collider.transform);
            }
        }
    }

    void damage(Transform enemy) {
        Enemy e = enemy.GetComponent<Enemy>();

        if(e != null) {
            e.takeDamage(damagePoints);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

}
