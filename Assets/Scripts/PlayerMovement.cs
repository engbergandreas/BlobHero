using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float rotationSpeed = 5.0f;
    public float movementSpeed;
    public Rigidbody rb;
    public Camera cam;
    // Start is called before the first frame update

    private LayerMask layerMask;
    void Awake()
    {
        //cam = Camera.current;
        layerMask = LayerMask.GetMask("Ground");

    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        //Not trying to move
        if (!Input.GetMouseButton(0))
            return;

        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(camRay, out RaycastHit hitinfo, 100.0f, layerMask);

        if (!hitinfo.transform)
            return;


        Vector3 target = hitinfo.point;

        Vector3 targetDirection = (target - transform.position).normalized;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationSpeed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
        //Remove tilt rotations
        Vector3 tmp = transform.eulerAngles;
        tmp.x = 0;
        tmp.z = 0;
        transform.eulerAngles = tmp;

        rb.AddForce(movementSpeed * transform.forward);
    }
}
