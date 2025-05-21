using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public HUD hud;
    public int TotalPoints { get { return totalPoints; } }
    private int totalPoints;
    private int lives = 3;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("More than one GameManager in scene!");       
        }
    }

    public void sumarPuntos(int puntosASumar)
    {
        totalPoints += puntosASumar;
        hud.ActualizarPuntos(totalPoints);
    }

    public void PerderVida()
    {
        lives -= 1;

        if (lives == 0)
        {
            SceneManager.LoadScene(0);
        }

        hud.DesactivarVida(lives);
    }

    public bool RecuperarVida()
    {
        if(lives == 3)
        {
            return false;
        }
        hud.ActivarVida(lives);
        lives += 1;
        return true;
    }
}
