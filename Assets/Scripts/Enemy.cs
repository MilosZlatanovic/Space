using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField]
    public float _enemySpeed = 6f;

    private Player _player;
    private Animator _animator;
    private AudioSource _audioSource;

    [SerializeField]
    private GameObject _laserPrifab;

    private float _fireRate = 3.0f;
    private float _canFire = -1;


    void Start()
    {
        // _player = GameObject.Find("Player").GetComponent<Player>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _animator = gameObject.GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        CalulateMovement();

        if (Time.time > _canFire)
        { 
            _fireRate = Random.Range(3, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrifab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            
            for (int i=0; i<lasers.Length; i++) 
            {
                lasers[i].AssignEnemylaser();
            }
        }
    }
    public void CalulateMovement()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);
        if (transform.position.y <= -10.0f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if other is Player
        //demage the player
        //Destroy Us

        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Demage();
            }
            _animator.SetTrigger("OnEnemyDead");
            _audioSource.Play();
            _enemySpeed = 0.3f;

            Destroy(this.gameObject, 2.5f);
        }


        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.AddScore(10);
            }
            _animator.SetTrigger("OnEnemyDead");

            _audioSource.Play();
            _enemySpeed = 0.3f;

            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2f);

        }
    }
}