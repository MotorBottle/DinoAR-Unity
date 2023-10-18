using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController character;
    private GameObject groundObject;
    private Vector3 direction;

    public float gravity = 9.81f * 2f;
    public float jumpForce = 8f;

    private float initialX;
    private float initialZ;

    bool JumpPressed()
    {
        // Check for keyboard input (for testing in the Unity editor)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }

        // Check for touchscreen input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) // TouchPhase.Began means the screen has just been touched
            {
                return true;
            }
        }

        return false;
    }


    private void Awake()
    {
        character = GetComponent<CharacterController>();
        groundObject = GameObject.FindGameObjectWithTag("GroundCube");
        initialX = transform.localPosition.x;
        initialZ = transform.localPosition.z;
    }

    private void OnEnable()
    {
        direction = Vector3.zero;
    }

    private void Update()
    {
        direction += Vector3.down * gravity * Time.deltaTime;

        if (character.isGrounded)
        {
            direction = Vector3.down;

            if (GameManager.Instance.CurrentState == GameManager.GameState.Playing && JumpPressed())
            {
                direction = Vector3.up * jumpForce;
            }
        }

        character.Move(direction * Time.deltaTime);

        // Fix the player's x and z positions
        Vector3 correctedLocalPosition = transform.localPosition;
        correctedLocalPosition.x = initialX;
        correctedLocalPosition.z = initialZ;
        transform.localPosition = correctedLocalPosition;
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, groundObject.transform.localEulerAngles.y + 90f, transform.localEulerAngles.z);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle")) {
            GameManager.Instance.GameOver();
        }
    }

}
