using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [Tooltip("Delete this Platform, add MovePlatform.cs to the new platform (which is a child of PlatformKit and in the Ground Layer).\nMake sure it has a collider *and* a RIGIDBODY.\nChange the below settings as needed.")]
    public bool tutorial;

    [Tooltip("Speed platform Moves. Should be at least 1.")]
    public float speed;
    [Tooltip("The Parent GameObject of all the transforms. Should have at least 2 children.")]
    public GameObject positionsParent; //Use at least 2 to move it
    [Tooltip("The index of the positionsParent representing the current destination. Should start at 0.")]
    public int currentPosition;


    // Start is called before the first frame update
    void Start()
    {
       for (int i = 0; i < positionsParent.transform.childCount; i++)
        {
            positionsParent.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 nextPoint = Vector3.MoveTowards(gameObject.transform.position, positionsParent.transform.GetChild(currentPosition).position, (speed * 3) * Time.deltaTime);
        gameObject.GetComponent<Rigidbody>().MovePosition(nextPoint);

        if (gameObject.transform.position == positionsParent.transform.GetChild(currentPosition).position)
        {
            currentPosition++;
            if (currentPosition >= positionsParent.transform.childCount)
            {
                currentPosition = 0;
            }
        }

        /*Vector3 nextPoint = Vector3.MoveTowards(gameObject.transform.position, positions[currentPosition], speed * Time.deltaTime);
        gameObject.GetComponent<Rigidbody>().MovePosition(nextPoint);
        if (Mathf.Approximately(gameObject.transform.position.x, positions[currentPosition].x) && Mathf.Approximately(gameObject.transform.position.y, positions[currentPosition].y) && Mathf.Approximately(gameObject.transform.position.z, positions[currentPosition].z))
        {
            currentPosition++;
            if (currentPosition >= positions.Length)
            {
                currentPosition = 0;
            }
            Debug.Log(currentPosition);
        }*/
        //Debug.Log(gameObject.transform.position);
    }
}
