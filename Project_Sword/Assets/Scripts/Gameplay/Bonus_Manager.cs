using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus_Manager : MonoBehaviour
{
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;
    private float currentTime = 0f;
    [SerializeField] private List<GameObject> bonuses;
    // Update is called once per frame
    void Spawn()
    {
        int i = (int)Random.Range(0f, (float)bonuses.Capacity);
        Instantiate(bonuses[i], transform.position, transform.rotation);
    }
    private void Start()
    {
        InvokeRepeating("Spawn",0.5f * (Random.Range(minTime, maxTime) + Random.Range(minTime, maxTime)), Random.Range(minTime, maxTime));
    }
}
