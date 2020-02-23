using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owl_Easter_Egg : MonoBehaviour
{
    [SerializeField] private SpriteRenderer owlSprite;
    [SerializeField] private int alwaysOwl = 500;
    [SerializeField] private AudioSource audioOwl;
    [SerializeField] private Collider2D owlCollider;
    [SerializeField] private Rigidbody2D rb;
    private bool touched = false;
    private bool owlSpawned = false;
    // Start is called before the first frame update
    void Awake()
    {
        audioOwl.volume = PlayerPrefs.GetFloat("Volume", 1f);  
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Started", 0) == 1)
        {
            if (!owlSpawned)
            {
                float throwing = Random.Range(0f, 1f);
                if (throwing < (float)((float)PlayerPrefs.GetInt("Score", 0) / (float)alwaysOwl))
                {
                    owlCollider.enabled = true;
                    owlSprite.enabled = true;
                }
                    
                owlSpawned = true;
            }
            if (owlSpawned)
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
                            if (hit.collider.CompareTag("Owl"))
                            {
                                if (!touched)
                                {
                                    touched = true;
                                    audioOwl.Play();
                                    Invoke("Die", 0.73f);
                                    Debug.Log("Bonus active");
                                    rb.gravityScale = 5000f;
                                    Stamina.bonusUsedCoef = 0f;
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
                            if (hit.collider.CompareTag("Owl"))
                            {
                                if (!touched)
                                {
                                    touched = true;
                                    audioOwl.Play();
                                    Invoke("Die", 0.73f);
                                    Debug.Log("Bonus active");
                                    rb.gravityScale = 5000f;
                                    Stamina.bonusUsedCoef = 0f;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    private void Die()
    {
        Destroy(this.gameObject);
    }
    private void OnMouseDown()
    {
        if (!UIMenu.paused)
        {
            audioOwl.Play();
            Invoke("Die", 0.73f);
            Debug.Log("Bonus active");
            rb.gravityScale = 5000f;
            Stamina.bonusUsedCoef = 0f;
        }

    }
}
