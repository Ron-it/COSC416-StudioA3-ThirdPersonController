using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 500f;
    [SerializeField] private float gravityMultiplier = 3f;
    [SerializeField] private float jumpForce = 10f;
    private CharacterController characterController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // TODO: Move this to GameManager
        Cursor.visible = false; // TODO: Move this to GameManager
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RotatePlayer();
    }

    private Vector3 velocity;
    private float moveAmount;
    private float verticalVelocity;

    private void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        moveAmount = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
        velocity = new Vector3(horizontal, 0f, vertical) * movementSpeed;
        velocity = Quaternion.LookRotation(new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z)) * velocity;

        if (characterController.isGrounded)
        {
            verticalVelocity = -2f;
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity += Physics.gravity.y * gravityMultiplier * Time.deltaTime;

            // Cut jump short if player releases jump button
            if (Input.GetButtonDown("Jump") && verticalVelocity > 0f)
            {
                verticalVelocity *= 0.5f;
            }
        }

        velocity.y = verticalVelocity;

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
}
