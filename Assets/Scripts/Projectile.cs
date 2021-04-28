using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 50;
    private RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        Vector3 previousPosition = transform.position;
        transform.position += transform.forward * speed * Time.deltaTime;

        if (Physics.SphereCast(previousPosition, 0.25f, transform.forward, out hit, Vector3.Distance(previousPosition, transform.position)))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Hit player");
            }
            Destroy(gameObject);
        }
    }
}
