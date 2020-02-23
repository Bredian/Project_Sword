using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Cloud",fileName = "New Cloud")]
public class CloudData : ScriptableObject
{
    [Tooltip("Main Cloud Sprite")]
    [SerializeField] private Sprite sprite;
    public Sprite MainSprite
    {
        get { return sprite; }
        protected set { }
    }
    [Tooltip("Minimum Cloud Speed")]
    [SerializeField] private float minSpeed;
    public float MinSpeed
    {
        get { return minSpeed; }
        protected set { }
    }
    [Tooltip("Maximum Cloud Speed")]
    [SerializeField] private float maxSpeed;
    public float MaxSpeed
    {
        get { return maxSpeed; }
        protected set { }
    }

}
