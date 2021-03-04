using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectAreaScript : MonoBehaviour
{
    public TextMeshPro myText;
    public int current;
    public int goal;
    private CheckPointScript cps;
    private UiControllerScript uics;
    public Transform platform;
    public float moveSpeed;
    private Vector3 targetPosPlatform;
    public bool checker;
    public bool newChecker;
    public float timer;
    public bool startCountdown;
    // Start is called before the first frame update
    void Start()
    {
        timer = 3;
        cps = GameObject.FindGameObjectWithTag("picker").GetComponent<CheckPointScript>();
        uics = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<UiControllerScript>();
        targetPosPlatform = platform.position + new Vector3(0, 0.57f, 0);
        checker = true;
        startCountdown = false;
        newChecker = true;
    }

    // Update is called once per frame
    void Update()
    {
        myText.text = current + "/" + goal;
        if (startCountdown && newChecker)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                Debug.Log("timer 0");
                startCountdown = false;
                uics.ShowLosePanel();
                checker = false;
            }
        }
        if (current >= goal && checker)
        {
            newChecker = false;
            startCountdown = false;
            MovePlatform();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "collectable")
        {
            startCountdown = true;
            current++;
        }
    }
    void MovePlatform()
    {
        platform.position = Vector3.MoveTowards(platform.position, targetPosPlatform, moveSpeed * Time.deltaTime / 10);
        if (platform.position == targetPosPlatform)
        {
            cps.goalReached = true;
            checker = false;
        }
    }
}
