using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] GameObject ballPrefab;
    [SerializeField] GameObject cloudPrefab;
    Vector3 offset = new Vector3(1, -1, 0);

    

     IEnumerator SpawnBall()
    {
        yield return new WaitForSeconds(5);
        while (GameManager.Instance.isGameActive)
        {
            

            GameObject ball = Instantiate(ballPrefab, GenerateSpawnPos(), ballPrefab.transform.rotation);
            GameObject cloud = Instantiate(cloudPrefab, ball.transform.position+offset, ball.transform.rotation);

            yield return new WaitForSeconds(4);
             
            
            
            
            /*GameObject ball = ObjectPool.sharedInstance.GetPooledObject();
            if (ball != null)
            {
                ball.transform.position = spawnPos;

                ball.SetActive(true);
                yield return new WaitForSeconds(2);
                ball.SetActive(false);
            }*/
        }
        

    }

    Vector3 GenerateSpawnPos()
    {
        float spawnPosX = Random.Range(-1f, 1f);
        float spawnPosY = 36;
        float spawnPosZ = Random.Range(-22, 22);
        Vector3 spawnPos = new Vector3(spawnPosX, spawnPosY, spawnPosZ);

        return spawnPos;
    }

}
