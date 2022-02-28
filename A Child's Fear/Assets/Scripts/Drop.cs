using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public float waitTime;
    public float fallSpeed;
    public float despawnTime;

    public bool dropping;
    private float newPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dropping)
        {
            if (waitTime <= 0f)
            {
                newPos = Mathf.MoveTowards(gameObject.transform.position.y, 0f, fallSpeed * Time.deltaTime);
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
