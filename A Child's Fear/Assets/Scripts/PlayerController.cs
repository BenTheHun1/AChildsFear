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
    private float defaultHeight;

    public Vector3 velocity;
    public bool isOnGround;
    public bool isOnDeath;
    public RaycastHit ray;

    public float desiredHeight;

    public GameObject buyableItem;
    private TextMeshProUGUI hudText;

    //private bool m_isAxisInUse = false;

    bool jumpPrep = false;

    private int currentItem = 1;
    private int maxItems = 2;
    public Image flashlightIcon;
    public GameObject flashlightObject;
    public Image slingshotIcon;
    public GameObject slingshotObject;
    public Image keyIcon;
    public GameObject keyObject;

    void Start()
    {
        defaultHeight = gameObject.GetComponent<CharacterController>().height;
        hudText = GameObject.Find("HUD Text").gameObject.GetComponent<TextMeshProUGUI>();


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
            jumpHeight += 3f * Time.deltaTime;
            desiredHeight -= .5f * Time.deltaTime; 
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

        if (Input.GetKeyDown(KeyCode.R) || isOnDeath)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.mouseScrollDelta.y < 0 && Time.timeScale == 1)
        {
            if (currentItem < maxItems)
            {
                currentItem++;
            }
            else
            {
                currentItem = 1;
            }
            UpdateUI();
        }
        else if (Input.mouseScrollDelta.y > 0 && Time.timeScale == 1)
        {
            if (currentItem > 1)
            {
                currentItem--;
            }
            else
            {
                currentItem = maxItems;
            }
            UpdateUI();
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
                    keyIcon.gameObject.SetActive(true);
                    maxItems++;
                    UpdateUI();
                    Destroy(buyableItem);
                }
            }
            else if (buyableItem.name == "Door")
            {
                if (currentItem == 3)
                {
                    hudText.text = "[E] Open " + buyableItem.name;
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        keyIcon.gameObject.SetActive(false);
                        maxItems--;
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
        if (currentItem > maxItems || currentItem <= 0)
        {
            currentItem = 1;
        }
        if (currentItem == 1)
        {
            flashlightIcon.color = Color.white;
            slingshotIcon.color = new Color(0.43f, 0.43f, 0.43f);
            keyIcon.color = new Color(0.43f, 0.43f, 0.43f);
            flashlightObject.SetActive(true);
            slingshotObject.SetActive(false);
            keyObject.SetActive(false);
        }
        else if (currentItem == 2)
        {
            flashlightIcon.color = new Color(0.43f, 0.43f, 0.43f);
            slingshotIcon.color = Color.white;
            keyIcon.color = new Color(0.43f, 0.43f, 0.43f);
            flashlightObject.SetActive(false);
            slingshotObject.SetActive(true);
            keyObject.SetActive(false);
        }
        else if (currentItem == 3)
        {
            flashlightIcon.color = new Color(0.43f, 0.43f, 0.43f);
            slingshotIcon.color = new Color(0.43f, 0.43f, 0.43f);
            keyIcon.color = Color.white;
            flashlightObject.SetActive(false);
            slingshotObject.SetActive(false);
            keyObject.SetActive(true);
        }
    }
}


