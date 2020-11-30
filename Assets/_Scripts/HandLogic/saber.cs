﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saber : MonoBehaviour
{
    public int layer;
    private Vector3 previousPos;
    private float rotation;
    private int toleration = 40;
    public float maxAngle = 95;
    public Rigidbody rb;
    public OVRInput.Controller OwningController;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.Log("moan");
        }
    }

    // Update is called once per frame
    void Update()
    {
       //rotation = Vector3.Angle(transform.position - previousPos, other.transform.up);
        previousPos = transform.position;
    }

    //When the Primitive collides with the walls, it will reverse direction
    private void OnTriggerEnter(Collider other)
    //void OnCollisionEnter(Collision collision)
    {
        rotation = Vector3.Angle(transform.position - previousPos, other.transform.up);


        if (other.transform.gameObject.tag == "beat")
        {
            //(rotation - toleration) <= 180 && 180 <= (rotation + toleration)
            beat beatObject = other.transform.gameObject.GetComponent<beat>();
            bool validRot = rotation + other.transform.rotation.z >= (180 - toleration);
            
            
            if (validRot && layer == other.transform.gameObject.layer)//if our hit is at the required angle +- toleration
            {
                Debug.Log("Play good note here");
                if (beatObject.isStroop)//will be replace by manager.isStroop
                {
                    FeedbackSystem.S.negativeFeedback();
                }
                else
                {
                    if (layer == 9)
                    {
                        FeedbackSystem.S.positiveFeedback(FeedbackSystem.SaberSide.Left);
                    }
                    else
                    {
                        FeedbackSystem.S.positiveFeedback(FeedbackSystem.SaberSide.Right);
                    }
                }
               
                DataTracker.on_slice(!beatObject.isStroop, true, beatObject.time_since_creation());

            }
            else
            {
                if (beatObject.isStroop)//will be replace by manager.isStroop
                {
                    if (layer == 9)
                    {
                        FeedbackSystem.S.positiveFeedback(FeedbackSystem.SaberSide.Left);
                    }
                    else
                    {
                        FeedbackSystem.S.positiveFeedback(FeedbackSystem.SaberSide.Right);
                    }
                }
                else
                {
                    FeedbackSystem.S.negativeFeedback();
                }
                
				DataTracker.on_slice(!beatObject.isStroop, false, beatObject.time_since_creation());
				//do something with points/play sound?
				Debug.Log("Play crappy note here");
            }
            Destroy(other.gameObject);

        }
        else if (other.transform.gameObject.tag == "bomb") {
            //do something with points/play sound?
            Destroy(other.gameObject);
        }
        
    }
     
}
