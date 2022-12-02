using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    public Transform tr;
    public GameObject effect;

    public float speed = 10f;

    public int hp = 1;
    public int initHp = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tr.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "PlayerBullet")
        {
            hp--;

            if (hp == 0)
            {
                GameManager.GM.EnemyDefeatSound();
                Instantiate(effect, tr.position, Quaternion.identity);
                Destroy(this.gameObject);
                Destroy(col.gameObject);
                GameManager.GM.Enemy_Score_Up();
            }
        }

        if (col.tag == "Destroyer")
        {
            Destroy(this.gameObject);
        }
    }
}
