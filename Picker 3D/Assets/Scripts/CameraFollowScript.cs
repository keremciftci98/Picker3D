using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public Transform picker;
    public Transform myCam;
    public Vector3 followDistance;
    public Vector3 myPos;

    // Update is called once per frame
    void Update()
    {
        myPos = new Vector3(myCam.position.x, myCam.position.y, picker.position.z);
        myCam.transform.position = myPos + followDistance;
    }
}
