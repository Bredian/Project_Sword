using UnityEngine;
using UnityEngine.UI;

public class DragMovement : MonoBehaviour
{
    private Vector3 newTouchPosition;
    private Vector3 oldTouchPosition;
    private bool pressedButton = false;
    [Tooltip("Rigidbody of the sword")]
    [SerializeField] private Rigidbody2D rb;
    private Vector3 direction;
    [Tooltip("How responsive our drag controls are")]
    [Range(0.1f, 10000f)]
    [SerializeField] private float moveSpeed;
    public AudioSource audioSourcePull;
    public AudioSource audioSourceBreak;
    public static float breakCoefficent = 1f;
    [SerializeField] private Text heightText;
    public static float height = 0f;
    public static bool gameOver = false;
    public float bonusSpeed = 0;
    private const float initialY = -582f;
    public float bonusTimer = 0;

    // Update is called once per frame
    private void Start()
    {

        if (PlayerPrefs.GetInt("adPlayed",0) == 1)
        {
            height = PlayerPrefs.GetInt("LastScore", 0);
            transform.position = new Vector3(0f, PlayerPrefs.GetFloat("LastPlace",0f), 0f);
            
            heightText.text = ((int) (height/1000)).ToString();
        }
        if (PlayerPrefs.GetInt("adPlayed", 0) == 0)
        {
            height = 0f;
            PlayerPrefs.SetInt("LastScore", 0);
            PlayerPrefs.SetFloat("LastPlace", initialY);
        }
        if (PlayerPrefs.GetInt("HelpUsed", 0) == 1)
        {
            PlayerPrefs.SetInt("HelpUsed", 0);
            height = PlayerPrefs.GetFloat("ScoreBeforeHelp", 0f);
            Debug.Log(height);
            transform.position = new Vector3(transform.position.x, PlayerPrefs.GetFloat("PositionBeforeHelp", initialY), transform.position.z);
            heightText.text = ((int)(height / 1000)).ToString();
        }
        breakCoefficent = 1f;
    }
    void PlayPull()
    {
        audioSourcePull.Play();
    }
    void Gameover()
    {
        if(PlayerPrefs.GetInt("Score",0) < ((int) height/1000))
        {
            PlayerPrefs.SetInt("Score", (int) height / 1000);
        }
        gameOver = true;
        direction = new Vector3 (0f,0f,0f);
    }
    //Decets how far finger moved from previous position
    void Update()
    {
        bonusTimer -= Time.deltaTime;
        if (bonusTimer <= 0)
            bonusTimer = 0;
        if(bonusTimer > 0)
            audioSourcePull.pitch = 1.12f;
        if (bonusTimer <= 0 && bonusSpeed > 0)
        {
            bonusTimer = 0;
            bonusSpeed = 0;
            audioSourcePull.pitch = 1f;
        }

        if (!UIMenu.paused)
        {
            if (Application.isMobilePlatform)
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began && direction.y != 10000f)
                    {
                        oldTouchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    }
                    if (direction.y != 10000f)
                    {
                        newTouchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                        newTouchPosition.z = 0f;
                        oldTouchPosition.z = 0f;
                        direction = (newTouchPosition - oldTouchPosition);
                    }
                    if (direction.y == 10000f)
                    {
                        oldTouchPosition = Vector3.zero;
                        newTouchPosition = oldTouchPosition;
                        direction.y = 0;
                    }
                    if(direction.y != 1000f && Stamina.bonusUsedCoef == 0f)
                    {
                        direction.y = 0;
                    }
                    if (direction.y < 0)
                    {
                        /* if (!audioSourceBreak.isPlaying)
                             audioSourceBreak.Play();
                         Invoke("Gameover", 0.5f);
                         breakCoefficent = 0f;*/
                        direction.y = 0;
                    }

                    height += direction.y * breakCoefficent;
                    heightText.text = ((int)(height / 1000)).ToString();
                    PlayerPrefs.SetInt("LastScore", (int)height);
                    PlayerPrefs.SetFloat("LastPlace", transform.position.y);
                    if(bonusSpeed == 0)
                        rb.velocity = new Vector2(direction.x, direction.y) * breakCoefficent * moveSpeed * Time.deltaTime;

                    if (bonusSpeed > 0)
                        rb.velocity = new Vector2(direction.x, direction.y) * breakCoefficent * bonusSpeed * Time.deltaTime;



                    if (direction.y > 0 && !audioSourcePull.isPlaying && breakCoefficent == 1f)
                    {
                        Invoke("PlayPull", 0f);
                    }

                    if (touch.phase == TouchPhase.Ended)
                    {
                        rb.velocity = Vector3.zero;
                    }
                    oldTouchPosition = newTouchPosition;
                }
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    if (!pressedButton)
                    {
                        pressedButton = true;
                        oldTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    }
                    newTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    newTouchPosition.z = 0f;
                    oldTouchPosition.z = 0f;
                    direction = (newTouchPosition - oldTouchPosition);
                    if (direction.y >0 && Stamina.bonusUsedCoef == 0f)
                    {
                        direction.y = 0;
                    }
                    if (direction.y < 0)
                    {
                       /* if (!audioSourceBreak.isPlaying)
                            audioSourceBreak.Play();
                        Invoke("Gameover", 0.5f);
                        breakCoefficent = 0f;*/
                        direction.y = 0;
                    }
                    height += direction.y * breakCoefficent;
                    heightText.text = ((int) (height / 1000)).ToString();
                    PlayerPrefs.SetInt("LastScore", (int) height);
                    PlayerPrefs.SetFloat("LastPlace", transform.position.y);
                    if(bonusSpeed == 0)
                        rb.velocity = new Vector2(direction.x, direction.y) * breakCoefficent * moveSpeed * Time.deltaTime;
                    if (bonusSpeed > 0)
                        rb.velocity = new Vector2(direction.x, direction.y) * breakCoefficent * bonusSpeed * Time.deltaTime;
                    

                    if (direction.y > 0 && !audioSourcePull.isPlaying && breakCoefficent == 1f)
                    {
                        Invoke("PlayPull", 0f);
                    }


                    oldTouchPosition = newTouchPosition;
                }
                if (!Input.GetMouseButton(0))
                {
                    pressedButton = false;
                    rb.velocity = Vector3.zero;
                }
                if (Input.GetMouseButton(1))
                {
                    gameOver = true;
                }
            }
        }
        if (UIMenu.paused)
        {
            direction = new Vector3(0f, 10000f, 0f);
        }
    }
}
