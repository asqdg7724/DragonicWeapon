using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Serializable classes
[System.Serializable]
public class EnemyWaves
{
    [Tooltip("time for wave generation from the moment the game started")]
    public float timeToStart;

    [Tooltip("Enemy wave's prefab")]
    public GameObject wave;
}

#endregion

public class LevelController : MonoBehaviour
{
    public GameObject powerItem;

    public float timeForNewPowerup;

    public EnemyWaves[] enemyWaves;

    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        //for each element in 'enemyWaves' array creating coroutine which generates the wave
        for (int i = 0; i < enemyWaves.Length; i++)
        {
            StartCoroutine(CreateEnemyWave(enemyWaves[i].timeToStart, enemyWaves[i].wave));
        }
        StartCoroutine(PoweritemBonusCreation());
    }

    //Create a new wave after a delay
    IEnumerator CreateEnemyWave(float delay, GameObject Wave)
    {
        if (delay != 0)
            yield return new WaitForSeconds(delay);
        if (PlayerCtrl.PC != null)
            Instantiate(Wave);
    }

    IEnumerator PoweritemBonusCreation()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeForNewPowerup);
            Instantiate(
                powerItem,
                //Set the position for the new bonus: for X-axis - random position between the borders of 'Player's' movement; for Y-axis - right above the upper screen border 
                new Vector2(
                    Random.Range(-2.8f, 2.8f),
                    mainCamera.ViewportToWorldPoint(Vector2.up).y + powerItem.GetComponent<Renderer>().bounds.size.y / 2),
                Quaternion.identity
                );
        }
    }
}
