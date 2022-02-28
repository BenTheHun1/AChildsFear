using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlongAxis : MonoBehaviour
{
    public axis inputAxis;
    private float originalHeight; //Cannot be 0
    public float inputNextValueOnAxis; //Cannot be 0
    private float newPos;
    private float nextHeight;
    public float speed;
    
    

    public enum axis {x, y, z}

    // Start is called before the first frame update
    void Start()
    {
        if (inputAxis == axis.x)
        {
            originalHeight = gameObject.transform.localPosition.x;
        }
        else if (inputAxis == axis.y)
        {
            originalHeight = gameObject.transform.localPosition.y;
        }
        else
        {
            originalHeight = gameObject.transform.localPosition.z;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inputAxis == axis.x)
        {
            if (gameObject.transform.localPosition.x == inputNextValueOnAxis)
            {
                nextHeight = originalHeight;
            }
            else if (gameObject.transform.localPosition.x == originalHeight)
            {
                nextHeight = inputNextValueOnAxis;
            }
            newPos = Mathf.MoveTowards(gameObject.transform.localPosition.x, nextHeight, speed * Time.deltaTime);
            gameObject.transform.localPosition = new Vector3(newPos, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
        }
        else if (inputAxis == axis.y)
        {
            if (gameObject.transform.localPosition.y == inputNextValueOnAxis)
            {
                nextHeight = originalHeight;
            }
            else if (gameObject.transform.localPosition.y == originalHeight)
            {
                nextHeight = inputNextValueOnAxis;
            }
            newPos = Mathf.MoveTowards(gameObject.transform.localPosition.y, nextHeight, speed * Time.deltaTime);
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, newPos, gameObject.transform.localPosition.z);
        }
        else
        {
            if (gameObject.transform.localPosition.z == inputNextValueOnAxis)
            {
                nextHeight = originalHeight;
            }
            else if (gameObject.transform.localPosition.z == originalHeight)
            {
                nextHeight = inputNextValueOnAxis;
            }
            newPos = Mathf.MoveTowards(gameObject.transform.localPosition.z, nextHeight, speed * Time.deltaTime);
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, newPos);
        }


        //Debug.Log(gameObject.transform.position);
    }
}
