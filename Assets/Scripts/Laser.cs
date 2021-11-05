using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update
    // speed varible 8
    [SerializeField]
    private float _speedLaser = 8.0f;
    private bool _isEnemyLaser = false;

    // Update is called once per frame
    void Update()
    {
        if (_isEnemyLaser == false)
        {
            MoveUp();
        } 
        else
        {
           MoveDown();
        } 
    }

    public void MoveUp()
    {
        //trenslate laser up
        transform.Translate(Vector3.up * _speedLaser * Time.deltaTime);

        //if laser position is greater than 8 on y
        // destroy object
        if (transform.position.y > 8f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    public void MoveDown()
    {
        //trenslate laser up
        transform.Translate(Vector3.down * _speedLaser * Time.deltaTime);

        //if laser position is greater than 8 on y
        // destroy object
        if (transform.position.y < -8f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
    public void AssignEnemylaser()
    {
        _isEnemyLaser = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _isEnemyLaser == true)
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Demage();
            }
          
        }
    }
}
