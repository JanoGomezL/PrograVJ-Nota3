using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    // Configuracinn de vida y armadura
    public int vidaMaxima = 100;
    private int vidaActual;
    [Range(0, 100)] public float porcentajeArmadura;

    // Referencias para el HUD
    public TMP_Text vidaTexto;
    public TMP_Text armaduraTexto;
    public Sprite[] spritesVida;
    public Image hudVidaImage;

    // Movimiento y control del jugador
    [SerializeField]
    private Transform m_FirePoint;
    [SerializeField]
    private float m_FireRange = 10f;
    [SerializeField]
    private CharacterController m_CharacterController;
    [SerializeField]
    private float m_Speed = 5.0f;
    [SerializeField]
    private float m_JumpHeight = 2.0f;
    [SerializeField]
    private GameObject m_FireSphere;

    private Vector3 m_PlayerVelocity = Vector3.zero;
    private bool m_IsGrounded;
    private bool m_OnJump = false;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        vidaActual = vidaMaxima;
        ActualizarTextoVida();
        ActualizarTextoArmadura();
        ActualizarSpriteVida();
        // Bloquea y oculta el cursor en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Si necesitas desbloquear el cursor temporalmente (por ejemplo, para abrir un menn), puedes agregar esta lngica
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Input.GetMouseButtonDown(0)) // Bloquea de nuevo cuando se haga clic
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            RecibirDanoDesdeEnemigo(10);  // Ejemplo para probar el dano
        }
    }

    public void RecibirDanoDesdeEnemigo(int dano)
    {
        int danoReducido = CalcularDanoConArmadura(dano);
        vidaActual -= danoReducido;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);

        Debug.Log($"Dano recibido: {danoReducido}. Vida actual: {vidaActual}");

        ActualizarTextoVida();
        ActualizarSpriteVida();
    }

    private int CalcularDanoConArmadura(int dano)
    {
        float reduccinn = dano * (porcentajeArmadura / 100f);
        int danoFinal = Mathf.RoundToInt(dano - reduccinn);
        return danoFinal;
    }

    private void ActualizarTextoVida()
    {
        vidaTexto.text = vidaActual.ToString() + "%";
    }

    private void ActualizarTextoArmadura()
    {
        armaduraTexto.text = porcentajeArmadura.ToString("F0") + "%";
    }

    public void AjustarArmadura(float nuevoPorcentaje)
    {
        porcentajeArmadura = nuevoPorcentaje;
        ActualizarTextoArmadura();
    }

    private void ActualizarSpriteVida()
    {
        int index = (vidaMaxima - vidaActual) / 15;
        index = Mathf.Clamp(index, 0, spritesVida.Length - 1);
        hudVidaImage.sprite = spritesVida[index];
    }

    // Movimiento del jugador
    public void Move(Vector2 movement)
    {
        m_IsGrounded = m_CharacterController.isGrounded;

        if (m_IsGrounded && m_PlayerVelocity.y < 0)
        {
            m_PlayerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(movement.x, 0, movement.y) * m_Speed;

        if (m_OnJump && m_IsGrounded)
        {
            m_PlayerVelocity.y += Mathf.Sqrt(-2f * Physics.gravity.y * m_JumpHeight);
            m_OnJump = false;
        }

        // Rotacinn y movimiento en referencia a la cnmara
        move = Camera.main.transform.forward * move.z + Camera.main.transform.right * move.x;
        move.y = 0f;

        Quaternion targetRotation = Quaternion.RotateTowards(transform.rotation, Camera.main.transform.rotation, 300f * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);

        m_PlayerVelocity.y += Physics.gravity.y * Time.deltaTime;
        move = new Vector3(move.x * Time.deltaTime, m_PlayerVelocity.y * Time.deltaTime, move.z * Time.deltaTime);

        m_CharacterController.Move(move);
    }

    public void Jump()
    {
        m_OnJump = true;
    }

    public void Fire()
    {
        // Lanzar un raycast desde el punto de disparo
        RaycastHit hit;
        bool collision = Physics.Raycast(m_FirePoint.position, Camera.main.transform.forward, out hit, m_FireRange);
        if (collision)
        {
            Instantiate(m_FireSphere, hit.point, Quaternion.identity);
        }
    }
}
