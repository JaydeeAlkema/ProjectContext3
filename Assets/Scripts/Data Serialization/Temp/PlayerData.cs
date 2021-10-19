using System;

[Serializable]
public class PlayerData
{
	public float[] position;

	public PlayerData( float[] position )
	{
		this.position = position;
	}
}
