using UnityEngine;
using UnityEngine.UI;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausa;
    public Button btnContinue;
    public Button btnSalir;
    public GameObject shotgun;

    private bool isPaused = false;

    void Start()
    {
        menuPausa.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        btnContinue.onClick.AddListener(ContinueGame);
        btnSalir.onClick.AddListener(ExitGame);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.F))
        {
            if (isPaused)
            {
                ContinueGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        isPaused = true;
        shotgun.SetActive(false);
        menuPausa.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ContinueGame()
    {
        isPaused = false;
        shotgun.SetActive(true);
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void ExitGame()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }
}
