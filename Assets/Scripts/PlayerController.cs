using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 500f;
    [SerializeField] private float gravityMultiplier = 3f;
    [SerializeField] private float jumpForce = 10f;

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.2f;
    private bool isDashing = false;
    private float dashTime;

    private CharacterController characterController;

    private Vector3 velocity;
    private float moveAmount;
    private float verticalVelocity;

    private bool canDoubleJump = false; // Tracks if the player can double-jump

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!isDashing)
        {
            MovePlayer();
            RotatePlayer();
        }
        HandleDash();
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        moveAmount = Mathf.Abs(horizontal) + Mathf.Abs(vertical);

        // Calculate movement direction
        velocity = new Vector3(horizontal, 0f, vertical) * movementSpeed;
        velocity = Quaternion.LookRotation(new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z)) * velocity;

        if (characterController.isGrounded)
        {
            verticalVelocity = -2f; // Reset vertical velocity when grounded
            canDoubleJump = true; // Reset double-jump ability

            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity += Physics.gravity.y * gravityMultiplier * Time.deltaTime;

            // Handle double-jump
            if (Input.GetButtonDown("Jump") && canDoubleJump)
            {
                verticalVelocity = jumpForce;
                canDoubleJump = false; // Disable double-jump after use
            }
        }

        velocity.y = verticalVelocity;

        // Move the character
        characterController.Move(velocity * Time.deltaTime);
    }

    private void RotatePlayer()
    {
        if (moveAmount > 0)
        {
            var targetRotation = Quaternion.LookRotation(new Vector3(velocity.x, 0f, velocity.z));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void HandleDash()
    {
        // Start dash when "E" is pressed and not already dashing
        if (Input.GetKeyDown(KeyCode.E) && !isDashing)
        {
            isDashing = true;
            dashTime = dashDuration;
        }

        // Perform dash
        if (isDashing)
        {
            dashTime -= Time.deltaTime;

            // Dash movement in the current forward direction
            Vector3 dashVelocity = transform.forward * dashSpeed;
            characterController.Move(dashVelocity * Time.deltaTime);

            // End dash after the duration
            if (dashTime <= 0)
            {
                isDashing = false;
            }
        }
    }
}
