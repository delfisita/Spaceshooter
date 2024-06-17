using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private AudioSource audioSource;

    public GameObject GameManager1;
    public GameObject PlayerBullet;
    public GameObject Bulletposition1;
    public GameObject Bulletposition2;
    public GameObject Explosion;

    public  Text LivesUIText;
    const int MaxLives = 3;
    int lives;
    public float speed;
    public void Init()
    {
        lives = MaxLives;
        LivesUIText.text = lives.ToString();

        transform.position = new Vector2 (0, 0);
        gameObject.SetActive(true);
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            if (audioSource != null)
            {
                audioSource.Play();
            }
            GameObject bullet01 = (GameObject)Instantiate(PlayerBullet);
            bullet01.transform.position = Bulletposition1.transform.position;

            GameObject bullet02 = (GameObject)Instantiate(PlayerBullet);
            bullet02.transform.position = Bulletposition2.transform.position;
        }
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(x, y).normalized;

        Move(direction);
    }

     void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2 (0,0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x - 0.225f;
        min.x = min.x + 0.225f;

        max.y = max.y - 0.285f;
        min.y = min.y + 0.285f;

        Vector2 pos = transform.position;

        pos += direction * speed * Time.deltaTime;

        pos.x = Mathf.Clamp (pos.x, min.x, max.x);

        pos.y = Mathf.Clamp (pos.y, min.y, max.y);

        transform.position = pos;   
    }

   void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.tag == "Enemyshiptag") || (collision.tag =="Enemybullettag"))
        {
            PlayExplosion();
            lives--;
            LivesUIText.text = lives.ToString();

            if(lives == 0)
            {
                GameManager1.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
                gameObject.SetActive(false);
            }
          
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(Explosion);
        explosion.transform.position = transform.position;
    }
}
