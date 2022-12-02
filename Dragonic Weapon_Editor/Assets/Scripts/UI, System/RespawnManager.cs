using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public GameObject obj;
    public Transform rewpawnTr;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RespawnEnemy());
    }

    IEnumerator RespawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            float range = (float)Screen.width / (float)Screen.height * Camera.main.orthographicSize;
            Instantiate(obj, rewpawnTr.position + 
                new Vector3(Random.Range(-range + 0.3f, range - 0.3f), 0, 0), Quaternion.identity);
        }
    }
}
