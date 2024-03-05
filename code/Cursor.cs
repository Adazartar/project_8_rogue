using Sandbox;

public sealed class Cursor : Component
{
	[Property] GameObject player = null;

	protected override void OnStart()
	{

	}
	protected override void OnUpdate()
	{
        updateCursor();
    }

	private void updateCursor()
	{
		Transform.Position = new Vector3(-Mouse.Position.y, -Mouse.Position.x, 50) + player.Transform.Position;
	}

}