using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<GameObject> fingers;
    [SerializeField] private Text tutorialText;
    private bool pressedButton = false;
    private int section = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (section)
        {
            case 0:
                tutorialText.text = "Drag sword as high as you can";
                break;
            case 1:
                tutorialText.text = "Pop lanterns to get bonuses";
                break;
            case 2:
                tutorialText.text = "Game ends when stamina\nis out";
                break;
            case 3:
                tutorialText.text = "Whatch the rewarded video to\ncontinue the game";
                break;
            case 4:
                tutorialText.text = "Have fun!";
                break;
        }
        for(int i = 0; i < fingers.Capacity; i++)
        {
            if(i == section)
            {
                fingers[i].SetActive(true);
            }
            else
            {
                fingers[i].SetActive(false);
            }
        }
        if (Application.isMobilePlatform)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    section++;
                }
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                if (!pressedButton)
                {
                    pressedButton = true;
                    section++;
                }
            }
            if (!Input.GetMouseButton(0))
            {
                pressedButton = false;
            }
        }
        if(section > 4)
        {
            PlayerPrefs.SetInt("Tutorial", 1);
            PlayerPrefs.SetInt("Started", 1);
            SceneManager.LoadScene(0);
        }
    }
}
