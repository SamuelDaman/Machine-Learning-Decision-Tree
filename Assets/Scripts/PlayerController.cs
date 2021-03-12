using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 move;
    private bool isGrounded;
    private float yVelocity = 0f;
    public float speed = 1f;
    public float acceleration = 0.75f;
    public float jumpForce = 10f;
    public float gravity = -9.81f;

    private Camera cam;
    private bool isLocked = true;
    private float mouseX = 0f;
    private float mouseY = 0f;
    public float xSpeed = 5f;
    public float ySpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        MouseLockState();

        isGrounded = controller.isGrounded;
        if (isGrounded && yVelocity < 0)
        {
            yVelocity = 0f;
        }

        float delta = Time.deltaTime;
        Vector3 input = transform.TransformDirection(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        Vector3.SmoothDamp(move, input * speed, ref move, speed / acceleration, speed, delta);
        if (input == Vector3.zero && move.magnitude < 0.01f)
        {
            move = Vector3.zero;
        }
        controller.Move(move);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            yVelocity = jumpForce;
        }
        yVelocity += gravity * delta;
        controller.Move(new Vector3(0, yVelocity, 0) * delta);
    }

    void MouseLockState()
    {
        if (isLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            MouseLook();
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                isLocked = false;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            if (Input.GetKeyDown(KeyCode.P))
            {
                isLocked = true;
            }
        }
    }

    void MouseLook()
    {
        mouseX += Input.GetAxis("Mouse X") * xSpeed;
        mouseY += Input.GetAxis("Mouse Y") * ySpeed;

        mouseY = Mathf.Clamp(mouseY, -90, 90);

        transform.eulerAngles = new Vector3(0, mouseX, 0);
        cam.transform.localEulerAngles = new Vector3(-mouseY, 0, 0);
    }
}
