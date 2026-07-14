using System.Collections.Generic;
using UnityEngine;

public class ProcGenerateStars : MonoBehaviour
{
    public GameObject starPrefab1;
    public GameObject starPrefab2;
    public GameObject starPrefab3;
    public GameObject starPrefab4;

    [Header("Star field settings")]
    public int starCount = 250; 
    public float speed = 10f; 
    public float left = -10f;
    public float right = 10f;
    public float bottom = -10f;
    public float top = 10f;

    private List<GameObject> stars = new List<GameObject>();

    void Start()
    {
        
        for (int i = 0; i < starCount; i++)
        {
            Vector3 position = new Vector3(Random.Range(left, right), Random.Range(bottom, top), Random.Range(-1f, 1f));
            GameObject prefab = ChooseRandomPrefab();
            if (prefab != null)
            {
                GameObject go = Instantiate(prefab, position, Quaternion.identity, transform);
                stars.Add(go);
            }
        }
    }

    void Update()
    {
        float dy = speed * Time.deltaTime;
        for (int i = 0; i < stars.Count; i++)
        {
            GameObject s = stars[i];
            if (s == null) continue;
            s.transform.position += Vector3.up * dy;
            if (s.transform.position.y > top)
            {
                
                Destroy(s);
                Vector3 pos = new Vector3(Random.Range(left, right), bottom, Random.Range(-1f, 1f));
                GameObject prefab = ChooseRandomPrefab();
                GameObject newStar = null;
                if (prefab != null)
                {
                    newStar = Instantiate(prefab, pos, Quaternion.identity, transform);
                }
                stars[i] = newStar; 
            }
        }
    }

    private GameObject ChooseRandomPrefab()
    {
        int prefabIndex = Random.Range(0, 4);
        switch (prefabIndex)
        {
            case 0: return starPrefab1;
            case 1: return starPrefab2;
            case 2: return starPrefab3;
            case 3: return starPrefab4;
            default: return null;
        }
    }
}
