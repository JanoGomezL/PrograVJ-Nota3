using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnContinuar : MonoBehaviour
{
    public GameObject menuPausa;
    public GameObject shotgun;

    public void Continue()
    {
        shotgun.SetActive(true);
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
