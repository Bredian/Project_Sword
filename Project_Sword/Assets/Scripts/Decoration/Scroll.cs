using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private SpriteRenderer renderer;
    private Vector3 startPos;
    void Start()
    {
        startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float newPos = Mathf.Repeat(DragMovement.height- (float) Bonus_Points.usedBonus * 3000f,renderer.bounds.size.y/0.75f);
        transform.localPosition = startPos + Vector3.down * newPos; 
    }
}
