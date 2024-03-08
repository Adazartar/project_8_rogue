using Sandbox;
public sealed class Statbook : Component 
{
    private Dictionary<string, Stat> statsDictionary = new Dictionary<string, Stat>();
    private Dictionary<string, bool> boolsDictionary = new Dictionary<string, bool>();
    // Bullet stats
    [Property] float base_bullet_fire_rate = 10f;
    [Property] float base_bullet_fire_rate_multi = 1f;
    [Property] float min_bullet_fire_rate = 0f;
    [Property] float max_bullet_fire_rate = 100f;

    [Property] float base_bullet_speed = 10f;
    [Property] float base_bullet_speed_multi = 1f;
    [Property] float min_bullet_speed = 0f;
    [Property] float max_bullet_speed = 100f;

    [Property] float base_bullet_range = 30f;
    [Property] float base_bullet_range_multi = 1f;
    [Property] float min_bullet_range = 0f;
    [Property] float max_bullet_range = 100f;

    [Property] float base_bullet_accuracy = 1f;
    [Property] float base_bullet_accuracy_multi = 1f;
    [Property] float min_bullet_accuracy = 0f;
    [Property] float max_bullet_accuracy = 100f;

    [Property] float base_bullet_count = 1f;
    [Property] float base_bullet_count_multi = 1f;
    [Property] float min_bullet_count = 0f;
    [Property] float max_bullet_count = 100f;

    [Property] float base_bullet_damage = 5f;
    [Property] float base_bullet_damage_multi = 1f;
    [Property] float min_bullet_damage = 0f;
    [Property] float max_bullet_damage = 100f;

    [Property] float base_bullet_size = 1f;
    [Property] float base_bullet_size_multi = 1f;
    [Property] float min_bullet_size = 0f;
    [Property] float max_bullet_size = 100f;

    [Property] bool base_bullet_piercing = false;
    // On end effect (explode, bullet scatter)
    // Status effect (fire, poison, slow)
    // Type (lazer, shoot and come back weapon, point and click attack, follow cursor weapon)
    // Chance bullets - likelihood of firing these scale with luck (stun, weaken, elemental effects)
    // Shape (triangle, square)

    // Player stats
    [Property] float base_player_dash_speed = 20f;
    [Property] float base_player_dash_speed_multi = 1f;
    [Property] float min_player_dash_speed = 0f;
    [Property] float max_player_dash_speed = 100f;

    [Property] float base_player_dash_range = 40f;
    [Property] float base_player_dash_range_multi = 1f;
    [Property] float min_player_dash_range = 0f;
    [Property] float max_player_dash_range = 100f;

    [Property] float base_player_dash_cooldown = 0.1f;
    [Property] float base_player_dash_cooldown_multi = 1f;
    [Property] float min_player_dash_cooldown = 0f;
    [Property] float max_player_dash_cooldown = 100f;

    [Property] float base_player_speed = 10f;
    [Property] float base_player_speed_multi = 1f;
    [Property] float min_player_speed = 0f;
    [Property] float max_player_speed = 100f;

    [Property] float base_player_max_redhealth = 20f;
    [Property] float base_player_max_redhealth_multi = 1f;
    [Property] float min_player_max_redhealth = 0f;
    [Property] float max_player_max_redhealth = 100f;

    [Property] float base_player_max_armour = 0f;
    [Property] float base_player_max_armour_multi = 1f;
    [Property] float min_player_max_armour = 0f;
    [Property] float max_player_max_armour = 100f;

    [Property] float base_player_max_whitehealth = 0f;
    [Property] float base_player_max_whitehealth_multi = 1f;
    [Property] float min_player_max_whitehealth = 0f;
    [Property] float max_player_max_whitehealth = 100f;

    [Property] float base_player_max_blackhealth = 0f;
    [Property] float base_player_max_blackhealth_multi = 1f;
    [Property] float min_player_max_blackhealth = 0f;
    [Property] float max_player_max_blackhealth = 100f;

    [Property] float base_player_regeneration = 2f;
    [Property] float base_player_regeneration_multi = 1f;
    [Property] float min_player_regeneration = 0f;
    [Property] float max_player_regeneration = 100f;

    [Property] float base_player_body_damage = 2f;
    [Property] float base_player_body_damage_multi = 1f;
    [Property] float min_player_body_damage = 0f;
    [Property] float max_player_body_damage = 100f;

    [Property] float base_player_luck = 2f;
    [Property] float base_player_luck_multi = 1f;
    [Property] float min_player_luck = 0f;
    [Property] float max_player_luck = 100f;

    [Property] float base_player_alignment = 0f;
    [Property] float base_player_alignment_multi = 1f;
    [Property] float min_player_alignment = 0f;
    [Property] float max_player_alignment = 100f;

    // Special effects on space (invisibility, invincibility, throw bomb - default)
    // Orbitals
    // Pets
    // On take damage effects
    public Statbook()
    {
        statsDictionary.Add("bullet_fire_rate", new Stat());
        statsDictionary.Add("bullet_speed", new Stat());
        statsDictionary.Add("bullet_range", new Stat());
        statsDictionary.Add("bullet_accuracy", new Stat());
        statsDictionary.Add("bullet_count", new Stat());
        statsDictionary.Add("bullet_damage", new Stat());
        statsDictionary.Add("bullet_size", new Stat());
        statsDictionary.Add("player_dash_speed", new Stat());
        statsDictionary.Add("player_dash_range", new Stat());
        statsDictionary.Add("player_dash_cooldown", new Stat());
        statsDictionary.Add("player_speed", new Stat());
        statsDictionary.Add("player_max_redhealth", new Stat());
        statsDictionary.Add("player_max_armour", new Stat());
        statsDictionary.Add("player_max_whitehealth", new Stat());
        statsDictionary.Add("player_max_blackhealth", new Stat());
        statsDictionary.Add("player_regeneration", new Stat());
        statsDictionary.Add("player_body_damage", new Stat());
        statsDictionary.Add("player_luck", new Stat());
        statsDictionary.Add("player_alignment", new Stat());

        boolsDictionary.Add("bullet_piercing", false);
    }
    protected override void OnStart()
    {

    }

