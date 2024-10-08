using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TurretScript : MonoBehaviour
{
    public bool isActive = true;
    public GameObject gun;
    public GameObject player;
    public GameObject muzzleFlash;
    public AudioSource gunShot;
    public float rotationSpeed;
    public float damage;
    public float turretCooldown;
    public float muzzleLifetime;

    //Internal
    private float timer;
    private bool canShoot = true;
    private void Update()
    {
        if (isActive)
        {
            RotateToPlayer();
            Shoot();
        }
    }
    private void RotateToPlayer()
    {
        //Check If Turret Can See Player
        RaycastHit hit;
        var rayDirection = player.transform.position - gun.transform.position;
        if (Physics.Raycast(gun.transform.position, rayDirection, out hit) && hit.transform == player.transform)
        {
            //Rotate Gun towards player
            var q = Quaternion.LookRotation(
                new Vector3(
                    player.transform.position.x - gun.transform.position.x,
                    gun.transform.position.y,
                    player.transform.position.z - gun.transform.position.z
                ));
            gun.transform.rotation = Quaternion.Lerp(gun.transform.rotation, q, rotationSpeed * Time.deltaTime);
        }
    }
    private void Shoot()
    {
        //Give The Turret A Shoot Cooldown
        if (!canShoot)
        {
            timer += Time.deltaTime;
            if (timer >= turretCooldown)
            {
                canShoot = true;
                timer = 0;
            }
        }

        if (canShoot)
        {
            //Check If Turret Is Aimed At Player, Then Shoot
            RaycastHit hit;
            if (Physics.Raycast(gun.transform.position, gun.transform.forward, out hit) && hit.transform == player.transform)
            {
                gunShot.Play();
                player.GetComponent<PlayerHealthManager>().TakeDamage(damage);
                canShoot = false;

                //Show MuzzleFlash
                muzzleFlash.SetActive(true);
                Invoke("DisableMuzzleFlash", muzzleLifetime);
            }
        }
    }
    private void DisableMuzzleFlash() { muzzleFlash.SetActive(false); }
}