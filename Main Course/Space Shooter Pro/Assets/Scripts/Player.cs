using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isPlayerOne = false;
    public bool isPlayerTwo = false;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    private float horizontalBound = 11f;
    [SerializeField] private float _speed = 3.5f;
    private float _speedMultiplier = 2f;
    [SerializeField] private float _fireRate = 0.2f;
    private float _canFire = -1;
    [SerializeField] private int _lives = 3;
    private SpawnManager _spawnManager;
    private bool _isTripleShotActive = false;
    private bool _isShieldActive = false;
    [SerializeField] private GameObject _shieldVisualizer;
    private int _score = 0;
    private UIManager _uiManager;
    [SerializeField] private GameObject _righttEngineDamage, _leftEngineDamage;
    AudioSource _audioSource;
    [SerializeField] private AudioClip _laserSFX;
    private GameManager _gameManager;
    private Animator _animator;
    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (_gameManager.isMultiPlayerMode == false)
        {
            //player starting position 
            transform.position = new Vector3(0, 0, 0);
        }
        
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        if (_spawnManager == null)
        {
            Debug.LogError("spawn manager script not found! it's NULL");
        }
        if (_uiManager == null)
        {
            Debug.LogError("UI Manager script not found! it's NULL");
        }

        if (_audioSource == null)
        {
            Debug.LogError("audio source of player is null");
        }
        else
        {
            _audioSource.clip = _laserSFX;
        }

        _animator = GetComponent<Animator>();

    }
    
    void Update()
    {
        if (isPlayerOne)
        {
            CalculateMovement();
            if ((Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire))
            {
                FireLaser();
            }
        }

        if (isPlayerTwo)
        {
            CalculatePlayertwoMovement();
            if ((Input.GetKeyDown(KeyCode.RightShift) && Time.time > _canFire))
            {
                FireLaserPlayerTwo();    
            }
            
        }
    }

    void CalculateMovement()
    {
        //user inputs WASD
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        //player movement
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        
        //anim
        if (Input.GetKeyDown(KeyCode.A))
        {
            _animator.SetBool("turnLeft",true);
            _animator.SetBool("turnRight",false);
        } else if (Input.GetKeyUp(KeyCode.A))
        {
            _animator.SetBool("turnLeft",false);
            _animator.SetBool("turnRight",false);
        } else if (Input.GetKeyDown(KeyCode.D))
        {
            _animator.SetBool("turnLeft",false);
            _animator.SetBool("turnRight",true);
        } else if (Input.GetKeyUp(KeyCode.D))
        {
            _animator.SetBool("turnLeft",false);
            _animator.SetBool("turnRight",false);
        }

        //player bounds
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.8f, 0), 0);

        if (transform.position.x >= horizontalBound)
        {
            transform.position = new Vector3(-horizontalBound, transform.position.y, 0);
        } else if (transform.position.x < -horizontalBound)
        {
            transform.position = new Vector3(horizontalBound, transform.position.y, 0);
        }
    }
    
    void CalculatePlayertwoMovement()
    {
        //user inputs WASD
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        //player movement
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        
        //anim
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _animator.SetBool("turnLeft",true);
            _animator.SetBool("turnRight",false);
        } else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _animator.SetBool("turnLeft",false);
            _animator.SetBool("turnRight",false);
        } else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _animator.SetBool("turnLeft",false);
            _animator.SetBool("turnRight",true);
        } else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            _animator.SetBool("turnLeft",false);
            _animator.SetBool("turnRight",false);
        }

        //player bounds
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.8f, 0), 0);

        if (transform.position.x >= horizontalBound)
        {
            transform.position = new Vector3(-horizontalBound, transform.position.y, 0);
        } else if (transform.position.x < -horizontalBound)
        {
            transform.position = new Vector3(horizontalBound, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        if (_isTripleShotActive)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }

        _audioSource.Play();
    }
    
    void FireLaserPlayerTwo()
    {
        _canFire = Time.time + _fireRate;
        if (_isTripleShotActive)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }

        _audioSource.Play();
    }

    public void Damage()
    {
        if (_isShieldActive)
        {
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }
        _lives--;
        if (_lives == 2)
        {
            _righttEngineDamage.SetActive(true);
        } else if (_lives == 1)
        {
            _leftEngineDamage.SetActive(true);
        }
        else
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
            
        }
        _uiManager.UpdateLives(_lives);
        
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    private IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _speed *= _speedMultiplier;
        //_isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _speed /= _speedMultiplier;
        //_isSpeedBoostActive = false;
    }

    public void ShieldPowerActive()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScoreText(_score);
    }
}
