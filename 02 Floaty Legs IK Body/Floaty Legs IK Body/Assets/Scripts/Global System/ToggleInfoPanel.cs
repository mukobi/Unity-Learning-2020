/* =============================================================================
 * Purpose: toggle off and on the info panel (Canvas component) with "i" key.
 * 
 * Author: Gabriel Mukobi
 * ============================================================================= */

using UnityEngine;

public class ToggleInfoPanel : MonoBehaviour
{
    private Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle config window with I key
        if (Input.GetKeyDown("i"))
        {
            canvas.enabled = !canvas.enabled;
        }
    }
}
