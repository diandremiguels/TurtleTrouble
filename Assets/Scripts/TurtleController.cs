using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TurtleController : MonoBehaviour
{
    // want to obtain object that represents body to move
    [SerializeField]
    private Rigidbody rigid;
    [SerializeField]
    private Transform turtleTransform;
    [SerializeField]
    private float MouseSensitivity;
    [SerializeField]
    private float MoveSpeed;
    [SerializeField]
    private Transform camTransform;
    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    private Text lifeText;
    [SerializeField]
    private int lifeDrainFactor;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Image gameScreen;
    [SerializeField]
    private GameObject UIScreen;
    [SerializeField]
    private Text finalScoreText;
    [SerializeField]
    private float jellyfishHealthBonus;
    [SerializeField]
    private GameObject jellyfishPrefab;
    [SerializeField]
    private GameObject plasticBagPrefab;
    [SerializeField]
    private GameObject seaweedPrefab;
    [SerializeField]
    private int numJellyfish;
    [SerializeField]
    private int numPlasticBags;
    [SerializeField]
    private int numSeaweed;
    private Vector2 inputDirection;
    private Vector2 mouseInputDirection;
    public Vector3 rotationSpeed = new Vector3(0, 40, 0);
    private bool canSwim;
    private float health = 100;
    private double score = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        for (int i = 0; i < numJellyfish; i++) {
            spawnObject(jellyfishPrefab, 40, 20, 40);
        }
        for (int i = 0; i < numPlasticBags; i++)
        {
            spawnObject(plasticBagPrefab, 40, 20, 40);
        }
        for (int i = 0; i < numSeaweed; i++)
        {
            spawnObject(seaweedPrefab, 500, -3, 1);
        }
        gameOverScreen.SetActive(false);
        this.canSwim = true;
        this.lifeText.text = "Health: 100%";
        this.scoreText.text = "Score: 0";
        this.gameScreen.color = new Color(255, 0, 0,0);
        this.finalScoreText.text = "Score: 0";
        // start swimming animation
    }

    // Update is called once per frame
    void Update()
    {
        float z = transform.eulerAngles.z;
        if (this.health <= 0)
        {
            endGame();
        }
        this.lifeText.text = "Health: " + (int)this.health + "%";
        this.scoreText.text = "Score: " + (int)this.score;
        if (this.canSwim)
        {
            this.gameScreen.color = new Color(255, 0, 0, 0.01f * (100f - health) - 0.3f);
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
            float h = MouseSensitivity * Input.GetAxis("Mouse X");
            float v = MouseSensitivity * -Input.GetAxis("Mouse Y");
            turtleTransform.Rotate(v, h, 0);
            turtleTransform.Rotate(0, 0, -z);
        }
    }

    void FixedUpdate()
    {
        if (this.canSwim)
        {
            this.score += Time.deltaTime;
            this.health -= Time.deltaTime * this.lifeDrainFactor;
            turtleTransform.position += turtleTransform.forward * MoveSpeed * Input.GetAxis("Vertical");
        } 
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("PlasticBag"))
        {
            endGame();
        } else if (collision.gameObject.tag.Equals("JellyFish"))
        {
            float newHealth = this.health + this.jellyfishHealthBonus;
            this.health = newHealth < 100 ? newHealth : 100;
            Destroy(collision.gameObject);
            this.score++;
            spawnObject(jellyfishPrefab, 40, 20, 40);
            spawnObject(plasticBagPrefab, 40, 20, 40);
        }
    }

    public void playAgain()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("OpeningMenuScene");
    }

    private void spawnObject(GameObject thing, int radius, int minHeight, int maxHeight)
    {
        Instantiate(thing, new Vector3(Random.Range(-1 * radius, radius), Random.Range(minHeight, maxHeight), Random.Range(-1 * radius, radius)), Quaternion.Euler(0, Random.Range(0, 360), 0));
    }

    private void endGame()
    {
        this.gameOverScreen.SetActive(true);
        this.UIScreen.SetActive(false);
        Cursor.visible = true;
        this.canSwim = false;
        this.finalScoreText.text = "Score: " + (int)this.score;
    }

    public void exit()
    {
        Application.Quit();
    }
}
