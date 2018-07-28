using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{

	private void Start()
	{
		Destroy(this.gameObject,3f);
	}


	private void OnTriggerEnter2D(Collider2D other)
	{
		//Increase Score by 1
		GameManager.instance.Score++;

		//Destroy bullet
		Destroy(gameObject);
	}

}
