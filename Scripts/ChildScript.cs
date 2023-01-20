using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChildScript : MonoBehaviour
{
    private bool isRight = true; //r
    private GameObject parent;
    private Rigidbody2D rb;
    public GameObject curPointHit;
    public bool playerHit = false;
    private List<GameObject> pointObjs;
    private Player parentScript;
    public Text pointText;

    void Start()
    {
        parent = GameObject.Find("Dial");
        rb = GetComponent<Rigidbody2D>();
        this.parentScript = parent.GetComponent<Player>();
        print(this.parentScript.playersPoints);
        this.pointObjs = this.parentScript.pointObjs;
    }

    void Update()
    {
        this.parentScript.Rotate(isRight);
        Movement();
    }


    void OnTriggerEnter2D(Collider2D other)
    { 
        curPointHit = other.gameObject;
        curPointHit.GetComponent<Point>().isHit = true;
    }


    void OnTriggerExit2D(Collider2D other)
    {
        curPointHit.GetComponent<Point>().isHit = false;
    }

    void PickRandomPoint(GameObject lastPoint) 
    {
        // Exclude the last point
        int randomStart = Random.Range(0, pointObjs.Count);
        // print(randomStart);

        while (pointObjs[randomStart] == lastPoint)
        {
            randomStart = Random.Range(0, pointObjs.Count); // gets a new number
        }
        
        pointObjs[randomStart].SetActive(true);
        
    }

    void Movement()
    {
        if (Input.GetKeyDown("space"))
        {
            bool isPointHit = curPointHit == null ? false : curPointHit.GetComponent<Point>().isHit;

            if (isPointHit == true) // if both the player is pressing space and the point is being collided with then the player hit the point
            {
                int pointCollisions = curPointHit.GetComponent<Point>().collisions;
                playerHit = true;
                pointCollisions += 1; // collisions should only be incremented if the player hits space and they are colliding with each other
                isRight = pointCollisions == 1 ? !isRight : isRight;
                curPointHit.SetActive(false);
                this.parentScript.playersPoints += 1;
                pointText.text = this.parentScript.playersPoints.ToString();
                // Destroy(curPointHit);
                PickRandomPoint(curPointHit); // then pick a random point to spawn in.
                curPointHit = null;
            }

            else
            {
                playerHit = false;
            }
        }
    }
}
