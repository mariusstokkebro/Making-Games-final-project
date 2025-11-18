using NUnit.Framework.Constraints;
using UnityEngine;

public class RotatingWeaponProjectile : WeaponProjectile
{
    private float rotationAngle = 180;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CountDown(Time.deltaTime);
        
        transform.Rotate(Vector3.up, rotationAngle * (Time.deltaTime / lifetime));
    }
}
