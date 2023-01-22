using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_sc : MonoBehaviour
{
    private float rorateSpeed = 20.0f;

    [SerializeField]
    private GameObject explosionPrefab;

    private SpawnManager_sc spawnManager_sc;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager_sc = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager_sc>();
        if (spawnManager_sc == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * rorateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            spawnManager_sc.StartSpawning();
            Destroy(this.gameObject, 0.25f);
        }
    }
}
