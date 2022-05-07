using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttony : MonoBehaviour
{
    [Tooltip("Platform to modify the condition of.")]
    public MovePlatform connectedPlatform;
    [Tooltip("If true, button has been pressed and no longer functions.")]
    public bool beenPressed;
    [Tooltip("The type of Button.\nHand must be pushed with [E].\nBullet must be fired at.\nEither is anything.")]
    public ButtonType buttonType;
    public enum ButtonType
    {
        Hand, Bullet, Either
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Push()
    {
        if (!beenPressed)
        {
            connectedPlatform.currentValue++;
            beenPressed = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Push();
        }
    }
}
