using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;

    [SerializeField] private int powerupID; // 0 : triple shoot 1: speed  2: shield

    [SerializeField] private AudioClip _clip;
    

    // Update is called once per frame
    void Update()
    {
        //power up move down
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y <= -6)
        {
            Destroy(this.gameObject);
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
                
        if( other.tag == "Player")
        {
            //access to the player class
            Player player = other.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);

            if (player != null)
            {
                if (powerupID == 0)
                {
                    //enable powerup triple shoot
                    player.TripleShootPowerUpOn();
                }

                else if (powerupID == 1)
                {
                    // enable powerup extra speed
                    player.SpeedBoostPowerUpOn();

                }
                
                else if (powerupID == 2)
                {
                    // enable powerup shield
                    player.ShieldActivePowerUpOn();
                }
            }

            //destroy game object
            Destroy(this.gameObject);
        }
   

    }
}
