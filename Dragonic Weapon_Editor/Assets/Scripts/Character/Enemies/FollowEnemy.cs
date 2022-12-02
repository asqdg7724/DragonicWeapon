using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    #region FIELDS
    public int hp = 1;

    [Tooltip("Enemy's projectile prefab")]
    public GameObject Projectile;

    public GameObject effect;

    [HideInInspector] public int shotChance; //probability of 'Enemy's' shooting during tha path
    [HideInInspector] public float shotTimeMin, shotTimeMax; //max and min time for shooting from the beginning of the path
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        Invoke("ActivateShooting", Random.Range(shotTimeMin, shotTimeMax));
    }

    void ActivateShooting() {
        if (Random.value < (float)shotChance / 100)
        {
            Instantiate(Projectile, gameObject.transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "PlayerBullet")
        {
            hp--;

            if (hp == 0)
            {
                GameManager.GM.EnemyDefeatSound();
                Instantiate(effect, gameObject.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                Destroy(col.gameObject);
                GameManager.GM.Enemy_Score_Up();
            }
        }
    }
}
