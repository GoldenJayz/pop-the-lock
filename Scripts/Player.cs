using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float rotationSpeed;
    public List<GameObject> pointObjs = new List<GameObject>();
    private GameObject child;
    public int playersPoints = 0; // The players total points they have gotten by hitting a point.


    void Start()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Point");
        int randomStart;
        GameObject chosenPoint;
        this.child = GameObject.FindWithTag("Player");

        for (int i = 0; i < temp.Length; i++)
        {
            pointObjs.Add(temp[i]);
        }

        randomStart = Random.Range(0, pointObjs.Count);
        chosenPoint = pointObjs[randomStart];

        for (int i = 0; i < pointObjs.Count; i++) 
        {
            if (pointObjs[i] != chosenPoint) 
            {
                pointObjs[i].SetActive(false);
            }
        }
    }
    

    public void Rotate(bool hit) // bool hit = isRight
    {
        int multiplier = hit ? -1 : 1;
        transform.Rotate(new Vector3(0, 0, rotationSpeed * multiplier) * Time.deltaTime);
    }
}
