using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float runSpeed = 10f;
    public float crouchSpeed = 2f;
    public float fov = 60f;
    public float mouseSensitivity = 100f;

    private bool isRunning = false;
    private bool isCrouching = false;

    private float verticalInput;
    private float horizontalInput;

    private float rotationX = 0f;
    private float rotationY = 0f;

    public void Start()
    {
        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Set camera FOV
        Camera.main.fieldOfView = fov;
    }

    public void Update()
    {
        // Get input from player
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        // Move player
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        movement = Vector3.ClampMagnitude(movement, 1f);
        if (isCrouching)
        {
            movement *= crouchSpeed;
        }
        else if (isRunning)
        {
            movement *= runSpeed;
        }
        else
        {
            movement *= speed;
        }
        movement *= Time.deltaTime;
        transform.Translate(movement);

        // Crouch and run controls
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = !isCrouching;
            if (isCrouching)
            {
                isRunning = false;
                transform.localScale = new Vector3(1f, 0.5f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = !isRunning;
            if (isRunning)
            {
                isCrouching = false;
            }
        }

        // Control camera with mouse
        CameraRotation();

        // Set camera FOV
        Camera.main.fieldOfView = fov;

        // Hide cursor
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Cursor.visible)
        {
            Cursor.visible = false;
        }
    }

    private void CameraRotation()
    {
        // Get the mouse input axis values
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Calculate the new rotation angles based on the mouse input
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        rotationY += mouseX;

        // Apply the new rotation angles to the camera and player objects
        transform.localRotation = Quaternion.Euler(0f, rotationY, 0f);
        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }
}
