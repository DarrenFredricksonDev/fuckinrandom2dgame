using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private float timeElapsed = 0f;
    public float refreshTimer = 0f;
    [SerializeField] private float lateGameStartTime = 60f;
    [SerializeField] private float refreshThreshold = 30f;
    public static bool isLateGame = false;
    

    void Start()
    {
       
    }

    void FixedUpdate()
    {
        timeElapsed += Time.fixedDeltaTime;
        if (timeElapsed >= lateGameStartTime)
        {
            isLateGame = true;
        }
        if (refreshTimer >= refreshThreshold)
        {
           Refresh();
            if (Refresh() == false)
            {
                Debug.Log("Items not spawned.");
            }
        }
    }
    bool Refresh()
    {
        // spawn items
        return false;
    }
}
