using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public int hp = 3;
    public int initHp = 3;

    public Rigidbody2D rb;
    public float speed = 300f;
    Animator animator;

    public Transform tr;
    public bool isAlive;
    public bool isPaused;

    float Moveh; 
    float Movev; 

    public static PlayerCtrl PC;

    public GameObject Life1, Life2, Life3;

    Renderer renderer;
    BoxCollider2D hitColider;
    Color color;

    void Awake()
    {
        hp = initHp;

        isAlive = true;
        isPaused = false;

        Life1.gameObject.SetActive(true);
        Life2.gameObject.SetActive(true);
        Life3.gameObject.SetActive(true);

        PC = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<Renderer>();
        hitColider = GetComponent<BoxCollider2D>();
        color = renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp > initHp)
            hp = initHp;

        switch(hp)
        {
            case 3:
                Life1.gameObject.SetActive(true);
                Life2.gameObject.SetActive(true);
                Life3.gameObject.SetActive(true);
                break;
            case 2:
                Life1.gameObject.SetActive(true);
                Life2.gameObject.SetActive(true);
                Life3.gameObject.SetActive(false);
                break;
            case 1:
                Life1.gameObject.SetActive(true);
                Life2.gameObject.SetActive(false);
                Life3.gameObject.SetActive(false);
                break;
            case 0:
                Life1.gameObject.SetActive(false);
                Life2.gameObject.SetActive(false);
                Life3.gameObject.SetActive(false);
                break;
        }

        if (isAlive == true)
        {
            Moveh = Input.GetAxis("Horizontal");
            Movev = Input.GetAxis("Vertical");

            if (Input.GetKeyDown(KeyCode.P))
            {
                if (isPaused == false)
                {
                    GameManager.GM.GamePauseOn();
                    isPaused = true;
                }

                else if (isPaused == true)
                {
                    GameManager.GM.GamePauseOff();
                    isPaused = false;
                }
            }
        }

        Vector2 dir = new Vector2(Moveh, Movev);
        rb.velocity = dir * speed * Time.deltaTime;

        if (Moveh == 0)
        {
            animator.SetTrigger("Base");
        }

        else if (Moveh < 0)
        {
            animator.SetTrigger("MoveLeft");
        }

        else if (Moveh > 0)
        {
            animator.SetTrigger("MoveRight");
        }


        //화면 밖으로 나가지 못하게
        float size = Camera.main.orthographicSize;
        float offset = 0.4f;

        if (tr.position.y >= size - offset)
        {
            tr.position = new Vector3(tr.position.x, size - offset, 0);
        }

        if (tr.position.y <= -size + offset)
        {
            tr.position = new Vector3(tr.position.x, -size + offset, 0);
        }

        float screenRatio = (float)Screen.width / (float)Screen.height;
        float wSize = Camera.main.orthographicSize * screenRatio;

        if (tr.position.x >= wSize - offset)
        {
            tr.position = new Vector3(wSize - offset, tr.position.y, 0);
        }

        if (tr.position.x <= -wSize + offset)
        {
            tr.position = new Vector3(-wSize + offset, tr.position.y, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "EnemyBullet" || col.tag == "Enemy")
        {
            GameManager.GM.PlayerHitEffect();

            Destroy(col.gameObject);

            hp--;

            StartCoroutine("FlashingPlayer");

            if (hp == 0)
            {
                isAlive = false;

                GameManager.GM.PlayerExplosionEffect();

                Life1.gameObject.SetActive(false);

                Destroy(this.gameObject);

                Debug.Log("PlayerDefeat");

                GameManager.GM.PlayerGameOver();
            }
        }

        if (col.tag == "BossBullet")
        {
            GameManager.GM.PlayerHitEffect();

            hp--;

            StartCoroutine("FlashingPlayer");

            if (hp == 0)
            {
                isAlive = false;

                GameManager.GM.PlayerExplosionEffect();

                Life1.gameObject.SetActive(false);

                Destroy(this.gameObject);

                Debug.Log("PlayerDefeat");

                GameManager.GM.PlayerGameOver();
            }
        }
    }

    IEnumerator FlashingPlayer()
    {
        hitColider.enabled = false;
        color.a = 0.5f;
        renderer.material.color = color;
        yield return new WaitForSeconds(3f);
        hitColider.enabled = true;
        color.a = 1f;
        renderer.material.color = color;
    }

    public void ClearInvincible()
    {
        hitColider.enabled = false;
        color.a = 0.5f;
        renderer.material.color = color;
    }
}
