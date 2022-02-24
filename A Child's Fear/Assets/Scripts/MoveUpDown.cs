using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
   public int[] MaxMin;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3 (gameObject.transform.position.x, Mathf.Lerp(MaxMin[0], MaxMin[1], 0.1f), gameObject.transform.position.z);
    }
}
