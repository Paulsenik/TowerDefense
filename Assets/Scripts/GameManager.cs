using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameEnded = false;

    // Update is called once per frame
    void Update() {
        if (gameEnded)
            return;
        if (PlayerStats.lives <= 0) {
            endGame();
        }
    }

    private void endGame() {
        Debug.Log("Game Over!");
        gameEnded = true;
        SceneManager.LoadScene(0);
    }
}
