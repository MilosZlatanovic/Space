using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _powerupSpeed = 3.0f;
    [SerializeField]
    private int powerID = 0;
  
    [SerializeField]
    private AudioClip _powerAudioClip;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _powerupSpeed * Time.deltaTime);
        if (transform.position.y <= -10.0f)
        {
            Destroy(this.gameObject);

            /* float randomX = Random.Range(-8f, 8f);
             transform.position = new Vector3(randomX, 7, 0);*/
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(_powerAudioClip, transform.position);
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                switch (powerID)
                {
                    case 0:
                        player.TripleShotActive();
                        
                        break;
                    case 1:
                        player.SpeedBoostActive();
                       
                        break;
                    case 2:
                        player.ShieldActivate();
                       
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

