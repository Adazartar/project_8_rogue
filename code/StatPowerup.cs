using Sandbox;

public sealed class StatPowerup : Component
{
    public string name;
    public string[] stat_name;
    public float[] additive;
    public float[] multiplier;
    public string rarity;

    protected override void OnUpdate(){

    }
    
    public StatPowerup(){}

    public StatPowerup(string in_name, string[] in_stat_name, float[] in_additive, float[] in_multiplier, string in_rarity){
        name = in_name;
        stat_name = in_stat_name;
        additive = in_additive;
        multiplier = in_multiplier;
        rarity = in_rarity;
    }

    public void setStatPowerup(StatPowerup in_stat){
        name = in_stat.name;
        stat_name = in_stat.stat_name;
        additive = in_stat.additive;
        multiplier = in_stat.multiplier;
        rarity = in_stat.rarity;
    }

    public void interact(){
        Log.Info("picked up " +name);
    }

}




