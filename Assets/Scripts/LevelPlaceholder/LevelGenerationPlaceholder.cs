using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerationPlaceholder : MonoBehaviour
{
    public GameObject levelPrefab1;
    public GameObject levelPrefab2;
    public GameObject levelPrefab3;

    [Header("Spawn settings")]
    public int pieceCount = 10;
    public Vector3 spawnStart = Vector3.zero;
    public Vector3 spawnSpacing = new Vector3(10f, 0f, 0f);
    [Header("Attachment settings")]
    public float detachDelay = 60f;

    private List<ConfigurableJoint> createdJoints = new List<ConfigurableJoint>();

    void Start()
    {
        GameObject previous = null;
        for (int i = 0; i < pieceCount; i++)
        {
            GameObject levelPrefab = null;
            int r = Random.Range(0, 3);
            if (r == 0) levelPrefab = levelPrefab1;
            else if (r == 1) levelPrefab = levelPrefab2;
            else levelPrefab = levelPrefab3;

            if (levelPrefab == null)
                continue;

            Vector3 position = spawnStart + spawnSpacing * i;
            GameObject go = Instantiate(levelPrefab, position, Quaternion.identity);

            // ensure the spawned piece has a 2D Collider so it participates in 2D physics
            // remove any 3D rigidbody/Collider that could interfere
            Collider old3d = go.GetComponent<Collider>();
            if (old3d != null) Destroy(old3d);
            Rigidbody oldRb3d = go.GetComponent<Rigidbody>();
            if (oldRb3d != null) Destroy(oldRb3d);

            // check for any existing Collider2D in children or self
            Collider2D col2d = go.GetComponentInChildren<Collider2D>();
            if (col2d == null)
            {
                // try to use SpriteRenderer bounds to create a BoxCollider2D on that renderer's GameObject
                SpriteRenderer sr = go.GetComponentInChildren<SpriteRenderer>();
                if (sr != null)
                {
                    GameObject rendGO = sr.gameObject;
                    BoxCollider2D bc2d = rendGO.AddComponent<BoxCollider2D>();
                    // size in local space (convert world bounds by lossyScale)
                    Vector2 worldSize = sr.bounds.size;
                    Vector3 ls = rendGO.transform.lossyScale;
                    float sx = Mathf.Approximately(ls.x, 0f) ? 1f : ls.x;
                    float sy = Mathf.Approximately(ls.y, 0f) ? 1f : ls.y;
                    bc2d.size = new Vector2(worldSize.x / sx, worldSize.y / sy);
                    bc2d.offset = rendGO.transform.InverseTransformPoint(sr.bounds.center);
                    col2d = bc2d;
                }
                else
                {
                    // fallback: add a BoxCollider2D to the root object with default size
                    BoxCollider2D bc2d = go.AddComponent<BoxCollider2D>();
                    bc2d.size = Vector2.one;
                    col2d = bc2d;
                }
            }

            if (col2d != null)
            {
                col2d.isTrigger = false;
            }

            // Ensure level pieces do not have a Rigidbody2D (static collider) for proper static collision behavior
            Rigidbody2D rb2d = go.GetComponent<Rigidbody2D>();
            if (rb2d != null)
            {
                Destroy(rb2d);
            }
        }

    }

    
    void Update()
    {
        
    }
}
