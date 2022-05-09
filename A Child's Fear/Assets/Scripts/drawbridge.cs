using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawbridge : MonoBehaviour
{
    [Tooltip("This value must be at least equal to the Required Value for movement to begin.")]
    public int currentValue;
    [Tooltip("The Current Value must be at least equal to this for movement to begin.")]
    public int requiredValue;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameObject.transform.eulerAngles.x);
        if (currentValue >= requiredValue && gameObject.transform.eulerAngles.x < 58f)
        {
            gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x + speed * Time.deltaTime, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);
        }
    }
}
