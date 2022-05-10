using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    [Tooltip("Time in seconds to wait to start dropping the platform after the player lands on it.")]
    public float waitTime;
    private float waitTimeDef;
    [Tooltip("Speed that platforms falls.")]
    public float fallSpeed;
    [Tooltip("Time in seconds to wait after the platform starts dropping to despawn it.")]
    public float despawnTime;
    private float despawnTimeDef;
    [Tooltip("Time in seconds to wait after the platform has despawned to respawn it.")]
    public float respawnTime;

    [Tooltip("Is the platform currently dropping? Should be false on scene start.")]
    public bool isCurrentlyDropping;
    private float newPos;
    private Vector3 startPos;
    [Tooltip("Input this Dropping platforms pair, if desired. Don't define the partner of the other Dropping platform.")]
    public Drop partner;
    public float rand;
    // Start is called before the first frame update
    void Start()
    {
        if (partner != null)
        {
            //Debug.Log(Random.Range(0, 2));
            rand = Random.Range(0, 2);
            if (rand < 0.5f)
            {
                partner.enabled = false;
            }
            else
            {
                this.enabled = false;
            }
        }

        startPos = gameObject.transform.position;
        waitTimeDef = waitTime;
        despawnTimeDef = despawnTime;

    }

    // Update is called once per frame
    void Update()
    {
        if (isCurrentlyDropping)
        {
            if (waitTime <= 0f)
            {
                newPos = Mathf.MoveTowards(gameObject.transform.position.y, -100f, fallSpeed * Time.deltaTime);
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, newPos, gameObject.transform.position.z);
                despawnTime -= Time.deltaTime;
                if (despawnTime <= 0f)
                {
                    gameObject.GetComponent<Collider>().enabled = false;
                    //gameObject.GetComponent<MeshRenderer>().enabled = false;
                    isCurrentlyDropping = false;
                    StartCoroutine("Respawn");
                }
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        gameObject.GetComponent<Collider>().enabled = true;
        //gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.transform.position = startPos;
        waitTime = waitTimeDef;
        despawnTime = despawnTimeDef;
    }
}
