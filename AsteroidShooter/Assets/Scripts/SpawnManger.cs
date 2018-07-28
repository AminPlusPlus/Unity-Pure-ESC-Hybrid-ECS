using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using Unity.Transforms2D;
using UnityEngine;
using UnityScript.Scripting;

public class SpawnManger : MonoBehaviour
{


	public GameObject ast;

    [Header("SpaceShip")]
    [SerializeField] private Mesh spaceShip;
    [SerializeField] private Material spaceShipMaterial;

    [Header("Asteroid")]
    [SerializeField] private Mesh asteroid;
    [SerializeField] private Material asteroidMaterial;

	[Space(10)]
	[SerializeField] private float asteroidOffset = 2f;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private void Start()
    {
        initEntity();
    }

    // Creating entitiy manager
    void initEntity()
    {
        //Entity Manager
        var entityManager = World.Active.GetOrCreateManager<EntityManager>();

        //create SpaceShip
       // createSpaceShip(entityManager);
        //create Asteroid
        createAsteroid(entityManager);
    }

    void createSpaceShip(EntityManager entityManager)
    {
        //SpaceShip
        var shipArch = entityManager.CreateArchetype(
            typeof(TransformMatrix),
            typeof(Position),
            typeof(MeshInstanceRenderer)
        );

        //Create Asteroid
        var ship = entityManager.CreateEntity(shipArch);

		entityManager.SetComponentData(ship, new SpaceShipInput {x = Random.Range(0,10)} );

        entityManager.SetSharedComponentData(ship, new MeshInstanceRenderer
        {
            mesh = spaceShip,
            material = spaceShipMaterial
        });
    }


    void createAsteroid(EntityManager entityManager)
    {
        //Asteroid
        var asteroidArch = entityManager.CreateArchetype(
            typeof(TransformMatrix),
            typeof(Position),
            typeof(MeshInstanceRenderer),
            typeof(MeshCollider),
			typeof(Asteroid)
        );

		// X and Y Position offsets
		float Xoffset = asteroidOffset;
        float Yoffest = asteroidOffset;

        for (int x = -128; x < 127; x++)
        {
			Xoffset  = Xoffset + asteroidOffset;
            for (int y = -128; y < 127; y++)
            {
                var ast = entityManager.CreateEntity(asteroidArch);

        

                entityManager.SetSharedComponentData(ast, new MeshInstanceRenderer
                {
                    mesh = asteroid,
                    material = asteroidMaterial
                });

                entityManager.SetComponentData(ast, new Position { Value = new float3(x + Xoffset, y + Yoffest, 0)});
				entityManager.SetComponentData(ast, new Asteroid 
                { 
                    x =  Random.Range(-5,5), 
                    y = Random.Range (-5,5)
                });

				Yoffest = Yoffest + asteroidOffset;
            }
			Yoffest = asteroidOffset;
        }
    }








}