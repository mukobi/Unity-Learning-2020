/* =============================================================================
 * Purpose: Naive forward kinematic (FK) solver for a VR player character.
 * Makes root follow hipTarget, rotates hips along with hipTarget on y-axis,
 * and rotates arm (shoulder) and leg (upper leg) joints to point towards
 * their corresponding targets.
 * 
 * Author: Gabriel Mukobi
 * ============================================================================= */

using UnityEngine;

public class VRFKOnlyHands : MonoBehaviour
{
    public Transform root;
    public Transform hips;
    //public Transform armJointL;
    //public Transform armJointR;
    public Transform legJointL;
    public Transform legJointR;

    public Transform hipTarget;
    //public Transform armTargetL;
    //public Transform armTargetR;
    public Transform legTargetL;
    public Transform legTargetR;

    public Vector3 headOffset;

    public Vector3 legRotOffsetL;
    public Vector3 legRotOffsetR;

    Transform origRoot;

    private void FixedUpdate()
    {
        FollowRootToHead();
        origRoot = root;
    }

    void FollowRootToHead()
    {
        root.position = hipTarget.position - headOffset;
        // only copy y-axis yaw
        hips.rotation = Quaternion.Euler(0, hipTarget.rotation.eulerAngles.y, 0);

        RotateJointToTarget(legJointL, legTargetL, legRotOffsetL);
        RotateJointToTarget(legJointR, legTargetR, legRotOffsetR);
    }

    void RotateJointToTarget(Transform joint, Transform target, Vector3 offset)
    {
        // point at the joint
        joint.LookAt(target);
        // use offset to tune rotation
        joint.rotation *= Quaternion.Euler(offset);
    }

    private void OnDisable()
    {
        root = origRoot;
    }
}
