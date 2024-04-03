using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    // TODO: Given item based membership checking, HashSet<Collider2D>() may be the better abstraction?
    // This would ensure that the same thing cannot be detected multiple times.
    // Alternatively, if we only ever collide with 1 thing, then perhaps we only need a field instead?
    public List<Collider2D> DetectedColliders = new List<Collider2D>();
    Collider2D _col;

    void Awake()
    {
        _col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DetectedColliders.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        DetectedColliders.Remove(collision);
    }
}
