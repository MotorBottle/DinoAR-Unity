//using UnityEngine;

//public class Obstacle : MonoBehaviour
//{
//    public GameObject groundObject;
//    private float leftEdge;

//    private void Start()
//    {
//        float groundWidth = groundObject.transform.localScale.x; // Get the scale along the x-axis
//        leftEdge = groundObject.transform.position.x - (groundWidth / 2f) - 0.3f;
//    }

//    private void Update()
//    {
//        transform.position += Vector3.left * GameManager.Instance.gameSpeed * Time.deltaTime;

//        if (transform.position.x < leftEdge)
//        {
//            Destroy(gameObject);
//        }
//    }
//}
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private GameObject groundObject;
    private float leftEdge;

    private float initialX;
    private float initialZ;

    private void Start()
    {
        groundObject = GameObject.FindGameObjectWithTag("GroundCube");
        float groundWidth = groundObject.transform.localScale.x; // Get the scale along the x-axis
        leftEdge = groundObject.transform.localPosition.x - (groundWidth / 2f);  // Use localPosition here

        initialX = transform.localPosition.x;  // Capture initialX just in case you need it in the future
        initialZ = transform.localPosition.z;  // Use localPosition
    }

    private void Update()
    {
        Vector3 movementDirection = groundObject.transform.right * -1; // Move opposite to the right direction of the ground object
        transform.localPosition += movementDirection * GameManager.Instance.gameSpeed * Time.deltaTime;  // This uses global coordinates because of the CharacterController-like movement
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, groundObject.transform.localEulerAngles.y, transform.localEulerAngles.z);

        //// Fix the obstacle's z position using localPosition
        //Vector3 correctedLocalPosition = transform.localPosition;
        //correctedLocalPosition.z = initialZ;
        //transform.localPosition = correctedLocalPosition;

        if (transform.localPosition.x < leftEdge - 0.95f)  // Use localPosition here
        {
            Destroy(gameObject);
        }
    }
}
