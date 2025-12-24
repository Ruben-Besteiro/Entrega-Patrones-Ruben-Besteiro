using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Actions")]
    [SerializeField] InputActionProperty moveAction;
    [SerializeField] InputActionProperty jumpAction;
    [SerializeField] InputActionProperty lookAction;
    [SerializeField] InputActionProperty shootAction;
    Vector3 movementVector;

    [Header("Stuff")]
    [SerializeField] float maxSpeed = 10;
    [SerializeField] float sensibilityX = 0.1f;
    [SerializeField] float sensibilityY = 0.05f;
    [SerializeField] Transform cameraTransform;
    Rigidbody rb;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject shootPosition;

    private float rotacionX = 0;

    private void Awake()
    {
        moveAction.action.Enable();
        jumpAction.action.Enable();
        lookAction.action.Enable();
        shootAction.action.Enable();
        jumpAction.action.started += Jump;
        shootAction.action.started += Shoot;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Jump(InputAction.CallbackContext ctx)
    {
        rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
    }

    void Shoot(InputAction.CallbackContext ctx)
    {
        StartCoroutine(IEShoot());
    }

    IEnumerator IEShoot()
    {
        GameObject e = Instantiate(bullet, shootPosition.transform.position, shootPosition.transform.rotation);
        e.GetComponent<Rigidbody>().AddForce(transform.forward * 100, ForceMode.Impulse);
        yield return new WaitForSeconds(2);
        Destroy(e);
    }

    void Update()
    {
        Vector2 lookInput = lookAction.action.ReadValue<Vector2>();

        // Rotación en Y (rota el cuerpo del jugador)
        transform.Rotate(Vector3.up * lookInput.x * sensibilityY);
    }

    private void FixedUpdate()
    {
        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();
        // Convertir input a espacio local del jugador
        movementVector = transform.right * moveInput.x + transform.forward * moveInput.y;
        movementVector.y = 0; // Mantener en el plano horizontal

        if (rb.linearVelocity.magnitude < maxSpeed) rb.AddForce(movementVector * 50, ForceMode.Acceleration);
        if (moveInput == Vector2.zero) rb.linearVelocity = Vector3.zero;
    }
}