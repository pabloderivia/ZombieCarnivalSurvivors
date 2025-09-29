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


    void Awake()
    {
        if (characterController == null)
            characterController = GetComponent<CharacterController>();
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
        characterController.Move(moveDirection * moveSpeed*Time.fixedDeltaTime);
        // Rotación suave hacia la dirección de movimiento
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.fixedDeltaTime
        );
    }

    public void HandleMove(InputAction.CallbackContext context)
    {

        moveInput = context.ReadValue<Vector2>();

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
}
