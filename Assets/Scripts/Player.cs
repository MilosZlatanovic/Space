using System.Collections;
using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _speedMultiplier = 2f;
    [SerializeField]
    public GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefat;
    [SerializeField]
    private float _fireRate = 0.3f;
    private float _canFire = -1.0f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;

    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;

    [SerializeField]
    public GameObject _shieldVisualizer;
    [SerializeField]
    private GameObject _fireRightVisualizer, _fireLeftVisualizer;


    [SerializeField]
    private int _score;


    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _laserAudioClip;
    [SerializeField]
    private AudioClip _destroyAudioClip;

    private UIManager _uIManager;
   
    void Start()
    {

        // current position
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        _audioSource = GetComponent<AudioSource>();

        if (_spawnManager == null)
        {
            Debug.LogError("Erorr");
        }
        if (_uIManager == null)
        {
            Debug.Log("The UI Manager is NULL");
        }
        if (_audioSource == null)
        {
            Debug.Log("The Audio Clip is NUll");
        }
        else
            _audioSource.clip = _laserAudioClip;
    }

    // Update is called once per frame 
    void Update()
    {
        ClaculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }
    void ClaculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        //transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        transform.Translate(direction * _speed * Time.deltaTime);

        if (transform.position.y >= 6.7f)
        {
            transform.position = new Vector3(transform.position.x, 6.7f, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }
        /*        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);
        */
        if (transform.position.x > 11)
        {
            transform.position = new Vector3(-13.8f, transform.position.y, 0);
        }
        else if (transform.position.x < -13.9f)
        {
            transform.position = new Vector3(11, transform.position.y, 0);
        }
    }
    void FireLaser()
    {
        // if i hit the space key 
        // spawn gameObject
        // delay fire
        {
            _canFire = Time.time + _fireRate;


            //if key space press
            //if tripleActive true
            //fire 3 laser
            if (_isTripleShotActive == true)
            {
                Instantiate(_tripleShotPrefat, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.08f, 0), Quaternion.identity);
            }
            _audioSource.Play();

        }
    }
    public void Demage()
    {
        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }

        _lives--;
        if (_lives == 2)
        {
            _fireRightVisualizer.SetActive(true);
        }
        else if (_lives == 1)
        {
            _fireLeftVisualizer.SetActive(true);
        }

        _uIManager.UpdateLives(_lives);


        if (_lives < 1)
        {
            Destroy(gameObject);
            _spawnManager.OnPlayerDeath();

        }
    }
    public void TripleShotActive()
    {
        _isTripleShotActive = true;

        StartCoroutine(TripleShotPowerDonwRutine());
    }
    IEnumerator TripleShotPowerDonwRutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }
    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostRutine());
    }
    IEnumerator SpeedBoostRutine()
    {

        yield return new WaitForSeconds(5.0f);
        _speed /= _speedMultiplier;
        _isSpeedBoostActive = false;
    }
    public void ShieldActivate()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
    }
    //method add 10 to the score
    //commuicate whid the UI  ot update the score!
   
    public void AddScore(int points)
    {
        _score += points;
        _uIManager.UpdateScore(points);
    }
}