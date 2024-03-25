using Sandbox;

public sealed class Projectile : Component, Component.ITriggerListener
{
	float projectile_duration;
	Vector3 projectile_direction;
	Statbook bullet_stats;
	Pool parent_pool;
	GameObject parent_player;
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

	public void defineProjectile(Statbook stats, Vector3 direction, Pool pool, GameObject source)
	{
		parent_pool = pool;
		bullet_stats = stats;
		projectile_duration = stats.getStat("bullet_speed").actual_stat / stats.getStat("bullet_range").actual_stat;
		projectile_direction = direction;
		parent_player = source;	
	}

	public void OnTriggerEnter(Collider other)
	{
		var hit_target = other.GameObject;
		if(hit_target != null && hit_target.Tags.Has("hittable") && hit_target != parent_player){
			Log.Info("we have hit another player");
			hit_target.Components.Get<Health>().handleHit(bullet_stats, true);
			parent_player.Components.Get<Health>().handleHit(bullet_stats, false);
		}
	}

	public void OnTriggerExit(Collider other) {}
}