using UnityEngine;

public class CoinSpin : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField]
    private float rotationSpeed = 100f; // degrees per second

    [Header("Bounce Settings")]
    [SerializeField]
    private float amplitude = 0.5f; // Maximum upward offset
    [SerializeField]
    private float frequency = 1f; // Bounce speed

    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
    }

    void Update()
    {
        // Rotate around the Y-axis
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);

        // Bounce up and down so that it never goes below the start position.
        // This maps the sine value, which oscillates between -1 and 1, into a 0 to 1 range.
        float bounce = (Mathf.Sin(Time.time * frequency) + 1f) / 2f;
        transform.position = new Vector3(_startPosition.x,
            _startPosition.y + (bounce * amplitude), _startPosition.z);
    }

    // When the player triggers the coin
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Attempt to add score using the GameManager
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(1);
            }
            Destroy(gameObject);
        }
    }
}
