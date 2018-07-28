using Unity.Entities;

public struct SpaceShipInput : IComponentData 
{
	public float x;
}

public struct Asteroid : IComponentData 
{
	public float x;
	public float y;

}
public struct Fly : IComponentData
{

}