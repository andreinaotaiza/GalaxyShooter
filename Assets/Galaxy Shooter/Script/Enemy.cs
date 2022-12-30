using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;

    [SerializeField] private GameObject _enemyExplosionPrefab;

    [SerializeField] private AudioClip _clip;

    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -6)
        {
            float randomX = Random.Range(-9.0f, 9.0f);
            transform.position = new Vector3 (randomX, 7f  , 0) ;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Laser")
        {
            if(other.transform.parent != null)
            {
                Destroy(transform.parent);
            }
            Destroy(other.gameObject);
            EnemyExplosion();
            _uiManager.UpdateScore();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);
        }

        else if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            
            if(player != null)
            {
                player.Damage();
            }

            EnemyExplosion();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);
            
        }   
    }
    public void EnemyExplosion()
    {
        Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
    }
}
