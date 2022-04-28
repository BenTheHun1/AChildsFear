using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletVelocity : MonoBehaviour
{
    [Tooltip("Time in seconds for bullet to despawn.")]
    public float time;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            Destroy(gameObject);
        }
    }
}
