using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stamina : MonoBehaviour
{
    // Start is called before the first frame update
    [Tooltip("How many seconds you can pull sword at the start")]
    [SerializeField] private float pullTime;
    [Tooltip("On what height in uints difficulty will increase by a factor of 2")]
    [SerializeField] private float difHight;
    private float currentTime = 0f;
    private float startingXscale;
    private float difficultyCoefficent = 1f;
    private Transform swordTransform;
    private float initialY;
    public static float pressedPause = 1f;
    public float bonusTimer = 0f;
    public float bonusCoefficent = 1f;
    private float defaultY;
    private int bonusesUsed;
    public static float bonusUsedCoef = 1f;
    void Start()
    {
        startingXscale = transform.localScale.x;
        swordTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        initialY = swordTransform.position.y;
        //defaultY = transform.position.y;
    }

    void Gameover()
    {
        if (PlayerPrefs.GetInt("Score", 0) < ((int) DragMovement.height / 1000))
        {
            PlayerPrefs.SetInt("Score", (int) DragMovement.height / 1000);
        }
        DragMovement.gameOver = true;
        currentTime = 0f;

    }
    // Update is called once per frame
    void Update()
    {
        bonusTimer -= Time.deltaTime;
        if (bonusTimer <= 0)
            bonusTimer = 0;
        if (bonusTimer <= 0 && bonusCoefficent != 1f)
        {
            bonusTimer = 0;
            bonusCoefficent = 1;
        }
        //transform.position = new Vector3(transform.position.x, defaultY + DragMovement.height, transform.position.z);
        Debug.Log(pullTime/difficultyCoefficent);
        if (!UIMenu.paused)
        {
            if (Application.isMobilePlatform)
            {
                if (Input.touchCount > 0)
                {
                    currentTime += Time.deltaTime * pressedPause * bonusCoefficent * bonusUsedCoef;
                    difficultyCoefficent = 1 + Mathf.Sqrt((swordTransform.position.y - initialY) / difHight);
                    if (currentTime <= (pullTime / difficultyCoefficent))
                    {
                        transform.localScale = new Vector3(startingXscale * (1 - (currentTime / pullTime)), transform.localScale.y, 1f);
                    }
                    if (currentTime >= (pullTime / difficultyCoefficent))
                    {
                        transform.localScale = Vector3.zero;
                        Invoke("Gameover", 0.05f);
                    }
                }
                if (Input.touchCount == 0)
                {
                    currentTime -= Time.deltaTime * pressedPause * bonusCoefficent;
                    transform.localScale = new Vector3(startingXscale * (1 - (currentTime / pullTime)), transform.localScale.y, 1f);
                    if (currentTime <= 0f)
                    {
                        currentTime = 0f;
                        transform.localScale = new Vector3(startingXscale, transform.localScale.y, 1);
                    }
                    bonusUsedCoef = 1f;
                }
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    currentTime += Time.deltaTime * pressedPause * bonusCoefficent * bonusUsedCoef;
                    difficultyCoefficent = 1 + Mathf.Sqrt((swordTransform.position.y - initialY) / difHight);
                    if (currentTime <= (pullTime / difficultyCoefficent))
                    {
                        transform.localScale = new Vector3(startingXscale * (1 - (currentTime / pullTime)), transform.localScale.y, 1f);
                    }
                    if (currentTime >= (pullTime / difficultyCoefficent))
                    {
                        transform.localScale = Vector3.zero;
                        Invoke("Gameover", 0.05f);
                    }
                }
                if (!Input.GetMouseButton(0))
                {
                    currentTime -= Time.deltaTime * pressedPause;
                    transform.localScale = new Vector3(startingXscale * (1 - (currentTime / pullTime)), transform.localScale.y, 1f);
                    if (currentTime <= 0f)
                    {
                        currentTime = 0f;
                        transform.localScale = new Vector3(startingXscale, transform.localScale.y, 1);
                    }
                    bonusUsedCoef = 1f;
                }
            }
        }
        
    }

}
