using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knife : MonoBehaviour
{
    [Tooltip("Speed of knife. Shoould be greater than 0.")]
    public float speed;
    [Tooltip("True when swinging back to original position.")]
    public bool reversing;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!reversing)
        {
            gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z + speed * Time.deltaTime);
            if (gameObject.transform.eulerAngles.z >= 110)
            {
                reversing = true;
            }
        }
        else if (reversing)
        {
            gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z - speed * Time.deltaTime);
            //Debug.Log(gameObject.transform.eulerAngles.z);
            if (gameObject.transform.eulerAngles.z <= 360 && gameObject.transform.eulerAngles.z > 190)
            {
                reversing = false;
            }
        }
      
    }
}
