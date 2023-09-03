using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float zBoundryDown;
    [SerializeField] float zBoundryUp;

    [SerializeField] GameObject powerUpIndicator;

    private bool isPowerUp;

    private Rigidbody rigid;
    private GameManager gameManager;

    [SerializeField] TMP_Text pointText;
    private int pointCount = 0;

    [SerializeField] ParticleSystem explosion;

    [SerializeField] AudioSource explosionSound;
    [SerializeField] AudioSource hurtSound;
    [SerializeField] AudioSource coinSound;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    
    void Update()
    {
        if (gameManager.isGameStart==true)
        {
            PlayerMovement();
        }

        PlayerBoundry();

        explosion.transform.position = new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z);
    }

    void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        rigid.AddForce(Vector3.forward * speed * verticalInput);
        rigid.AddForce(Vector3.right * speed * horizontalInput);
    }

    void PlayerBoundry()
    {
        if (transform.position.z < zBoundryDown)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBoundryDown);
        }
        if (transform.position.z > zBoundryUp)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBoundryUp);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")&&!isPowerUp)
        {
            Destroy(collision.gameObject);
            gameManager.Health();
            hurtSound.Play();
        }
        if (collision.gameObject.CompareTag("Enemy") && isPowerUp)
        {
            Destroy(collision.gameObject);
            pointCount++;
            pointText.text = "Points: " + pointCount;
        }

        if (collision.gameObject.CompareTag("PowerUp"))
        {
            StartCoroutine(PowerUp());
            Destroy(collision.gameObject);
            coinSound.Play();
        }
        if (collision.gameObject.CompareTag("Point"))
        {
            pointCount++;
            pointText.text = "Points: " + pointCount;
            Destroy(collision.gameObject);
            coinSound.Play();
        }
    }

    IEnumerator PowerUp()
    {
        powerUpIndicator.SetActive(true);
        isPowerUp = true;
        yield return new WaitForSeconds(5);
        powerUpIndicator.SetActive(false);
        isPowerUp = false;
    }

    public void GameOver()
    {
        explosionSound.Play();
        Destroy(gameObject);
        explosion.Play();
    }
}
