using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 500f;
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

    private void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        moveAmount = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
        velocity = new Vector3(horizontal, 0f, vertical) * movementSpeed;

        characterController.Move(velocity * Time.deltaTime);
    }

    private void RotatePlayer()
    {
        if (moveAmount > 0)
        {
            var targetRotation = Quaternion.LookRotation(velocity);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
