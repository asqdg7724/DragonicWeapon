using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCtrl : MonoBehaviour
{
    public Animator anim;

    public bool BossAlive;

    public int Hp = 1000;

    IEnumerator play_BossPattern = null;

    [SerializeField]
    private int LeftbulletsAmount = 10;
    [SerializeField]
    private float LeftstartAngle = 90f, LeftendAngle = 270f;

    [SerializeField]
    private int RightbulletsAmount = 10;
    [SerializeField]
    private float RightstartAngle = 90f, RightendAngle = 270f;

    private Vector2 bulletMoveDirection;

    Renderer renderer;
    Color color;

    void Awake()
    {
        anim = GetComponent<Animator>();

        BossAlive = true;

        GameManager.GM.BossAppearEvent();
    }

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        color = renderer.material.color;

        StartCoroutine(BossPattern(true));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LeftFire()
    {
        float angleStep = (LeftendAngle - LeftstartAngle) / LeftbulletsAmount;
        float angle = LeftstartAngle;

        for (int i = 0; i < LeftbulletsAmount + 1; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = LeftBulletPool.leftBulletPoolInstanse.GetLeftBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<BossBulletMove>().SetMoveDirection(bulDir);

            angle += angleStep;
        }
    }

    private void RightFire()
    {
        float angleStep = (RightendAngle - RightstartAngle) / RightbulletsAmount;
        float angle = RightstartAngle;

        for (int i = 0; i < RightbulletsAmount + 1; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = RightBulletPool.rightBulletPoolInstanse.GetRightBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<BossBulletMove>().SetMoveDirection(bulDir);

            angle += angleStep;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "PlayerBullet")
        {
            Destroy(col.gameObject);

            Hp--;

            StartCoroutine("FlashingBoss");

            if (Hp <= 0)
            {
                BossAlive = false;

                GameManager.GM.BossExplosionEffect();

                Destroy(this.gameObject);

                Debug.Log("BossDefeat");

                GameManager.GM.PlayerGameClear();
            }
        }
    }

    IEnumerator FlashingBoss()
    {
        color.a = 0.7f;
        renderer.material.color = color;
        yield return new WaitForSeconds(0.5f);
        color.a = 1f;
        renderer.material.color = color;
    }

    IEnumerator BossPattern(bool firstpattern)
    {
        while(BossAlive)
        {
            if (firstpattern)
            {
                firstpattern = false;

                yield return new WaitForSeconds(4f);

                anim.SetTrigger("LeftAttack");

                yield return new WaitForSeconds(1f);

                InvokeRepeating("LeftFire", 0f, 0.5f);

                yield return new WaitForSeconds(6f);

                CancelInvoke("LeftFire");

                anim.SetTrigger("RightAttack");

                yield return new WaitForSeconds(1f);

                InvokeRepeating("RightFire", 0f, 0.5f);

                yield return new WaitForSeconds(6f);

                CancelInvoke("RightFire");

                anim.SetTrigger("TwoHandAttack");

                yield return new WaitForSeconds(2f);

                InvokeRepeating("LeftFire", 0f, 0.5f);

                InvokeRepeating("RightFire", 0f, 0.5f);

                yield return new WaitForSeconds(10f);

                CancelInvoke("LeftFire");

                CancelInvoke("RightFire");

                anim.SetTrigger("Reset");

                yield return null;
            }

            else if (!firstpattern)
            {
                yield return new WaitForSeconds(2f);

                anim.SetTrigger("LeftAttack");

                yield return new WaitForSeconds(1f);

                InvokeRepeating("LeftFire", 0f, 0.5f);

                yield return new WaitForSeconds(6f);

                CancelInvoke("LeftFire");

                anim.SetTrigger("RightAttack");

                yield return new WaitForSeconds(1f);

                InvokeRepeating("RightFire", 0f, 0.5f);

                yield return new WaitForSeconds(6f);

                CancelInvoke("RightFire");

                anim.SetTrigger("TwoHandAttack");

                yield return new WaitForSeconds(2f);

                InvokeRepeating("LeftFire", 0f, 0.5f);

                InvokeRepeating("RightFire", 0f, 0.5f);

                yield return new WaitForSeconds(10f);

                CancelInvoke("LeftFire");

                CancelInvoke("RightFire");

                anim.SetTrigger("Reset");

                yield return null;
            }

            //anim.SetTrigger("Die");

            //yield return new WaitForSeconds(1f);

            //Destroy(this.gameObject);

            //break;
        }

    }
}
