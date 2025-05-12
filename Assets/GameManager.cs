using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private int score = 0; // score empieza en 0
    // 2.1. El jugador debe de tener 3 vidas.
    private int lives = 3; // 3 vidas

    public int Score
    {
        get => score;
    }

    public void AddScore()
    {
        this.score += 10;
        Debug.Log("Puntos: " + score);
    }

    public void PlayerHit()
    {
        lives--;
        Debug.Log("Vidas restantes: " + lives);

        if (lives <= 0) // si se nos acabo la vida, bye bye
        {
            RestartGame();
        }
    }

    // 	2.2. Al finalizar el juego, se reinicia la escena.
    private void RestartGame()
    {
        Debug.Log("Game Over. Reiniciando...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
