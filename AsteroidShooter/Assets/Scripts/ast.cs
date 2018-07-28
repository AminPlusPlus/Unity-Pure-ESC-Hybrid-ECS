using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class ast : MonoBehaviour 
{
	public float speed = 5f;
	public float x;
	public float y;

	 private void Start()
	{
		x =  Random.Range(-3,3);
		y = Random.Range(-3,3);
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		Destroy(gameObject);
	}
}

public class RotatorSystem : ComponentSystem
{

	struct Components
	{
		public ast rotator;
		public Transform transform;
	}

	
	protected override void OnUpdate()
	{
		foreach (var item in GetEntities<Components>())
		{
			item.transform.Translate(item.rotator.x * Time.deltaTime,item.rotator.y * Time.deltaTime,0f);
		}
	}

}
