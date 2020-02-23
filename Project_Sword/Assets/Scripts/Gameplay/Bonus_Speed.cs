﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus_Speed : MonoBehaviour
{
    // Start is called before the first frame update
    public float defaultMoveSpeed;
    private GameObject sword;
    private bool pressed_button = false;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float highCoef;
    [SerializeField] private AudioSource audioBonus;
    private bool readyToDie = false;
    public static int usedBonus = 0;
   //[SerializeField] private AudioSource bonus;
   private void Die()
    {
        Destroy(this.gameObject);
    }
    private void Start()
    {
        sword = GameObject.FindGameObjectWithTag("Player");
        rb.AddForce(Vector2.right * Random.Range(minSpeed, maxSpeed) * Time.deltaTime * (1 - 2 / Mathf.PI * Mathf.Atan(DragMovement.height/highCoef)), ForceMode2D.Impulse);
        audioBonus.volume = PlayerPrefs.GetFloat("Volume", 1f);
    }
    private void OnMouseDown()
    {
        if (!UIMenu.paused)
        {
            audioBonus.Play();
            Invoke("Die", 0.45f);
            Debug.Log("Bonus active");
            usedBonus++;
            sword.GetComponent<DragMovement>().bonusSpeed = 100000;
            sword.GetComponent<DragMovement>().bonusTimer = 3f;
            rb.gravityScale = 5000;
            Stamina.bonusUsedCoef = 0f;
        }
        
    }
    private void Update()
    {
        if (!UIMenu.paused)
        {
            if (Application.isMobilePlatform)
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero, 0);
                    if (hit == false)
                    {
                        return;
                    }
                        
                    if (hit.collider.CompareTag("Bonus"))
                    {
                        audioBonus.Play();
                        Invoke("Die", 0.45f);
                        Debug.Log("Touched it");
                        Debug.Log("Bonus active");
                        usedBonus++;
                        sword.GetComponent<DragMovement>().bonusSpeed = 100000;
                        sword.GetComponent<DragMovement>().bonusTimer = 3f;
                        rb.gravityScale = 5000;
                        Stamina.bonusUsedCoef = 0f;
                    }
                }
            }
            if (Application.isEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
            {
                if (Input.GetMouseButton(0))
                {
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.mousePosition)), Vector2.zero, 0);
                    if (hit == false)
                    {
                        return;
                    }
                        
                    if (hit.collider.CompareTag("Bonus"))
                    {
                        audioBonus.Play();
                        Invoke("Die", 0.45f);
                        Debug.Log("Touched it");
                        Debug.Log("Bonus active");
                        usedBonus++;
                        sword.GetComponent<DragMovement>().bonusSpeed = 100000;
                        sword.GetComponent<DragMovement>().bonusTimer = 3f;
                        rb.gravityScale = 5000;
                        Stamina.bonusUsedCoef = 0f;
                    }
                }
            }
        }
        if (readyToDie)
            Destroy(this.gameObject);
    }
}