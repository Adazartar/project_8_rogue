using Sandbox;

public sealed class Player : Component
{
    // Debug tools
    [Property] bool controllable = true;

    // Player Stats
    Statbook statbook;
    [Property] float speed = 10f;

    // Interacting Helpers
    [Property] float interact_cooldown = 0.3f;
	[Property] float interact_range = 100;
	float interact_timer;
    [Property] GameObject pickups = null;
    Pool pickup_pool;

    // Vectors
    Vector3 vel = Vector3.Zero;
    Vector3 aim = Vector3.Zero;
    Vector3 dash_target =  Vector3.Zero;
    
    // GameObjects
    [Property] GameObject cursor = null;
    
    // Dash Items
    [Property] float dash_distance = 100f;
    [Property] float dash_cooldown = 0.2f;
    [Property] float dash_speed = 10f;
    [Property] float dash_duration = 0.2f;
    bool in_dash = false;
    
    // Timers
    float dash_timer;
    float in_dash_timer;

    // Attack pools
    [Property] Pool projectile_pool = null;

    // Player components
    FireProjectile projectile_shooter = null;
    Health health = null;
    NearbyObjects nearby_object_handler = null;
    
    protected override void OnStart(){
        Log.Info("player alive");
        statbook = GameObject.Components.Get<Statbook>();
        statbook.initialiseStats();

        // Set misc components
        nearby_object_handler = GameObject.Components.Get<NearbyObjects>();

        // Set health component
        health = GameObject.Components.Get<Health>();
        health.updateHealthStats(statbook);
        health.fullHeal();

        // Set attacking components
        projectile_shooter = GameObject.Components.Get<FireProjectile>();
        projectile_shooter.refreshFireProjectile(statbook, projectile_pool);

        // Set timers
        dash_timer = dash_cooldown;

        // Set interacting components
        pickup_pool = pickups.Components.Get<Pool>();

        
    }

    protected override void OnUpdate()
    {
        if(controllable){
            updateMoveInput();
            updateMove();
            updateDash();
            updateAim();
            updateAttack();
            updateInteract();
        }

        // Increment timers
        dash_timer -= Time.Delta;
        in_dash_timer -= Time.Delta;
    }

    private void updateMove()
    {
        if(in_dash){
            dashMovement();
        }
        else{
            Transform.Position += vel * Time.Delta * speed * 100;
        }
    }

    private void updateMoveInput()
	{
		vel = Vector3.Zero;
		
        if(Input.Down("forward")){
            vel += new Vector3(1, 0, 0);
        }
        if(Input.Down("backward")){
            vel += new Vector3(-1,0, 0);
        }
        if(Input.Down("left")){
            vel += new Vector3(0, 1, 0);
        }
        if(Input.Down("right")){
            vel += new Vector3(0, -1, 0);
        }
		
	}

    private void updateDash()
    {
        if(Input.Down("dash") && dash_timer < 0 && !in_dash){
            dash_timer = dash_cooldown;
            in_dash_timer = dash_duration;
            dash_target = Transform.Position + aim * dash_distance * 10;
            in_dash = true;
        }

        if(in_dash_timer < 0){
            dash_target = Vector3.Zero;
            in_dash = false;
        }
    }

    private void updateAim()
    {
        var pos = cursor.Transform.Position - Transform.Position;
        aim = new Vector3(pos.x, pos.y, 0).Normal;
    }

    private void dashMovement()
    {
        Transform.Position = Vector3.Lerp(Transform.Position, dash_target, Time.Delta * dash_speed, true);
    }

    private void updateAttack()
    {
        if(Input.Down("attack")){
            projectile_shooter.fire(aim, GameObject);
        }
    }

    public void updateInteract(){
		interact_timer -= Time.Delta;
		if(interact_timer < 0 && Input.Down("use")){
			interact_timer = interact_cooldown;
			List<GameObject> nearby = nearby_object_handler.getNearbyObjects(pickups.Children, interact_range);
			interactWithClosest(nearby);
		}
	}

    public void interactWithClosest(List<GameObject> nearby)
	{
		GameObject closestInteractable = null;
        float closestDistance = 9999;
        foreach (var interactable_object in nearby){
			if(interactable_object.Enabled == true){
				float distance = Transform.Position.Distance(interactable_object.Transform.Position);
				if (distance < closestDistance){
					closestInteractable = interactable_object;
					closestDistance = distance;
				}
			}
        }

        if (closestInteractable != null){
            closestInteractable.Components.Get<StatPowerup>().interact();
            closestInteractable.Parent = null;
            pickup_pool.returnObject(closestInteractable);
			closestInteractable.Enabled = false;
        }
	}
}