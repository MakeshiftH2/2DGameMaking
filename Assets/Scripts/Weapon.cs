using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName ="Weapon")]
public class Weapon : ScriptableObject
{
    public Sprite currentWeaponSpr;

    public GameObject bulletPrefab;
    public float fireRate = 1;
    public int damage = 20;

    float projectileSpeed = 30f;

    float projectileFiringPeriod = 0.1f;

    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;

    public void Shoot(float direction)
    {
            GameObject laser = Instantiate(
                    bulletPrefab,
                    GameObject.Find("FirePoint").transform.position,
                    Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed, 0);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(direction * projectileSpeed, 0);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
        
        
    }
}
