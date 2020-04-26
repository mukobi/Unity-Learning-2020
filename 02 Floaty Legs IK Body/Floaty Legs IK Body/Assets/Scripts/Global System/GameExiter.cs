/* =============================================================================
 * Purpose: close the application on "Esc" keypress.
 * 
 * Author: Gabriel Mukobi
 * ============================================================================= */

using UnityEngine;

public class GameExiter : MonoBehaviour
{
    public static GameExiter instance;

    void Awake()
    {
        // enforce singleton GameObject
        if (!instance)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
