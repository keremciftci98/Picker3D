using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiControllerScript : MonoBehaviour
{
    public PickerMoveScript pms;
    public CheckPointScript cps;
    public GameObject winPanel;
    public GameObject startPanel;
    public GameObject losePanel;
    public bool levelCleared;
    public bool startPanelBool;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = pms.speed;
        levelCleared = false;
        ShowStartPanel();
        startPanelBool = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (startPanelBool && Input.GetMouseButtonDown(0))
        {
            Debug.Log("close start panel");
            CloseStartPanel();
        }
    }
    public void ShowWinPanel()
    {
        levelCleared = true;
        winPanel.SetActive(true);
    }
    public void CloseWinPanel()
    {
        winPanel.SetActive(false);
    }
    public void ShowStartPanel()
    {
        startPanelBool = true;
        startPanel.SetActive(true);
        pms.levelFinished = true;
        cps.goalReached = true;
    }
    public void CloseStartPanel()
    {
        startPanelBool = false;
        startPanel.SetActive(false);
        pms.levelFinished = false;
        pms.speed = speed;
    }
    public void ShowLosePanel()
    {
        losePanel.SetActive(true);
    }
    public void CloseLosePanel()
    {
        losePanel.SetActive(false);
    }
}
