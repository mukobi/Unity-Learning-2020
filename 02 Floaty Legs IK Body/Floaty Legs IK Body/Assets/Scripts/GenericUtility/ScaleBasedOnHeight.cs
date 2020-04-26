using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBasedOnHeight : MonoBehaviour
{
    public bool initializeAutomatically = false;
    [SerializeField] private Transform topTarget = default;
    [SerializeField] private Transform bottomTarget = default;
    [SerializeField] private float magicScaleMultiplier = 0.666f;

    public void Scale()
    {
        if (initializeAutomatically == true)
        {
            GameManager gameManager = GameObject.Find("Managers").GetComponent<GameManager>();
            topTarget = gameManager.playerCamera.transform;
        }

        float heightDifference = topTarget.position.y - bottomTarget.position.y;
        float targetScale = heightDifference * magicScaleMultiplier;
        Debug.Log($"Rescaling avatar to {targetScale}");
        Debug.Log("height difference: " + (topTarget.position.y));
        transform.localScale = new Vector3(targetScale, targetScale, targetScale);
    }
}
