using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float movementSpeed;
    public float movementTime;

    public Vector3 newPosition;
    public float newSize;
    public Vector3 newEuler;

    public float dragSpeed;
    Vector3 mousePosLastFrame;

    Camera cam;

    private void Start() {

        cam = GetComponent<Camera>();
        newPosition = transform.position;
        newSize = cam.orthographicSize;
    }

    private void Update() {
        HandleMovementInput();
        Cursor.visible = !Input.GetMouseButton(0);
    }

    void HandleMovementInput() {
        newSize -= Input.GetAxisRaw("Vertical") * movementSpeed;
        newPosition += transform.right * Input.GetAxisRaw("Horizontal") * movementSpeed;

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newSize, Time.deltaTime * movementTime);
        //transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);

        Vector3 pos = cam.ScreenToViewportPoint(Input.mousePosition - mousePosLastFrame);
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        int mouseDir = (Vector3.Cross((screenCenter - Input.mousePosition).normalized, pos).z > 0) ? -1 : 1;

        if (Input.GetMouseButton(0)) {
            transform.parent.Rotate(new Vector3(0, pos.magnitude * dragSpeed * mouseDir, 0));
        }

        mousePosLastFrame = Input.mousePosition;
    }
}
