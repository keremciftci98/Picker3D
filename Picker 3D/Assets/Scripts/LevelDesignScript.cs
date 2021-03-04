using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDesignScript : MonoBehaviour
{
    public UiControllerScript uics;
    public List<GameObject> levels;
    public int currentLevel;
    public GameObject levelHolder;
    public GameObject tempHolder;
    public GameObject picker;
    public Vector3 pickerStartPos;
    public Vector3 pickerTempPos;
    public bool goToNextLevel;
    public float speed;
    public bool tempPos;
    public bool createLevel;
    public Text levelText;
    public int levelNum;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("currentlevel", 0);
        currentLevel = PlayerPrefs.GetInt("currentlevel");
        if (currentLevel == 0)
        {
            currentLevel = 1;
        }

        createLevel = false;
        tempPos = false;
        goToNextLevel = false;
        pickerStartPos = picker.transform.position;
        RestartLevel();
    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = "LEVEL " + currentLevel;
        if (goToNextLevel)
        {
            uics.CloseWinPanel();
            if (picker.transform.position != pickerTempPos && tempPos == false)
            {
                picker.transform.position = Vector3.MoveTowards(picker.transform.position, pickerTempPos, speed * Time.deltaTime);
            }
            else if (picker.transform.position == pickerTempPos)
            {
                tempPos = true;
                Debug.Log("temp pos reached");
            }
            if (tempPos && createLevel == false)
            {
                createLevel = true;

                foreach (Transform child in levelHolder.transform)
                {
                    Destroy(child.gameObject);
                }
                foreach (Transform child in tempHolder.transform)
                {
                    Destroy(child.gameObject);
                }
                if (currentLevel > 12)
                {
                    levelNum = currentLevel * 5;
                    levelNum = levelNum % 12;
                }
                else
                {
                    levelNum = currentLevel - 1;
                }
                var tempLevel = Instantiate(levels[levelNum], levelHolder.transform.position, Quaternion.identity);

                tempLevel.transform.parent = levelHolder.transform;

                picker.transform.position = pickerStartPos - new Vector3(0, 0, 10);

            }
            if (createLevel)
            {
                if (picker.transform.position != pickerStartPos)
                {
                    picker.transform.position = Vector3.MoveTowards(picker.transform.position, pickerStartPos, speed * Time.deltaTime);
                }
                else
                {
                    goToNextLevel = false;
                    tempPos = false;
                    createLevel = false;
                    uics.ShowStartPanel();
                }
            }
        }
    }
    public void RestartLevel()
    {
        foreach (Transform child in levelHolder.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in tempHolder.transform)
        {
            Destroy(child.gameObject);
        }
        if (currentLevel > 12)
        {
            levelNum = currentLevel * 5;
            levelNum = levelNum % 12;
        }
        else
        {
            levelNum = currentLevel - 1;
        }
        var tempLevel = Instantiate(levels[levelNum], levelHolder.transform.position, Quaternion.identity);

        tempLevel.transform.parent = levelHolder.transform;
        picker.transform.position = pickerStartPos;
        uics.CloseLosePanel();
        uics.ShowStartPanel();
        picker.GetComponent<PickerMoveScript>().levelFinished = true;
    }
    public void NextLevel()
    {
        Debug.Log("next level");
        currentLevel++;

        PlayerPrefs.SetInt("currentlevel", currentLevel);
        goToNextLevel = true;
        pickerTempPos = picker.transform.position + new Vector3(0, 0, 7);
        uics.CloseWinPanel();
    }
}
