﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foodSpawn : MonoBehaviour
{
    private GameObject floor;
    float minX;
    float maxX;
    float minZ;
    float maxZ;

    public float growTime = 5;
    
    float timeToGrow;

    bool readyToEat = false;
    bool firstFood = true;

    Collider col;

    // Start is called before the first frame update
    void Start()
    {

        timeToGrow = 0;

        floor = GameObject.FindGameObjectWithTag("Floor");
        var scale = floor.transform.localScale;
        minX = 0.9f*(-5 * scale[0]);
        maxX = 0.9f*(5 * scale[0]);
        minZ = 0.9f*(-5 * scale[2]);
        maxZ = 0.9f*(5 * scale[2]);
        col = GetComponent<Collider>();

        randomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(1f,1f,1f)*(1f-0.8f*timeToGrow/growTime);
        transform.position = new Vector3(transform.position[0], transform.localScale[0]/2f, transform.position[2]);
        if(timeToGrow>=0){
            timeToGrow -= Time.deltaTime;
        }else{
            readyToEat = true;
            col.isTrigger = false;
        }

    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Chicken" && readyToEat)
        {
            randomPosition();
        }
    }

    void randomPosition()
    {
        readyToEat = false;
        col.isTrigger = true;
        if(!firstFood){
            timeToGrow = growTime;
        }else{
            firstFood = false;
        }
        transform.localScale = new Vector3(1f,1f,1f)*(1f-0.8f*timeToGrow/growTime);
        transform.position = new Vector3(Random.Range(minX, maxX), transform.localScale[0]/2f, Random.Range(minZ, maxZ));   
    }

    public bool isReadyToEat(){
        return readyToEat;
    }
}
