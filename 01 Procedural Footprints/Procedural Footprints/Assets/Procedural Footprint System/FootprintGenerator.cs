using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventVector3 : UnityEvent<Vector3> { };

public class FootprintGenerator : MonoBehaviour
{
    [Tooltip("Enable/disable footprint generation.")]
    public bool CanGenerateFootprints = true;

    [Tooltip("The object to instantiate as a left footprint (if useRightPrefab is false, this is the only prefab that will be used).")]
    [SerializeField] private GameObject footprintLeftPrefab = default;
    [Tooltip("The object to instantiate as a right footprint (if useRightPrefab is false, this doesn't matter).")]
    [SerializeField] private GameObject footprintRightPrefab = default;

    [Tooltip("Whether to switch prefabs every other footprint (use with an asymmetric footprintPrefab).")]
    [SerializeField] private bool useRightPrefab = true;

    [Tooltip("Min distance from last footprint before a new one is generated.")]
    [SerializeField] private float distanceBetweenFootsteps = 1;

    [Tooltip("Y-axis position (height) that footprints are placed at.")]
    [SerializeField] private float footprintWorldHeight = 0.001f;

    [Tooltip("Number of frames between footprint generation calculations (for performance).")]
    [SerializeField] private int checkIntervalFrames = 1;

    [Tooltip("Maximum number of footprints before old ones are erased.")]
    [SerializeField] private int maxNumFootprints = 128;

    [Tooltip("You might use this for footstep sounds.")]
    public UnityEventVector3 OnFootstepAtLocation;

    private readonly List<GameObject> placedFootprints = new List<GameObject>();
    private Vector3 lastFootprintPosition;
    private bool wasLastFootstepRight = false;


    /// <summary>
    /// Permanently deletes all footprints that have been placed and empties internal list. Use for cleanup.
    /// </summary>
    /// 
    [ContextMenu("DestroyAllFootprints")]
    public void DestroyAllFootprints()
    {
        foreach (GameObject footprint in placedFootprints)
        {
            Destroy(footprint);
        }
        placedFootprints.Clear();
    }

    /// <summary>
    ///  Calls SetActive() on all footprints that have been placed (but their GameObjects still exist).
    /// 
    /// Use this to easily hide or show the footprints
    /// </summary>
    /// <param name="active"></param>
    public void SetActiveAllFootprints(bool active)
    {
        foreach (GameObject footprint in placedFootprints)
        {
            footprint.SetActive(active);
        }
    }

    private void Update()
    {
        if (!CanGenerateFootprints ||
            Time.frameCount % checkIntervalFrames != 0) return;  // Only check footprints every nth frame

        Vector3 positionDelta = CalculatePositionDelta();

        if (ShouldGenerateFootprint(positionDelta))
        {
            bool toUseRight = wasLastFootstepRight && useRightPrefab;
            if (useRightPrefab) wasLastFootstepRight = !wasLastFootstepRight;
            Vector3 desiredForward = new Vector3(positionDelta.x, 0, positionDelta.z);
            GenerateFootprintAtMyPosition(toUseRight, desiredForward);
        }
    }

    /// <summary>
    /// Compare distance from last footprint to decide whether a new footprint should be placed. 
    /// Only looks at distance between the (X,Z) coordinate projections.
    /// </summary>
    /// <returns>Whether a new footprint is warrented.</returns>
    private bool ShouldGenerateFootprint(Vector3 positionDelta)
    {
        Vector2 XZProjection = new Vector2(positionDelta.x, positionDelta.z);
        return XZProjection.magnitude >= distanceBetweenFootsteps;
    }

    /// <summary>
    /// Computes the difference between the last placed footprint and my current position.
    /// Note: the difference is from my current world position to last footprint's world
    /// position, so the y-component of the returned vector may not be useful.
    /// </summary>
    /// <returns>Difference as a vector.</returns>
    private Vector3 CalculatePositionDelta()
    {
        if (lastFootprintPosition == null)
        {
            lastFootprintPosition = Vector3.zero;
        }
        return transform.position - lastFootprintPosition;
    }

    /// <summary>
    /// Instantiates a new footprint with my current position and rotation (but on the ground).
    /// </summary>
    /// <param name="useRightPrefab">Whether to use the right prefab instead of the left.</param>
    /// <param name="desiredForward">The forward direction to orient the new footprint.</param>
    private void GenerateFootprintAtMyPosition(bool useRightPrefab, Vector3 desiredForward)
    {
        Vector3 targetPosition = new Vector3(transform.position.x, footprintWorldHeight, transform.position.z);
        Quaternion targetRotation = Quaternion.LookRotation(desiredForward, Vector3.up); // align footprints with world up
        GameObject prefabToInstantiate = useRightPrefab ? footprintRightPrefab : footprintLeftPrefab;
        GameObject instantiatedFootprint = Instantiate(prefabToInstantiate, targetPosition, targetRotation);
        placedFootprints.Add(instantiatedFootprint);
        lastFootprintPosition = targetPosition;
    }
}
