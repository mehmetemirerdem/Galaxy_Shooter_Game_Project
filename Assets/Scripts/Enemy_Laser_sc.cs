using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Laser_sc : MonoBehaviour
{
    [SerializeField]
    private float speed = 4.5f;

    private Player_sc player_sc;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);

        if (transform.position.y < -5.0f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            //TODO Damage the player
            Player_sc player = other.transform.GetComponent<Player_sc>();
            if (player != null)
            {
                player.Damage();
            }
            Destroy(this.gameObject);
        }
    }
}
