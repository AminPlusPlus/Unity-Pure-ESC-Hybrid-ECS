using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

public class AsteroidMovement : ComponentSystem
{
    public struct Components
    {
        public ComponentDataArray<Position> pos;
		public ComponentDataArray<Asteroid> speed;
        public int Length;

    }

    [Inject] Components asteroidGroup;

    float x;

    protected override void OnCreateManager(int capacity)
    {
      
    }

    protected override void OnUpdate()
    {


        for (int i = 0; i < asteroidGroup.Length; i++)
        {
            //assing cordinate movement
			float x = asteroidGroup.speed[i].x;
            float y = asteroidGroup.speed[i].y;

            var newPos = asteroidGroup.pos[i];


            newPos.Value.x += x * Time.deltaTime;
            newPos.Value.y += y * Time.deltaTime;

            asteroidGroup.pos[i] = newPos;
        }
    }

}
