using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Omsk_Easter_Egg : MonoBehaviour
{
    [SerializeField] private SpriteRenderer omskSprite;
    [SerializeField] private float alwaysOmsk = 500f;
    [SerializeField] private AudioSource audioOmsk;
    [SerializeField] private Collider2D omskCollider;
    [SerializeField] private Rigidbody2D rb;
    private Vector3 startPos;
    private bool touched = false;
    private bool omskSpawned = false;
    // Start is called before the first frame update
    void Awake()
    {
        audioOmsk.volume = PlayerPrefs.GetFloat("Volume", 1f);
        startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(!audioOmsk.isPlaying)
            transform.localPosition = startPos;
        if (PlayerPrefs.GetInt("adPlayed", 0) == 1)
        {
            if (!omskSpawned)
            {
                float throwing = Random.Range(0f, 1f);
                if (throwing < alwaysOmsk)
                {
                    omskCollider.enabled = true;
                    omskSprite.enabled = true;
                }
                    
                omskSpawned = true;
            }
            if (omskSpawned)
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
                            if (hit.collider.CompareTag("Omsk"))
                            {
                                if (!touched)
                                {
                                    touched = true;
                                    audioOmsk.Play();
                                    Invoke("Die", 0.52f);
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
                            if (hit.collider.CompareTag("Omsk"))
                            {
                                if (!touched)
                                {
                                    touched = true;
                                    audioOmsk.Play();
                                    Invoke("Die", 0.52f);
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
            audioOmsk.Play();
            Invoke("Die", 0.52f);
            Debug.Log("Bonus active");
            rb.gravityScale = 5000f;
            Stamina.bonusUsedCoef = 0f;
        }

    }
}
