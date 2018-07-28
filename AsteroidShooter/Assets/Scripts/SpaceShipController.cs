using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour 
{

	[SerializeField] private float speed = 150;
	[SerializeField] private float speedRotation = 3;

	[Space(5)]
	[Header("Bullet")]
	[SerializeField] private Rigidbody2D bullet;
	[SerializeField] private float bulletSpeed = 30f;
	[SerializeField] private Transform bulletPull;

	  float fireRate = 0.5F;
  	  float nextFire = 0.0F;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine(Execute());

		fire();
	}
	
	
	//Update every frame optimization instead of void Update
	IEnumerator Execute ()
	{
		while (true)
		{
			movement();
			if (Time.time > nextFire)
			fire();

			yield return new WaitForEndOfFrame();
		}
	}


	//movement of Spaceship
	void movement()
	{
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * speedRotation;

        transform.Rotate(0, 0, -x);
        transform.Translate(0, z, 0);
	}

	//Fire bullet 
	void fire ()
	{
		//calculation next fire
		nextFire = Time.time + fireRate;

		Rigidbody2D b =Instantiate(bullet, bulletPull.position, Quaternion.Euler(new Vector3(0, 0, 1))) as Rigidbody2D;
		b.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;

		Physics2D.IgnoreCollision(b.GetComponent<Collider2D>(),  GetComponent<Collider2D>());
	}


	private void OnTriggerEnter2D(Collider2D other)
	{
		// Game over buy trigger asteroid 
		GameManager.instance.GameOver();	
	}
}
