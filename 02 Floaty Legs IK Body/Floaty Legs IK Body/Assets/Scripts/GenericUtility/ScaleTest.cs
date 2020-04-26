using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTest : MonoBehaviour
{
    [SerializeField] private Transform topTarget = default;
    [SerializeField] private Transform bottomTarget = default;
    [SerializeField] private float magicScaleMultiplier = 0.666f;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            Scale();
        }
    }

    public void Scale()
    {
        float heightDifference = topTarget.position.y - bottomTarget.position.y;
        float targetScale = heightDifference * magicScaleMultiplier;
        Debug.Log($"Rescaling avatar to {targetScale}");
        Debug.Log("height difference: " + (topTarget.position.y));
        transform.localScale = new Vector3(targetScale, targetScale, targetScale);
    }
}
