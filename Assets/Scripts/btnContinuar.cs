using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnContinuar : MonoBehaviour
{
    public GameObject menuPausa;
    public GameObject shotgun;

    public void Continue()
    {
        if (menuPausa.Equals(true))
        {

        }
        menuPausa.SetActive(false);
        shotgun.SetActive(true);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
