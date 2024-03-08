using Sandbox;

public sealed class FireProjectile : Component
{
	Statbook stats;
	float next_shot_interval;
	float next_shot_timer;
	Pool projectile_pool;

	public void refreshFireProjectile(Statbook input_stats, Pool pool)
	{
		stats = input_stats;
		next_shot_interval = 1 / stats.getStat("bullet_fire_rate").actual_stat;
		next_shot_timer = next_shot_interval;
		projectile_pool = pool;
	}

	protected override void OnUpdate()
	{
		next_shot_timer -= Time.Delta;
	}

	public void fire(Vector3 aim, GameObject self)
	{
		if(next_shot_timer < 0){
			next_shot_timer = next_shot_interval;
			GameObject new_projectile = projectile_pool.getObject();
			new_projectile.Transform.Position = Transform.Position;
			new_projectile.Components.Get<Projectile>().defineProjectile(stats, aim, projectile_pool, self);
		}
	}

}