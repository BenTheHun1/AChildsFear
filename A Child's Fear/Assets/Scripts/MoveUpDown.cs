using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    public axis inputAxis;
    private float originalHeight; //Cannot be 0
    public float inputNextValueOnAxis; //Cannot be 0
    private float newPos;
    private float nextHeight;
    public float speed;
    private float sensitivity = 0.1f;
    

    public enum axis {x, y, z}

    // Start is called before the first frame update
    void Start()
    {
        if (inputAxis == axis.x)
        {
            originalHeight = gameObject.transform.position.x;
        }
        else if (inputAxis == axis.y)
        {
            originalHeight = gameObject.transform.position.y;
        }
        else
        {
            originalHeight = gameObject.transform.position.z;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inputAxis == axis.x)
        {
            Debug.Log(gameObject.transform.position.x / inputNextValueOnAxis);
            if (gameObject.transform.position.x / inputNextValueOnAxis <= 1 + sensitivity && gameObject.transform.position.x / inputNextValueOnAxis >= 1 - sensitivity)
            {
                nextHeight = originalHeight;
            }
            else if (gameObject.transform.position.x / originalHeight <= 1 + sensitivity && gameObject.transform.position.x / originalHeight >= 1 - sensitivity)
            {
                nextHeight = inputNextValueOnAxis;
            }
            newPos = Mathf.MoveTowards(gameObject.transform.position.x, nextHeight, speed * Time.deltaTime);
            gameObject.transform.position = new Vector3(newPos, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        else if (inputAxis == axis.y)
        {
            if (gameObject.transform.position.y / inputNextValueOnAxis <= 1 + sensitivity && gameObject.transform.position.y / inputNextValueOnAxis >= 1 - sensitivity)
            {
                nextHeight = originalHeight;
            }
            else if (gameObject.transform.position.y / originalHeight <= 1 + sensitivity && gameObject.transform.position.y / originalHeight >= 1 - sensitivity)
            {
                nextHeight = inputNextValueOnAxis;
            }
            newPos = Mathf.MoveTowards(gameObject.transform.position.y, nextHeight, speed * Time.deltaTime);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, newPos, gameObject.transform.position.z);
        }
        else
        {
            if (gameObject.transform.position.z / inputNextValueOnAxis <= 1 + sensitivity && gameObject.transform.position.z / inputNextValueOnAxis >= 1 - sensitivity)
            {
                nextHeight = originalHeight;
            }
            else if (gameObject.transform.position.z / originalHeight <= 1 + sensitivity && gameObject.transform.position.z / originalHeight >= 1 - sensitivity)
            {
                nextHeight = inputNextValueOnAxis;
            }
            newPos = Mathf.MoveTowards(gameObject.transform.position.z, nextHeight, speed * Time.deltaTime);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, newPos);
        }


        //Debug.Log(gameObject.transform.position);
    }
}
