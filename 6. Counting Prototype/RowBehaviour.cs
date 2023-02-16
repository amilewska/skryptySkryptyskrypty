using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowBehaviour : MonoBehaviour
{
    float timer;

    //variables for lerp
    float speed = 1;
    [SerializeField] Transform startPos;
    [SerializeField] Transform endPos;
    [SerializeField] float startTime;

    
    // Update is called once per frame
    void Update()
    {
        timer = GameManager.Instance.timer;
        if (timer <=  startTime)
        {
            StartCoroutine(LerpMoving());
        }


    }

    IEnumerator LerpMoving()
    {
        while (true)
        {
            float time = 0;
            while (time < 2)
            {
                transform.position = Vector3.Lerp(startPos.position, endPos.position, time);
                time += Time.deltaTime * speed;
                yield return null;
            }

            time = 0;
            while (time < 2)
            {
                transform.position = Vector3.Lerp(endPos.position, startPos.position, time);
                time += Time.deltaTime * speed;
                yield return null;
            }
        }
    }
}
