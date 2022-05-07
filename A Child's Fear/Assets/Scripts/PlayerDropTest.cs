using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDropTest : MonoBehaviour
{
    Drop platform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Landed on " + other.gameObject.name + "!");
        if (other.gameObject.TryGetComponent<Drop>(out platform))
        {
            platform.isCurrentlyDropping = true;
            Debug.Log("Dropping " + platform.gameObject.name + "!");
        }
    }
}
