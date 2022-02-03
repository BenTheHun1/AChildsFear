using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //Variables
    public CharacterController controller;
    public CameraController cc;

    public Transform groundCheck;
    private float groundDistance = 0.2f;
    public LayerMask groundMask;
    public LayerMask deathMask;

    private float speed = 4f;
    private float gravity = 0.3f * (-9.81f * 6);
    private float jumpHeight = 2f;

    public Vector3 velocity;
    public bool isOnGround;
    public bool isOnDeath;
    public RaycastHit ray;

    float desiredHeight;

    public GameObject buyableItem;
    public TextMeshProUGUI hudText;
    private int keys;

    private bool m_isAxisInUse = false;



    void Start()
    {
    }

    void Update()
    {
        isOnGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //Checks if you are on a Ground layer object
        isOnDeath = Physics.CheckSphere(groundCheck.position, groundDistance, deathMask);
        if (isOnGround && velocity.y < 0)
        {
            velocity.y = -2f; //Stops y velocity from infinitely decreasing
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        controller.Move(velocity * Time.deltaTime);

        //Crouching System
        if (Input.GetAxis("Crouch") > 0)
        {
            desiredHeight = 1f;
        }
        else
        {
            desiredHeight = 2f;
        }
        controller.height = Mathf.Lerp(controller.height, desiredHeight, 0.1f);

        //Make sword swing only occur once
        if (Input.GetAxisRaw("Fire1") != 0)
        {
            if (m_isAxisInUse == false)
            {
                m_isAxisInUse = true;
            }
        }
        if (Input.GetAxisRaw("Fire1") == 0)
        {
            m_isAxisInUse = false;
        }

        if (Input.GetKeyDown(KeyCode.R) || isOnDeath)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //Detect and show info on item
        if (ray.transform != null)
        {
            if (ray.transform.gameObject.CompareTag("Item") && Vector3.Distance(ray.transform.position, gameObject.transform.position) < 4f)
            {
                buyableItem = ray.transform.gameObject;

            }
            else
            {
                buyableItem = null;
            }
        }
        if (buyableItem != null)
        {
            if (buyableItem.name == "Key")
            {
                hudText.text = "[E] Take " + buyableItem.name;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    keys++;
                }
            }

            //Use item
            
        }
        else
        {
            hudText.text = "";
        }

        
    }
    void UpdateUI()
    {
        keys++;
    }
}


