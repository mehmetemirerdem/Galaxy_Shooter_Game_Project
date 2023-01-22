using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_sc : MonoBehaviour
{
    [SerializeField]
    private float speed = 4.0f;

    private Player_sc player_sc;

    private Animator anim;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        player_sc = GameObject.Find("Player").GetComponent<Player_sc>();
        if (player_sc == null)
        {
            Debug.LogError("The Player is NULL");
        }
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("The Animator is NULL");
        }
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);

        if (transform.position.y < -5.4f) {
            float randomX = Random.Range(-14f, 14f);
            transform.position = new Vector3(randomX, 7.4f, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            
            //TODO Damage the player
            Player_sc player = other.transform.GetComponent<Player_sc>();
            if (player != null) {
                player.Damage();
            }
            anim.SetTrigger("OnEnemyDeath");
            speed = 0;
            audioSource.Play();
            Destroy(this.gameObject, 2.3f);
        }
        else if (other.tag == "Laser") {
            Destroy(other.gameObject);

            if (player_sc != null)
            {
                player_sc.AddScore(10);
            }
            anim.SetTrigger("OnEnemyDeath");
            speed = 0;
            audioSource.Play();
            Destroy(this.gameObject, 2.3f);
        }
        Debug.Log("Hit: " + other.transform.name);
    }
}
