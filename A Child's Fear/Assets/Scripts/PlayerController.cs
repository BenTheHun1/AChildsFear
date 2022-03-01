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
    public CameraControllerFPS cc;

    public Transform groundCheck;
    private float groundDistance = 0.2f;
    public LayerMask groundMask;
    public LayerMask deathMask;

    private float speed = 4f;
    private float gravity = 0.3f * (-9.81f * 6);
    private float jumpHeight;
    private float defaultHeight = 1.85f;

    public Vector3 velocity;
    public bool isOnGround;
    public bool isOnDeath;
    public RaycastHit ray;

    public float desiredHeight;

    public GameObject buyableItem;
    private TextMeshProUGUI hudText;
    private TextMeshProUGUI keyCount;
    private int keys;

    private bool m_isAxisInUse = false;

    bool jumpPrep = false;

    void Start()
    {
        hudText = GameObject.Find("HUD Text").gameObject.GetComponent<TextMeshProUGUI>();
        keyCount = GameObject.Find("Key Count").gameObject.GetComponent<TextMeshProUGUI>();


        UpdateUI();
    }

    void Update()
    {
        isOnGround = Physics.CheckSphere(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - desiredHeight / 2, gameObject.transform.position.z), groundDistance, groundMask); //Checks if you are on a Ground layer object
        isOnDeath = Physics.CheckSphere(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - desiredHeight / 2, gameObject.transform.position.z), groundDistance, deathMask);
        if (isOnGround && velocity.y < 0)
        {
            velocity.y = -2f; //Stops y velocity from infinitely decreasing
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        /*if (Input.GetButtonDown("Jump") && isOnGround)
        {
            jumpHeight = 2f;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }*/
        if (Input.GetButton("Jump") && isOnGround && jumpHeight < 3f)
        {
            jumpPrep = true;
            jumpHeight += 1f * Time.deltaTime;
            desiredHeight -= .3f * Time.deltaTime; 
        }
        if (Input.GetButtonUp("Jump") && isOnGround)
        {
            if (jumpHeight < 1f)
            {
                jumpHeight = 1f;
            }
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpHeight = 0f;
            jumpPrep = false;
            desiredHeight = defaultHeight;
        }

        controller.Move(velocity * Time.deltaTime);

        //Crouching System
        if (!jumpPrep)
        {
            if (Input.GetAxis("Crouch") > 0)
            {
                desiredHeight = 1f;
            }
            else
            {
                desiredHeight = defaultHeight;
            }
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
        else
        {
            buyableItem = null;
        }
        if (buyableItem != null)
        {
            if (buyableItem.name == "Key")
            {
                hudText.text = "[E] Take " + buyableItem.name;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    keys++;
                    UpdateUI();
                    Destroy(buyableItem);
                }
            }
            else if (buyableItem.name == "Door")
            {
                if (keys >= 1)
                {
                    hudText.text = "[E] Open " + buyableItem.name;
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        keys--;
                        UpdateUI();
                        Destroy(buyableItem);
                    }
                }
                else
                {
                    hudText.text = "Locked";
                }
            }
            
        }
        else
        {
            hudText.text = "";
        }

        
    }
    void UpdateUI()
    {
        keyCount.text = keys + " Keys";
    }
}


