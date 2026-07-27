using UnityEngine;

public class fpsCounter : MonoBehaviour
{
    private float displayValue = 0f;
    private int frameCount = 0;
    private float timeElapsed = 0f;
    void Start()
    {
        
    }

    void Update()
    {
        frameCount++;
    }
    void FixedUpdate()
    {
        timeElapsed += Time.fixedDeltaTime;
        if (timeElapsed >= 1f)
        {
            displayValue = frameCount;
            frameCount = 0;
            timeElapsed = 0f;
        }
    }
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "FPS: " + displayValue.ToString("F0"));
    }
}
