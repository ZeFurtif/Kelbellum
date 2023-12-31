using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private PlayerData playerData;

    [Header("Movement")]
    private float gravityValue = -9.81f; 

    private CharacterController controller;
    private Animator animator;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    [Header("Knockback")]
    public Vector3 knockbackVector;
    public float knockbackDuration = 0.2f;
    private Vector3 knockbackRef = Vector3.zero;

    [Header("Aim")]
    public bool faceAimDir;

    public Vector2 movementInput = Vector2.zero;
    public Vector2 aimInput = Vector2.zero;

    Camera cam;
    Vector2 screenSize;
    Vector2 viewportSize;

    //INIT
    private void Start()
    {
        playerData = gameObject.GetComponent<PlayerData>();
        
        controller = gameObject.GetComponent<CharacterController>();

        cam = Camera.main.GetComponent<Camera>();
        screenSize = new Vector2(Screen.width, Screen.height);
        viewportSize = new Vector2(480, 270);
    }

    public void LoadPlayerData(PlayerData newData)
    {
        playerData = newData;
    }


    //INPUT GET
    public void OnMove(InputAction.CallbackContext context) 
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        aimInput = context.ReadValue<Vector2>();
        if (context.control.device.description.deviceClass == "Mouse" && cam != null) {
            var posToScreen = cam.WorldToScreenPoint(this.gameObject.transform.position);

            posToScreen = posToScreen / viewportSize;        
            aimInput = aimInput / screenSize;
            
            aimInput = aimInput - new Vector2(posToScreen.x, posToScreen.y);
            aimInput.Normalize();
        }
    }

    public void OnKnockback(Vector3 knockback)
    {
        knockbackVector = Vector3.Normalize(knockback) * 10 / playerData.size; 
    }


    //MOVEMENT
    private void HandleMovement() 
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        controller.Move(move * Time.deltaTime * playerData.speed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void HandleKnockback()
    {
        knockbackVector = Vector3.SmoothDamp(knockbackVector, Vector3.zero, ref knockbackRef , knockbackDuration);
        controller.Move(knockbackVector * Time.deltaTime);
    }

    private void HandleAim()
    {
        if (aimInput != Vector2.zero)
        {
            Vector3 aimVector = new Vector3(aimInput.x, 0, aimInput.y);
            gameObject.transform.forward = aimVector;
        }
    }

    void Update()
    {
        HandleMovement();
        HandleKnockback();
        if (!faceAimDir)
        {
            HandleAim();
        }
    }
}