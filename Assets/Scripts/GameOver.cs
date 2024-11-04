using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject gameOver;
    public GameObject hud;
    public GameObject menuPausa;
    public Button btnSalir;

    private void Start()
    {
        gameOver.SetActive(false);
        btnSalir.onClick.AddListener(ExitGame);
    }

    private void Update()
    {
        if (PlayerController.Instance != null && PlayerController.Instance.GetVidaActual() <= 0)
        {
            ActivateGameOver();
            hud.SetActive(false);
            menuPausa.SetActive(false);
        }
    }

    public void ActivateGameOver()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void ExitGame()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }
}
