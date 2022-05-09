using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttony : MonoBehaviour
{
    [Tooltip("Platform to modify the condition of. Only define a Platform OR a Bridge, not both.")]
    public MovePlatform connectedPlatform;
    [Tooltip("Bridge to modify the condition of. Only define a Platform OR a Bridge, not both.")]
    public drawbridge connectedBridge;
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
            if (connectedPlatform != null)
            {
                connectedPlatform.currentValue++;
            }
            else if (connectedBridge != null)
            {
                connectedBridge.currentValue++;
            }
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
