using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI points;
    public GameObject[] lives;
    // Update is called once per frame
    void Update()
    {
        points.text = GameManager.Instance.TotalPoints.ToString();
    }

    public void ActualizarPuntos(int totalPoints)
    {
        points.text = totalPoints.ToString();
    }

    public void DesactivarVida(int indice)
    {
        lives[indice].SetActive(false);
    }

    public void ActivarVida(int indice)
    {
        lives[indice].SetActive(true);
    }
}
