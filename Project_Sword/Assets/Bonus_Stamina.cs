using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus_Stamina : MonoBehaviour
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
    private bool touched = false;
    public static int usedBonus = 0;
    private void Start()
    {
        // sword = GameObject.FindGameObjectWithTag("Player");
        rb.AddForce(Vector2.right * Random.Range(minSpeed, maxSpeed) * Time.deltaTime, ForceMode2D.Impulse);
        audioBonus.volume = PlayerPrefs.GetFloat("Volume", 1f);
    }
    private void Die()
    {
        Destroy(this.gameObject);
    }
    private void OnMouseDown()
    {
        if (!UIMenu.paused)
        {
            if (!touched)
            {
                touched = true;
                audioBonus.Play();
                Invoke("Die", 0.45f);
                usedBonus++;
                Debug.Log("Bonus active");
                
                rb.gravityScale = 5000;
                Stamina.bonusTimer = 3f;
            }
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
                    if (hit.collider.CompareTag("Banana"))
                    {
                        if (!touched)
                        {
                            touched = true;
                            audioBonus.Play();
                            Invoke("Die", 0.45f);
                            Debug.Log("Touched it");
                            Debug.Log("Bonus active");
                            usedBonus++;
                            
                            rb.gravityScale = 5000;
                            Stamina.bonusTimer = 3f;
                        }

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
                    if (hit.collider.CompareTag("Banana"))
                    {
                        if (!touched)
                        {
                            touched = true;
                            audioBonus.Play();
                            Invoke("Die", 0.45f);
                            Debug.Log("Touched it");
                            Debug.Log("Bonus active");
                            usedBonus++;
                            
                            rb.gravityScale = 5000;
                            Stamina.bonusTimer = 3f;
                        }
                    }
                }
            }
        }
    }
}