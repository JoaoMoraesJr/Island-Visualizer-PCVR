using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementController : MonoBehaviour
{
    public float moveDamage = 20;
    public GameObject model;
    public GameObject trailPrefab;
    public float transparency = .65f;
    public float minTrailDistance = 0.05f;
    private GameObject trail;
    private Vector3 lastPosition;
    private bool isMoving = false;
    private Material material;
    public Animator elementAnimator;
    public float dissipateTime = 10f;
    public float startTime;
    public bool isPermanent = false;
    public Collider collider;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        lastPosition = transform.position;
        model.GetComponent<MeshRenderer>().material = new Material(model.GetComponent<MeshRenderer>().material);
        material = model.GetComponent<MeshRenderer>().material;
        material.SetFloat("_Transparency", transparency);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, lastPosition);
        if (distance >=  minTrailDistance && !isMoving)
        {
            isMoving = true;
            //material.SetFloat("_Transparency", 0f);
            collider.enabled = false;
            elementAnimator.SetBool("isMoving", true);
            trail = Instantiate(trailPrefab, this.transform.position, Quaternion.identity);
            trail.GetComponent<FollowDelay>().target = GetComponent<FollowDelay>().target;
        }
        else if (distance < minTrailDistance && isMoving)
        {
            isMoving = false;
            collider.enabled = true;
            trail.GetComponent<TrailController>().DestroyTrail();
            elementAnimator.SetBool("isMoving", false);
            trail.GetComponent<FollowDelay>().target = trail.transform;
            //material.SetFloat("_Transparency", transparency);
        }
        lastPosition = transform.position;
        if (Time.time - startTime >= dissipateTime && !isPermanent)
        {
            if (trail != null) trail.GetComponent<TrailController>().DestroyTrail();
            collider.enabled = false;
            elementAnimator.SetBool("Dissipate", true);
            Destroy(this.gameObject, 2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Enemy"))
        {
            Health enemyHealth = other.GetComponentInParent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(moveDamage);
            }
        }
    }

    //public void Interact(UnityEngine.XR.Interaction.Toolkit.HoverEnterEventArgs args)
    //{
    //    Debug.Log("Interacted 1");
    //    Debug.Log(args);
    //}
}