    public void initialiseStats()
    {
        statsDictionary["bullet_fire_rate"].min = min_bullet_fire_rate;
        statsDictionary["bullet_fire_rate"].max = max_bullet_fire_rate;
        statsDictionary["bullet_fire_rate"].setBase(base_bullet_fire_rate_multi, base_bullet_fire_rate);

        statsDictionary["bullet_speed"].min = min_bullet_speed;
        statsDictionary["bullet_speed"].max = max_bullet_speed;
        statsDictionary["bullet_speed"].setBase(base_bullet_speed_multi, base_bullet_speed);

        statsDictionary["bullet_range"].min = min_bullet_range;
        statsDictionary["bullet_range"].max = max_bullet_range;
        statsDictionary["bullet_range"].setBase(base_bullet_range_multi, base_bullet_range);

        statsDictionary["bullet_accuracy"].min = min_bullet_accuracy;
        statsDictionary["bullet_accuracy"].max = max_bullet_accuracy;
        statsDictionary["bullet_accuracy"].setBase(base_bullet_accuracy_multi, base_bullet_accuracy);

        statsDictionary["bullet_count"].min = min_bullet_count;
        statsDictionary["bullet_count"].max = max_bullet_count;
        statsDictionary["bullet_count"].setBase(base_bullet_count_multi, base_bullet_count);

        statsDictionary["bullet_damage"].min = min_bullet_damage;
        statsDictionary["bullet_damage"].max = max_bullet_damage;
        statsDictionary["bullet_damage"].setBase(base_bullet_damage_multi, base_bullet_damage);

        statsDictionary["bullet_size"].min = min_bullet_size;
        statsDictionary["bullet_size"].max = max_bullet_size;
        statsDictionary["bullet_size"].setBase(base_bullet_size_multi, base_bullet_size);

        statsDictionary["player_dash_speed"].min = min_player_dash_speed;
        statsDictionary["player_dash_speed"].max = max_player_dash_speed;
        statsDictionary["player_dash_speed"].setBase(base_player_dash_speed_multi, base_player_dash_speed);

        statsDictionary["player_dash_range"].min = min_player_dash_range;
        statsDictionary["player_dash_range"].max = max_player_dash_range;
        statsDictionary["player_dash_range"].setBase(base_player_dash_range_multi, base_player_dash_range);

        statsDictionary["player_dash_cooldown"].min = min_player_dash_cooldown;
        statsDictionary["player_dash_cooldown"].max = max_player_dash_cooldown;
        statsDictionary["player_dash_cooldown"].setBase(base_player_dash_cooldown_multi, base_player_dash_cooldown);

        statsDictionary["player_speed"].min = min_player_speed;
        statsDictionary["player_speed"].max = max_player_speed;
        statsDictionary["player_speed"].setBase(base_player_speed_multi, base_player_speed);

        statsDictionary["player_max_redhealth"].min = min_player_max_redhealth;
        statsDictionary["player_max_redhealth"].max = max_player_max_redhealth;
        statsDictionary["player_max_redhealth"].setBase(base_player_max_redhealth_multi, base_player_max_redhealth);

        statsDictionary["player_max_armour"].min = min_player_max_armour;
        statsDictionary["player_max_armour"].max = max_player_max_armour;
        statsDictionary["player_max_armour"].setBase(base_player_max_armour_multi, base_player_max_armour);

        statsDictionary["player_max_whitehealth"].min = min_player_max_whitehealth;
        statsDictionary["player_max_whitehealth"].max = max_player_max_whitehealth;
        statsDictionary["player_max_whitehealth"].setBase(base_player_max_whitehealth_multi, base_player_max_whitehealth);

        statsDictionary["player_max_blackhealth"].min = min_player_max_blackhealth;
        statsDictionary["player_max_blackhealth"].max = max_player_max_blackhealth;
        statsDictionary["player_max_blackhealth"].setBase(base_player_max_blackhealth_multi, base_player_max_blackhealth);

        statsDictionary["player_regeneration"].min = min_player_regeneration;
        statsDictionary["player_regeneration"].max = max_player_regeneration;
        statsDictionary["player_regeneration"].setBase(base_player_regeneration_multi, base_player_regeneration);

        statsDictionary["player_body_damage"].min = min_player_body_damage;
        statsDictionary["player_body_damage"].max = max_player_body_damage;
        statsDictionary["player_body_damage"].setBase(base_player_body_damage_multi, base_player_body_damage);

        statsDictionary["player_luck"].min = min_player_luck;
        statsDictionary["player_luck"].max = max_player_luck;
        statsDictionary["player_luck"].setBase(base_player_luck_multi, base_player_luck);

        statsDictionary["player_alignment"].min = min_player_alignment;
        statsDictionary["player_alignment"].max = max_player_alignment;
        statsDictionary["player_alignment"].setBase(base_player_alignment_multi, base_player_alignment);


        boolsDictionary["bullet_piercing"] = base_bullet_piercing;
    }

    public Stat getStat(string stat_name)
    {
        if(statsDictionary.ContainsKey(stat_name)){
            return statsDictionary[stat_name];
        }
        else{
            return null;
        }
    }

}