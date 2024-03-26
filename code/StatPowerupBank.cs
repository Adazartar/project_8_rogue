using Sandbox;
using System;

public sealed class StatPowerupBank : Component
{
    Random rand = new Random();
    List<StatPowerup> common_powerups = new List<StatPowerup>();
    List<StatPowerup> uncommon_powerups = new List<StatPowerup>();
    List<StatPowerup> rare_powerups = new List<StatPowerup>();
    List<StatPowerup> epic_powerups = new List<StatPowerup>();
    List<StatPowerup> legendary_powerups = new List<StatPowerup>();

    [Property] float x_limit = 100;
    [Property] float y_limit = 100;
    [Property] float spawn_gap = 1;
    [Property] float box_health = 10;

    [Property] Pool pickup_pool = null;

    float timer;

    protected override void OnUpdate(){
        timer -= Time.Delta;
        updateSpawner();
    }

    public StatPowerup getItem(){
        int random_num = rand.Next(1,101);
        StatPowerup new_powerup = new StatPowerup("none",["player_luck"], [0], [1], "N");
        if(random_num < 4){
            new_powerup = legendary_powerups[rand.Next(legendary_powerups.Count)];
        }
        else if(random_num < 10){
            new_powerup = epic_powerups[rand.Next(epic_powerups.Count)];
        }
        else if(random_num < 20){
            new_powerup = rare_powerups[rand.Next(rare_powerups.Count)];
        }
        else if(random_num < 41){
            new_powerup = uncommon_powerups[rand.Next(uncommon_powerups.Count)];
        }
        else if(random_num < 83){
            new_powerup = common_powerups[rand.Next(common_powerups.Count)];
        }
        else{
            Log.Info("No powerup");
        }
        return new_powerup;
    }


    protected override void OnStart(){
        common_powerups.Add(new StatPowerup("damage up", ["bullet_damage"], [0.2f], [1], "C"));

        uncommon_powerups.Add(new StatPowerup("damage up", ["bullet_damage"], [0.4f], [1], "U"));

        rare_powerups.Add(new StatPowerup("damage up", ["bullet_damage"], [0.6f], [1], "R"));

        epic_powerups.Add(new StatPowerup("damage and firerate up", ["bullet_damage", "bullet_fire_rate"], [0.6f, 0.2f], [1, 1], "E"));

        legendary_powerups.Add(new StatPowerup("cannon", ["bullet_damage", "bullet_fire_rate"], [0, 0], [1.4f, 0.8f], "L"));

    }

    public void updateSpawner(){
        if(timer < 0){
            timer = spawn_gap;
            int neg_x = rand.Next(0,2);
            int neg_y = rand.Next(0,2);
            int x_sign = (neg_x == 0) ? 1 : -1;
            int y_sign = (neg_y == 0) ? 1 : -1;
            float x_coord = (float)rand.NextDouble() * x_limit * x_sign;
            float y_coord = (float)rand.NextDouble() * y_limit * y_sign;

            Vector3 spawn_loc = new Vector3(x_coord, y_coord, 0);
            GameObject new_pickup = pickup_pool.getObject();
            
            new_pickup.SetParent(GameObject, true);
            
            StatPowerup new_pickup_stat = new_pickup.Components.Get<StatPowerup>();
            new_pickup_stat.setStatPowerup(getItem());

            new_pickup_stat.initialiseBox(box_health, pickup_pool);
            
			new_pickup.Transform.Position = spawn_loc;
        }
    }
}