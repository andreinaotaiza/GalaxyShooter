using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool canTripleShoot = false;

    public bool canSpeedBoost = false;

    public bool shieldActive = false; 

    public int lives = 3;

    [SerializeField] private GameObject _laserPrefab;

    [SerializeField] private GameObject _tripleShootPrefab;

    [SerializeField] private GameObject _explosionPrefab;

    [SerializeField] private GameObject _shieldGameObject;

    [SerializeField] private GameObject[] _engineFailure;

    [SerializeField] private float _canfire = 0f;

    [SerializeField] public float _fireRate = 0.5f;

    [SerializeField] private float _speed = 5.0f;

    private UIManager _uiManager;

    private GameManager _gameManager;

    private SpawnManager _spawnManager;

    private AudioSource _audioSource;

    private int hitCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);

        hitCount = 0;

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _audioSource = GetComponent<AudioSource>();

        if (_uiManager != null)
        {
            _uiManager.UpdateLives(lives);
        }

        if(_spawnManager != null)
        {
            _spawnManager.StarSpawnCoroutine();
        }

    }


    // Update is called once per frame
    void Update()
    {
        Movement();

        //if space key pressed
        //spawn laser at player position

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }


    private void Shoot()
    {

        if (Time.time > _canfire)
        {
            _audioSource.Play();
            // clone tripleshoot
            if (canTripleShoot == true)
            {
                Instantiate(_tripleShootPrefab, transform.position, Quaternion.identity);
            }
            //shoot simple laser
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
            }
            _canfire = Time.time + _fireRate;
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (canSpeedBoost == true)
        {
            transform.Translate(Vector3.right * horizontalInput * _speed * 5f * Time.deltaTime);
            transform.Translate(Vector3.up * verticalInput * _speed * 5f* Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
            transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
        }


        // position y < 0 && y > 4.2

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        /* limits player on X can't go out the window
         
        if( transform.position.x > 11.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
        else if(transform.position.x < -11.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }*/

        if (transform.position.x > 9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
    }

    public void Damage()
    {
        if(shieldActive == true)
        {
            shieldActive = false;
            _shieldGameObject.SetActive(false);
            return;
        }

        hitCount++;

        if(hitCount == 1)
        {
            _engineFailure[0].SetActive(true);
        }
        else if(hitCount == 2)
        {
            _engineFailure[1].SetActive(true);
        }

        lives--;
        _uiManager.UpdateLives(lives);

        if (lives < 1)
        {
            Explosion();
            _uiManager.ShowScreenTitle();
            _gameManager.gameOver = true;
            Destroy(this.gameObject);
        }






    }

    public void Explosion()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
    }

    //enable power up triple shoot
    public void TripleShootPowerUpOn()
    {
        canTripleShoot = true;
        StartCoroutine(TripleShootPowerDownRoutine());
    }

    //enable power up estra speed
    public void SpeedBoostPowerUpOn()
    {
        canSpeedBoost = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    public void ShieldActivePowerUpOn()
    {
        shieldActive = true;
        _shieldGameObject.SetActive(true);
        StartCoroutine(ShieldActivePowerDownRoutine());
    }

    //cold down power up triple shoot
    public IEnumerator TripleShootPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShoot = false;
    }

    //cold down power up extra speed
    public IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canSpeedBoost = false;
    }

    public IEnumerator ShieldActivePowerDownRoutine()
    {
        yield return new WaitForSeconds(10);
        _shieldGameObject.SetActive(false);
        shieldActive = false;
    }
}
