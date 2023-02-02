using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    private bool isRotating = true;
    public GameObject gameOverBox;

    void Start()
    {
        parent = GameObject.Find("Dial");
        rb = GetComponent<Rigidbody2D>();
        this.parentScript = parent.GetComponent<Player>();
        this.pointObjs = this.parentScript.pointObjs;
    }

    void Update()
    {
        if (isRotating)
        {
            this.parentScript.Rotate(isRight);
            HitCoin();
        }
        else
        {
          
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        curPointHit = other.gameObject;
        curPointHit.GetComponent<Point>().isHit = true;
        curPointHit.GetComponent<Point>().collisions += 1;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        curPointHit.GetComponent<Point>().isHit = false;

        if (curPointHit.GetComponent<Point>().isHit == false && curPointHit.GetComponent<Point>().collisions == 1)
        {
            isRotating = false;
        }
    }

    void PickRandomPoint(GameObject lastPoint)
    {
        int randomStart = Random.Range(0, pointObjs.Count);

        while (pointObjs[randomStart] == lastPoint)
        {
            randomStart = Random.Range(0, pointObjs.Count); // gets a new number
        }

        pointObjs[randomStart].SetActive(true);
    }

    void HitCoin()
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
                PickRandomPoint(curPointHit); // then pick a random point to spawn in.
                curPointHit = null;
            }
            else // If lose, load the game over scene and make the dial stop rotating
            {
                this.isRotating = false;
                SceneManager.LoadScene("SampleScene", LoadSceneMode.Single); 
            }
        }

        if (this.parentScript.playersPoints == 5)
        {
            this.isRotating = false;
            SceneManager.LoadScene("level_up", LoadSceneMode.Single);
        }
        // if both the point has 1 collision and hit equals true we make it 
    }
}
