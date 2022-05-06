using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [Tooltip("Unpack Prefab completely (Right click->Prefab).\nDelete this Platform, add MovePlatform.cs to the new platform (which should be a child of PlatformKit and in the Ground Layer).\nMake sure it has a collider *and* a RIGIDBODY, and is in the GROUND layer.\nChange the below settings as needed, including draggin the 'Positions' object to the Positions Parent variable.")]
    public bool tutorial;

    [Tooltip("The type of Movement. Always Move starts right away, Move On Condition only starts to move once Current Value is at least Required Value")]
    public MovementOptions movementType;
    [Tooltip("If Movement Type is 'Move On Condition,' this value must be at least equal to the Required Value for movement to begin.")]
    public int currentValue;
    [Tooltip("If Movement Type is 'Move On Condition,' the Current Value must be at least equal to this for movement to begin.")]
    public int requiredValue;

    public enum MovementOptions
    {
        AlwaysMove, MoveOnCondition
    }

    [Tooltip("Speed platform Moves. Should be at least 1.")]
    public float speed;
    [Tooltip("The Parent GameObject of all the transforms.\n2+ moves between all of them, only 1 moves from starting position to there and then stops.")]
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
        if (movementType == MovementOptions.AlwaysMove || (movementType == MovementOptions.MoveOnCondition && currentValue >= requiredValue))
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
}
