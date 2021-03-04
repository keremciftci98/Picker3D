using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineScript : MonoBehaviour
{
    public PickerMoveScript pms;
    public UiControllerScript uics;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "finishline")
        {
            Debug.Log("level finished");
            pms.levelFinished = true;
            uics.ShowWinPanel();
        }
    }
}
