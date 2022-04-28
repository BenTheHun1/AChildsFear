using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    [Tooltip("Time in seconds to wait to start dropping the platform after the player lands on it.")]
    public float waitTime;
    [Tooltip("Speed that platforms falls.")]
    public float fallSpeed;
    [Tooltip("Time in seconds to wait after the platform starts dropping to despawn it.")]
    public float despawnTime;

    [Tooltip("Is the platform currently dropping? Should be false on scene start.")]
    public bool isCurrentlyDropping;
    private float newPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isCurrentlyDropping)
        {
            if (waitTime <= 0f)
            {
                newPos = Mathf.MoveTowards(gameObject.transform.position.y, -100f, fallSpeed * Time.deltaTime);
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, newPos, gameObject.transform.position.z);
                despawnTime -= Time.deltaTime;
                if (despawnTime <= 0f)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
