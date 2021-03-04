using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    public PickerMoveScript pms;
    public float speed;
    public bool goalReached;
    public Transform holder;
    public Transform tempHolder;
    public float power;

    private void Start()
    {
        speed = pms.speed;
        goalReached = true;
    }

    private void Update()
    {
        if (goalReached)
        {
            pms.speed = speed;           
        }
        else
        {
            if (holder.childCount > 0)
            {
                foreach (Transform child in holder)
                {
                    child.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * power);
                    child.parent = tempHolder;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "checkpoint")
        {
            if (other.gameObject.GetComponent<CheckPointControlScript>().entered == false)
            {
                other.gameObject.GetComponent<CheckPointControlScript>().entered = true;
                Debug.Log("checkpoint");
                goalReached = false;
                pms.speed = 0;
                
            }
        }
        if (other.gameObject.tag == "collectable")
        {
            other.gameObject.transform.parent = holder;
        }
    }
}
