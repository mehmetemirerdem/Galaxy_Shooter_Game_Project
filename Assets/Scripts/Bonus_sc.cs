using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus_sc : MonoBehaviour
{
    [SerializeField]
    private float speed = 3.0f;

    [SerializeField]
    private int powerupID;

    [SerializeField]
    private AudioClip audioClip;
    [SerializeField]
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -5.8f) {
            Destroy(this.gameObject);
        }
    }

    public AudioSource PlayClipAt(AudioClip clip, Vector3 pos)
    {
        GameObject tempGO = new GameObject();
        tempGO.name = "TempAudio";
        tempGO.transform.position = pos;

        AudioSource aSource = tempGO.AddComponent<AudioSource>();
        aSource.clip = clip;
        aSource.spatialBlend = 0;

        aSource.Play();

        Destroy(tempGO, clip.length);

        return aSource;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //Üçlü ateş bonusu yakalandı
        if (other.tag == "Player") {

            //Player scriptini elde et
            Player_sc player = other.transform.GetComponent<Player_sc>();
            audioSource = PlayClipAt(audioSource.clip, transform.position);
            //Player scriptinin içindeki bonus aktifleştirme fonksiyonunu çağır
            if (player != null) {
                switch (powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldPowerupActive();
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }

    
}
