using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slingshot : MonoBehaviour
{
    public GameObject bullet;
    private GameObject newestBullet;
    public float force;
    private Text forceDisplay;

    // Start is called before the first frame update
    void Start()
    {
        forceDisplay = GameObject.Find("Slingshot Force").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && force <= 5000)
        {
            force += 2000f * Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (force > 300)
            {
                newestBullet = Instantiate(bullet, transform.TransformPoint(Vector3.forward * 2), transform.rotation);
                newestBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * force);
            }
            force = 0;
        }
        forceDisplay.text = force.ToString("F0");
    }
}
