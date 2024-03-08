using Sandbox;

public sealed class Health : Component
{
	bool alive = true;
	[Property] Statbook stats = null;
	
	float current_redhealth;
	float max_redhealth;
	float current_armour;
	float max_armour;
	float current_whitehealth;
	float max_whitehealth;
	float current_blackhealth;
	float max_blackhealth;

	protected override void OnStart()
	{
		updateHealthStats();
		fullHeal();
	}
	protected override void OnUpdate()
	{

	}

	public void handleHit(Statbook bullet_stats, bool is_hit)
	{
		if(is_hit){
			// Apply damage since this means it is this player that got hit
		}
		else {
			// Apply lifesteal/whatever since this means it isn't this player that got hit
		}
	}

	public void updateHealthStats()
	{
		max_redhealth = stats.getStat("player_max_redhealth").actual_stat;
		max_armour = stats.getStat("player_max_armour").actual_stat;
		max_whitehealth = stats.getStat("player_max_whitehealth").actual_stat;
		max_blackhealth = stats.getStat("player_max_blackhealth").actual_stat;
	}

	public void fullHeal()
	{
		current_redhealth = max_redhealth;
		current_armour = max_armour;
		current_whitehealth = max_whitehealth;
		current_blackhealth = max_blackhealth;
	}

	public void checkAlive()
	{
		float current_health = current_redhealth + current_armour + current_whitehealth + current_blackhealth;
		if(current_health <= 0){
			alive = false;
		}
	}

}