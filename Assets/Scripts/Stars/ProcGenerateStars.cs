using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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
    public Transform fieldCenter;
    private Vector3 centerBefore;
    private List<GameObject> stars = new List<GameObject>();
    void Start()
    {
        fieldCenter = transform;
        centerBefore = fieldCenter.position;
        for (int i = 0; i < starCount; i++)
        {
            GameObject starPrefab = GetRandomStarPrefab();
            // position relative to the field center
            Vector3 randomPosition = fieldCenter.position + new Vector3(Random.Range(left, right), Random.Range(bottom, top), -1f);
            GameObject star = Instantiate(starPrefab, randomPosition, Quaternion.identity);
            stars.Add(star);
        }
    }

    void Update()
    {
        Vector3 cameraMovement = fieldCenter.position - centerBefore;

        // compute world-space bounds each frame (don't permanently modify the original offsets)
        float leftWorld = fieldCenter.position.x + left;
        float rightWorld = fieldCenter.position.x + right;
        float bottomWorld = fieldCenter.position.y + bottom;
        float topWorld = fieldCenter.position.y + top;

        foreach (GameObject star in stars)
        {
            // apply camera movement
            star.transform.position += cameraMovement;

            // move star upward in world space
            star.transform.position += Vector3.up * speed * Time.deltaTime;

            // wrap when above top
            if (star.transform.position.y > topWorld)
            {
                float randomX = Random.Range(leftWorld, rightWorld);
                star.transform.position = new Vector3(randomX, bottomWorld, -1f);
            }
        }

        centerBefore = fieldCenter.position;
    }
    GameObject GetRandomStarPrefab()
    {
        int randomIndex = Random.Range(0, 4);
        switch (randomIndex)
        {
            case 0:
                return starPrefab1;
            case 1:
                return starPrefab2;
            case 2:
                return starPrefab3;
            case 3:
                return starPrefab4;
            default:
                return starPrefab1;
        }
    }
}