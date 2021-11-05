using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _speedAsteroid = 20f;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;
   
   
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _speedAsteroid * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _spawnManager.StartSpawning();
           
            Destroy(GetComponent<Collider2D>());
            Destroy(other.gameObject);
            Destroy(this.gameObject, 0.5f);
           
        }
        
    } 
}
