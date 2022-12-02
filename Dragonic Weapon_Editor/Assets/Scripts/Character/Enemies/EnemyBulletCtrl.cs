using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletCtrl : MonoBehaviour
{
    public GameObject[] pos;
    public GameObject bullet;

    public float delayTime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fire", 1.0f, delayTime);
    }

    // Update is called once per frame
    void Update()
    {
        //Fire();
    }

    void Fire()
    {
        for(int i = 0; i < pos.Length; i++)
        {
            Instantiate(bullet, pos[i].transform.position, pos[i].transform.rotation);
        }
    }
}
