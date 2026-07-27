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
        centerBefore = transform.position;
        for (int i = 0; i < starCount; i++)
        {
            GameObject starPrefab = GetRandomStarPrefab();
            Vector3 randomPosition = new Vector3(Random.Range(left, right), Random.Range(bottom, top), -1f);
            GameObject star = Instantiate(starPrefab, randomPosition, Quaternion.identity);
            stars.Add(star);
        }
    }
    void Update()
    {
        Vector3 cameraMovement = fieldCenter.position - centerBefore;
        top += cameraMovement.y;
        bottom += cameraMovement.y;
        left += cameraMovement.x;
        right += cameraMovement.x;
        foreach (GameObject star in stars)
        {
            star.transform.position += cameraMovement;

            star.transform.Translate(Vector3.up * speed * Time.deltaTime);

            if (star.transform.position.y > top)
            {
                float randomX = Random.Range(left, right);
                star.transform.position = new Vector3(randomX, bottom, -1f);
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