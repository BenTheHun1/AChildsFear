using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController3RD : MonoBehaviour
{
    // Start is called before the first frame update

    public float mouseSensitivity;
    public Transform player;
    private float xRotation = 0f;
    private RaycastHit hit;
    private Vector3 offset;

    public bool inControl;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        offset = player.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = gameObject.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            print(hit.transform.gameObject.name);
            player.gameObject.GetComponent<PlayerController>().ray = hit;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        player.Rotate(0, mouseX, 0);

        float desiredAngle = player.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = player.position - (rotation * offset);

        float desiredX = player.eulerAngles.x;
        Quaternion rotation2 = Quaternion.Euler(desiredAngle, 0, 0);
        transform.position = player.position - (rotation2 * offset);


        transform.LookAt(player);

        


    }
}
