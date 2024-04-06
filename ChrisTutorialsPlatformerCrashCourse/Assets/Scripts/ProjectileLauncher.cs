using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    // TODO: Possibly limit the number of projectiles on screen?
    [Tooltip("The project prefab to be instantiated when launching the projectile.")]
    public GameObject ProjectilePrefab;
    
    [Tooltip("The location from which the projectile is launched")]
    public Transform LaunchPoint;

    private void Awake()
    {
        Debug.Assert(ProjectilePrefab != null, "'ProjectilePrefab' must be set!");
        Debug.Assert(LaunchPoint != null, "'LaunchPoint' must be set!");
    }

    public void FireProjectile()
    {
        GameObject projectile = Instantiate(ProjectilePrefab, LaunchPoint.position, ProjectilePrefab.transform.rotation);
        Vector3 originalLocalScale = projectile.transform.localScale;
        projectile.transform.localScale = new Vector3(
            originalLocalScale.x * transform.localScale.x > 0 ? 1 : -1, // Inherit local scale x-axis direction from ProjectileLauncher
            originalLocalScale.y,
            originalLocalScale.z
        );
    }
}
