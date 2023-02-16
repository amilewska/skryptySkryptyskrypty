using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 offset = new Vector3(35, 17, 0);
    [SerializeField] float delay = 0.001f;
    [SerializeField] float speed = 0.3f;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, delay);
        if(player.transform.position.z>37) delay = 0.009f;
        //if (GameManager.Instance.score >= GameManager.Instance.score) delay = 0.05f;
        if (GameManager.Instance.timer <= 30) StartCoroutine(CameraRotatating());
    }
    IEnumerator CameraRotatating()
    {
        Quaternion startPos = Quaternion.Euler(2.4f, -90, 0);
        Quaternion endPos = Quaternion.Euler(2.4f, -90, 180);
        float time = 0;
        while (time < 5)
        {
            transform.rotation = Quaternion.Lerp(startPos, endPos, time);
            time += Time.deltaTime * speed;
            yield return null;
        }

        time = 0;
        while (time < 4)
        {
            transform.rotation = Quaternion.Lerp(endPos, startPos, time);
            time += Time.deltaTime * speed;
            yield return null;
        }
    }
}
