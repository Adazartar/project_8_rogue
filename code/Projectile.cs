using Sandbox;

public sealed class Projectile : Component
{
	float projectile_duration;
	Vector3 projectile_direction;
	Statbook bullet_stats;
	Pool parent_pool;
	protected override void OnStart()
	{
		
	}
	protected override void OnUpdate()
	{
		projectile_duration -= Time.Delta;
		if(projectile_duration < 0){
			parent_pool.returnObject(GameObject);
		}
		Transform.Position += bullet_stats.getStat("bullet_speed").actual_stat * projectile_direction * Time.Delta * 100;
	}
	
	protected override void OnFixedUpdate()
	{
		
	}

	public void defineProjectile(Statbook stats, Vector3 direction, Pool pool)
	{
		Log.Info("projectile is being defined");
		parent_pool = pool;
		bullet_stats = stats;
		projectile_duration = stats.getStat("bullet_speed").actual_stat / stats.getStat("bullet_range").actual_stat;
		projectile_direction = direction;		
	}
}