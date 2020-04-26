/* =============================================================================
 * Purpose: Follow a given transform position (offset given by worldOffset) and
 * rotation (offset given by original transform.rotation on start).
 * 
 * Author: Gabriel Mukobi
 * ============================================================================= */

using UnityEngine;

public class FollowObjectWorldOffset : MonoBehaviour
{
    public Transform target;
    [HideInInspector]
    public Vector3 worldOffset;

    Quaternion originalRotation;

    private void Start()
    {
        originalRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        transform.position = target.position + worldOffset;
        transform.rotation = target.rotation * originalRotation;
    }
}
