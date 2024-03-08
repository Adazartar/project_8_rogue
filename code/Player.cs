using Sandbox;

public sealed class Player : Component
{
    // Debug tools
    [Property] bool controllable = true;

    // Player Stats
    Statbook statbook;
    [Property] float speed = 10f;

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

    // Attacking components
    FireProjectile projectile_shooter = null;
    
    protected override void OnStart(){
        Log.Info("player alive");
        statbook = GameObject.Components.Get<Statbook>();
        statbook.initialiseStats();


        // Set attacking components
        projectile_shooter = GameObject.Components.Get<FireProjectile>();
        projectile_shooter.refreshFireProjectile(statbook, projectile_pool);

        // Set timers
        dash_timer = dash_cooldown;

        
    }

    protected override void OnUpdate()
    {
        if(controllable){
            updateMoveInput();
            updateMove();
            updateDash();
            updateAim();
            updateAttack();
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
}