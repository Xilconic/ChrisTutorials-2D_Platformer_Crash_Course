using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <remarks>
/// Tutorial mentions that this script is coming from another video: 
/// <see href="https://www.youtube.com/watch?v=tMXgLBwtsvI&t=0s"/>
/// </remarks>
public class ParallaxEffect : MonoBehaviour
{
    public Camera Camera;
    public Transform FollowTarget;

    /// <summary>
    /// Starting position for the parallax game object
    /// </summary>
    Vector2 _startingPosition;

    /// <summary>
    /// Starting Z-value of the parallax game object
    /// </summary>
    float _startingZ;

    /// <summary>
    /// 
    /// </summary>
    float zDistanceFromTarget => transform.position.z - FollowTarget.transform.position.z;

    /// <summary>
    /// If object is in front of target, use <see cref="Camera.nearClipPlane"/>. 
    /// Otherwise use <see cref="Camera.nearClipPlane"/>.
    /// </summary>
    float clippingPlane => (Camera.transform.position.z + (zDistanceFromTarget > 0 ? Camera.farClipPlane : Camera.nearClipPlane));

    /// <summary>
    /// Distance that the camera has moved from the starting position of the parallax object
    /// </summary>
    Vector2 CameraMoveSinceStart => (Vector2) Camera.transform.position - _startingPosition;

    float parallaxFactor => Mathf.Abs(zDistanceFromTarget / clippingPlane);

    // Start is called before the first frame update
    void Start()
    {
        _startingPosition = transform.position;
        _startingZ = transform.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        // When the target moves, move the parallax object the same distance times a multiplier:
        Vector2 newPosition = _startingPosition + CameraMoveSinceStart * parallaxFactor;

        // The X/Y position changes based on target travel speed times the parallax factor.
        // Z remains unchanged:
        transform.position = new Vector3(newPosition.x, newPosition.y, _startingZ);
    }
}
