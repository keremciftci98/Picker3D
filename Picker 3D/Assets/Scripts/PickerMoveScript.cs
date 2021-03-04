using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickerMoveScript : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;

    private Vector3 firstPressPos;
    private Vector3 secondPressPos;
    private Vector3 myDirection;
    private Vector3 targetPos;

    public float dragSpeed;
    private float distance;
    public float distanceMultiplier;

    public bool clicked;
    private Vector3 objectFirstPos;
    public bool levelFinished;

    // Start is called before the first frame update
    void Start()
    {
        clicked = false;
        rb = GetComponent<Rigidbody>();
        levelFinished = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (levelFinished == false)
        {
            rb.isKinematic = false;
            rb.velocity = Vector3.forward * speed * Time.deltaTime;
        }
        else
        {
            rb.isKinematic = true;
        }
    }

    void Update()
    {
        Swipe();
        if (transform.position.x < -0.95f)
        {
            transform.position = new Vector3(-0.93f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > 0.95f)
        {
            transform.position = new Vector3(0.93f, transform.position.y, transform.position.z);
        }
    }

    void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clicked = true;
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            objectFirstPos = transform.position;
        }
        else if (Input.GetMouseButton(0) && clicked)
        {

            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            myDirection = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            distance = myDirection.magnitude;

            distance /= distanceMultiplier;

            myDirection.Normalize();

            myDirection *= distance;

            targetPos = new Vector3(objectFirstPos.x + myDirection.x, transform.position.y, transform.position.z);

            if (distance > 0.05 && clicked)
            {
                rb.MovePosition(Vector3.Lerp(transform.position, targetPos, dragSpeed * Time.deltaTime));
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {
            clicked = false;
        }
    }
}
