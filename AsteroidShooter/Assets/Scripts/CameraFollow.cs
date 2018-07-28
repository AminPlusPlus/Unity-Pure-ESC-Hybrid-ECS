using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;

    [HideInInspector]
    public Transform target;

	private Camera camera;

    
	void Start()
	{
        //get component Camera
		camera = GetComponent<Camera>();
        //Set Target
        target = GameObject.FindObjectOfType<SpaceShipController>().transform;
	}
    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 point = camera.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }

    }
}
