using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] private float minTime=10;
    [SerializeField] private float maxTime=20;
    private float currentTime = 0f;
    [SerializeField] private List<GameObject> clouds;
    // Update is called once per frame
    void Spawn()
    {
        int i = (int)Random.Range(0f, (float)clouds.Capacity);
        Instantiate(clouds[i], transform.position, transform.rotation);
    }
    private void Start()
    {
        InvokeRepeating("Spawn", 0, Random.Range(minTime, maxTime));
    }
}
