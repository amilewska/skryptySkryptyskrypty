using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    Rigidbody rb;
    public bool isCutting = false;
    public GameObject prefabTrail;
    GameObject currentBladeTrail;
    public float minCuttingVelocity = .001f;
    SphereCollider colliderSphere;
    MeshRenderer meshSphere;
    Vector3 previousPosition;

    private AudioSource soundEffect;
    public AudioClip clickEffect;
    public AudioClip crashEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        colliderSphere = GetComponent<SphereCollider>();
        meshSphere = GetComponent<MeshRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) StartCutting();
        else if (Input.GetMouseButtonUp(0)) StopCutting();

        if (isCutting) UpdateCut();
    }

    void UpdateCut()
    {
        Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        rb.position = Camera.main.ScreenToWorldPoint(newPosition);
        
        float velocity = (newPosition - previousPosition).magnitude*Time.deltaTime;

        if(velocity > minCuttingVelocity)
        {
            colliderSphere.enabled = true;
            meshSphere.enabled = true;
        }
        else
        {
            colliderSphere.enabled = false;
            meshSphere.enabled = false;
        }

        previousPosition = newPosition;
    }

    void StartCutting()
    {
        
        isCutting = true;
        currentBladeTrail = Instantiate(prefabTrail, transform);
        previousPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        colliderSphere.enabled = false;
        meshSphere.enabled = false;
    }

    void StopCutting()
    {
        isCutting = false;
        currentBladeTrail.transform.SetParent(null);
        Destroy(currentBladeTrail, 2);
        colliderSphere.enabled = false;
        meshSphere.enabled = false;

    }


}
