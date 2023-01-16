using System.Collections;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour{

    public Transform enemyPrefab;
    public Transform spawnPoint;

    public Text waveCountdownText;

    public float timeBetweenWaves = 5f;
    public float spawnDelay = 0.5f;

    private float countdown = 2f;
    private int waveNumber = 0;

    private void Update() {
        if(countdown <= 0f) {
            StartCoroutine(spawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator spawnWave() {
        waveNumber++;
        for (int i = 0; i < waveNumber; i++) {
            spawnEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }

    }

    void spawnEnemy() {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

}
