using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public axis inputAxis;
    public float speed;

    public enum axis { x, y, z }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inputAxis == axis.x)
        {
            gameObject.transform.Rotate(Time.deltaTime * speed, 0, 0);
        }
        else if (inputAxis == axis.y)
        {
            gameObject.transform.Rotate(0, Time.deltaTime * speed, 0);
        }
        else
        {
            gameObject.transform.Rotate(0, 0, Time.deltaTime * speed);
        }
    }
}
