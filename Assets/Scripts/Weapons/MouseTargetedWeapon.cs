using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTargetedWeapon : MonoBehaviour
{
    public GameObject projectilePrefab; // Assign the prefab that has Projectile and EnemyDamager scripts
    public Transform firePoint; // The point where projectiles spawn

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detect left mouse button click
        {
            ShootTowardMouse();
        }
    }

    void ShootTowardMouse()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure it's on the correct plane

        // Calculate direction and rotation
        Vector3 direction = (mousePosition - firePoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Instantiate the projectile
        GameObject newProjectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.Euler(0, 0, angle));
        newProjectile.SetActive(true);

        // Ensure EnemyDamager is properly configured
        EnemyDamager damager = newProjectile.GetComponent<EnemyDamager>();
        if (damager != null)
        {
            damager.lifeTime = 5f; // Example lifetime; adjust as needed
            damager.damageAmount = 10f; // Example damage; adjust as needed
            damager.destroyOnContact = true; // Ensure it destroys on contact
        }

        // Ensure the projectile moves
        Projectile projectile = newProjectile.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.moveSpeed = 1f; // Example speed; adjust as needed
        }
    }
}

