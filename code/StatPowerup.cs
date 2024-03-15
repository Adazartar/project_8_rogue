using Sandbox;

public sealed class StatPowerup : Component
{
    public string name;
    public string[] stat_name;
    public float[] additive;
    public float[] multiplier;

    public StatPowerup(string in_name, string[] in_stat_name, float[] in_additive, float[] in_multiplier){
        name = in_name;
        stat_name = in_stat_name;
        additive = in_additive;
        multiplier = in_multiplier;
    }

}




