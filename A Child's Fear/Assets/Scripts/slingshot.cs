using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slingshot : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnpoint;
    private GameObject newestBullet;
    public float force;
    private Text forceDisplay;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        forceDisplay = GameObject.Find("Slingshot Force").GetComponent<Text>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && force <= 5000)
        {
            force += 2000f * Time.deltaTime;
            anim.SetBool("pull", true);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            anim.SetBool("pull", false);
            anim.SetTrigger("release");
            if (force > 300)
            {
                newestBullet = Instantiate(bullet, spawnpoint.position, spawnpoint.rotation);
                newestBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * force);
            }
            force = 0;
        }
        forceDisplay.text = force.ToString("F0");
    }
}
