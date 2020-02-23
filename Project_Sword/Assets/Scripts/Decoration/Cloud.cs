using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    // Start is called before the first frame update
    public CloudData cloudData;
    [Tooltip("Rigidbody of cloud")]
    [SerializeField]private Rigidbody2D rb;
    private SpriteRenderer renderer;
    [Tooltip("How long clowds can live before they can die")]
    [SerializeField]private float invisnsibilityTime = 30f;
    private bool visible;
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = cloudData.MainSprite;
        rb.AddForce(Vector2.right * Random.Range(cloudData.MinSpeed, cloudData.MaxSpeed) * Time.deltaTime, ForceMode2D.Impulse);
    }
    private void Update()
    {
        invisnsibilityTime -= Time.deltaTime;
        if (invisnsibilityTime <=0 && !visible)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnBecameInvisible()
    {
        visible = false;
    }
    private void OnBecameVisible()
    {
        visible = true;
    }
}
