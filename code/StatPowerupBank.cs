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

    public StatPowerup getItem(){
        int random_num = rand.Next(1,101);
        StatPowerup new_powerup = new StatPowerup("none",["player_luck"], [0], [1]);
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
        common_powerups.Add(new StatPowerup("damage up", ["bullet_damage"], [0.2f], [1]));

        uncommon_powerups.Add(new StatPowerup("damage up", ["bullet_damage"], [0.4f], [1]));

        rare_powerups.Add(new StatPowerup("damage up", ["bullet_damage"], [0.6f], [1]));

        epic_powerups.Add(new StatPowerup("damage and firerate up", ["bullet_damage", "bullet_fire_rate"], [0.6f, 0.2f], [1, 1]));

        legendary_powerups.Add(new StatPowerup("cannon", ["bullet_damage", "bullet_fire_rate"], [0, 0], [1.4f, 0.8f]));

    }
}