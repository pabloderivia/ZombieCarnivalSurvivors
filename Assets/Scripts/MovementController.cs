using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    public Transform cameraTransform;

    [SerializeField] public float moveSpeed = 10f;
    [SerializeField] CharacterController characterController;
    [SerializeField] Vector2 moveInput;
    [SerializeField] Vector3 moveDirection;
    [SerializeField] private float rotationSpeed = 10f; // ajusta en el inspector

    [Header("Animation")]
    AnimatorController animatorController;
    [SerializeField] float moveInputForAnimation = 0;

    void Awake()
    {
        if (characterController == null)
            characterController = GetComponent<CharacterController>();

        if (animatorController == null)
            animatorController = GetComponent<AnimatorController>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Move();
    }

    public void HandleMove(InputAction.CallbackContext context)
    {

        moveInput = context.ReadValue<Vector2>();

        //Para las animaciones
        animatorController.SetMoveInput(Mathf.Abs(moveInput.x) + Mathf.Abs(moveInput.y));

        // Direcciones relativas a la cámara
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        // Aseguramos que no afecte la inclinación de la cámara
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        // Construimos la dirección según input y cámara
        moveDirection = camForward * moveInput.y + camRight * moveInput.x;
        moveDirection.Normalize();
    }

    public void Move()
    {
        moveDirection.y = 0;
        characterController.Move(moveDirection * moveSpeed * Time.fixedDeltaTime);
        HandleRotation();

    }

    private void HandleRotation()
    {
        // Rotación suave hacia la dirección de movimiento
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.fixedDeltaTime
        );

    }
}
