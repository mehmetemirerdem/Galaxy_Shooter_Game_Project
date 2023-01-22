using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_sc : MonoBehaviour
{
    private UIManager_sc uiManager_sc;
    
    [SerializeField]
    private int score = 0;

    [SerializeField]
    private int lives = 3;

    [SerializeField]
    private float speed = 3.5f;

    private float speedMultiplier = 2;

    private float bonusDuration = 5.0f;

    private bool isTripleShotActive = false;

    private bool isSpeedBoostActive = false;

    private bool isShieldPowerupActive = false;

    [SerializeField]
    private GameObject laserPrefab;

    [SerializeField]
    private GameObject tripleShotPrefab;

    [SerializeField]
    private GameObject shieldVisualizer;

    [SerializeField]
    private float fireRate = 0.5f;
    [SerializeField]
    private float nextFire = 0f;

    private SpawnManager_sc sm_sc;

    [SerializeField]
    private GameObject rightEngine, leftEngine;

    [SerializeField]
    private AudioClip laserSoundClip;
    private AudioSource audioSource;

    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-2, 0, 0);
        sm_sc = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager_sc>();
        uiManager_sc = GameObject.Find("Canvas").GetComponent<UIManager_sc>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        if (audioSource == null)
        {
            Debug.LogError("Audio Source is NULL!");
        }
        else
        {
            audioSource.clip = laserSoundClip;
        }
        
        if (sm_sc == null) {
            Debug.Log("Spawn Manager Script is NULL!!");
        }

        if (uiManager_sc == null)
        {
            Debug.Log("UI Manager Script is NULL!!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            FireLaser();
        }

        if (Input.GetAxis("Horizontal") < 0.0f)
        {
            animator.SetBool("left", false);
        }
        else
        {
            animator.SetBool("left", true);
        }

        if (Input.GetAxis("Horizontal") > 0.0f)
        {
            animator.SetBool("right", false);
        }
        else
        {
            animator.SetBool("right", true);
        }
    }

    public void TripleShotActive() {
        isTripleShotActive = true;
        StartCoroutine( TripleShotBonusDisableRoutine() );
    }

    IEnumerator TripleShotBonusDisableRoutine() {
        yield return new WaitForSeconds(bonusDuration);
        isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        isSpeedBoostActive = true;
        speed *= speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedBoostActive = false;
        speed /= speedMultiplier;
    }

    public void ShieldPowerupActive()
    {
        isShieldPowerupActive = true;
        shieldVisualizer.SetActive(true);
    }
    public void AddScore(int points)
    {
        score += points;
        uiManager_sc.UpdateScore(score);
    }

    public void Damage() {

        if(isShieldPowerupActive == true)
        {
            isShieldPowerupActive = false;
            shieldVisualizer.SetActive(false);
            return;
        }
        lives--;
        uiManager_sc.UpdateLives(lives);

        if (lives < 1) {
            sm_sc.OnPlayerDeath();
            Destroy(this.gameObject);
        }

        if (lives == 2)
        {
            leftEngine.SetActive(true);
        }
        else if (lives == 1)
        {
            rightEngine.SetActive(true);
        }
    }

    void FireLaser() {
        if (isTripleShotActive == true) {
            Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
        } else {
            Instantiate(laserPrefab, transform.position+new Vector3(0, 1.35f, 0), Quaternion.identity);
        }

        audioSource.Play();
    }
    void calculateMovement() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(Time.deltaTime * speed * direction);

        //Set Y position
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0) , 0);
        //Set X position
        if (transform.position.x >= 11.3f) {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        } else if (transform.position.x <= -11.3f) {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }

    }
}
