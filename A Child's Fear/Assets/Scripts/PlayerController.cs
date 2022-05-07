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
    public Animator anim;

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
    private Text forceDisplay;


    private int curLevel = 1;
    public Transform startLvl2;
    public Transform startLvl3;
    public Transform startLvl4;
    void Start()
    {
        defaultHeight = gameObject.GetComponent<CharacterController>().height;
        hudText = GameObject.Find("HUD Text").gameObject.GetComponent<TextMeshProUGUI>();
        forceDisplay = GameObject.Find("Slingshot Force").GetComponent<Text>();
        forceDisplay.gameObject.SetActive(false);

        UpdateUI();
    }

    void Update()
    {
        
        if (gameObject.transform.position.y <-30)
        {
            if (curLevel == 2)
            {
                gameObject.transform.position = startLvl2.position;
            }
            else if (curLevel == 3)
            {
                gameObject.transform.position = startLvl3.position;
            }
            else if (curLevel == 4)
            {
                gameObject.transform.position = startLvl4.position;
            }
        }


        isOnGround = Physics.CheckSphere(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - desiredHeight / 2, gameObject.transform.position.z), groundDistance, groundMask); //Checks if you are on a Ground layer object
        isOnDeath = Physics.CheckSphere(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - desiredHeight / 2, gameObject.transform.position.z), groundDistance, deathMask);
        if (isOnGround && velocity.y < 0)
        {
            if (anim.GetBool("jump") == true)
            {
                anim.SetBool("jump", false);
                anim.SetTrigger("land");
            }
            velocity.y = -2f; //Stops y velocity from infinitely decreasing
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        anim.SetFloat("speed", x + z);

        /*if (Input.GetButtonDown("Jump") && isOnGround)
        {
            jumpHeight = 2f;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }*/
        if (Input.GetButton("Jump") && isOnGround && jumpHeight < 3f)
        {
            anim.SetBool("crouching", true);
            jumpPrep = true;
            jumpHeight += 3f * Time.deltaTime;
            desiredHeight -= .5f * Time.deltaTime; 
        }
        if (Input.GetButtonUp("Jump") && isOnGround)
        {
            anim.SetBool("crouching", false);
            anim.SetBool("jump", true);
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

        if (!jumpPrep)
        {
            desiredHeight = defaultHeight;
        }

        //Crouching System
        /*if (!jumpPrep)
        {
            if (Input.GetAxis("Crouch") > 0)
            {
                desiredHeight = 1f;
                anim.SetBool("crouching", true);
            }
            else
            {
                desiredHeight = defaultHeight;
                anim.SetBool("crouching", false);
            }
        }*/
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
            //Debug.Log(ray.transform.gameObject.name + " " + Vector3.Distance(ray.point, gameObject.transform.position));
            if (ray.transform.gameObject.CompareTag("Item") && Vector3.Distance(ray.point, gameObject.transform.position) < 4f)
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
            
            if (buyableItem.name == "Key" || buyableItem.name == "GoldKey")
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
            else if (buyableItem.name == "door")
            {
                if (currentItem == 3)
                {
                    hudText.text = "[E] Open " + buyableItem.name;
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        keyIcon.gameObject.SetActive(false);
                        maxItems--;
                        UpdateUI();
                        curLevel++;
                        if (curLevel == 2)
                        {
                            gameObject.transform.position = startLvl2.position;
                        }
                        else if (curLevel == 3)
                        {
                            gameObject.transform.position = startLvl3.position;
                        }
                        else if (curLevel == 4)
                        {
                            gameObject.transform.position = startLvl4.position;
                        }
                    }
                }
                else
                {
                    hudText.text = "Locked";
                }
            }
            else if (buyableItem.name == "udoor")
            {
                hudText.text = "[E] Open " + buyableItem.name;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    curLevel++;
                    if (curLevel == 2)
                    {
                        gameObject.transform.position = startLvl2.position;
                    }
                    else if (curLevel == 3)
                    {
                        gameObject.transform.position = startLvl3.position;
                    }
                    else if (curLevel == 4)
                    {
                        gameObject.transform.position = startLvl4.position;
                    }
                }
            }
            else if (buyableItem.name == "button2")
            {
                if (buyableItem.GetComponent<Buttony>().buttonType == Buttony.ButtonType.Hand)
                {
                    hudText.text = "[E] Push Button";
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        buyableItem.GetComponent<Buttony>().Push();
                    }
                }
            }
            else if (buyableItem.name == "teddybear")
            {
                SceneManager.LoadScene("The End");
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
            forceDisplay.gameObject.SetActive(false);
        }
        else if (currentItem == 2)
        {
            flashlightIcon.color = new Color(0.43f, 0.43f, 0.43f);
            slingshotIcon.color = Color.white;
            keyIcon.color = new Color(0.43f, 0.43f, 0.43f);
            flashlightObject.SetActive(false);
            slingshotObject.SetActive(true);
            keyObject.SetActive(false);
            forceDisplay.gameObject.SetActive(true);
        }
        else if (currentItem == 3)
        {
            flashlightIcon.color = new Color(0.43f, 0.43f, 0.43f);
            slingshotIcon.color = new Color(0.43f, 0.43f, 0.43f);
            keyIcon.color = Color.white;
            flashlightObject.SetActive(false);
            slingshotObject.SetActive(false);
            keyObject.SetActive(true);
            forceDisplay.gameObject.SetActive(false);
        }
    }
}


