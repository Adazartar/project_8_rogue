using Sandbox;
public sealed class Stat : Component 
{
    private float min { get; set; }
    private float max { get; set; }
    private List<float> multipliers = new List<float>();
    private List<float> additives = new List<float>();

    private float actual_stat { get; set; }

    protected override void OnStart()
    {

    }

    public void addMultiplier(float multiplier)
    {
        multipliers.Add(multiplier);
        calc_actual_stat();
    }

    public void addAdditive(float additive)
    {
        additives.Add(additive);
        calc_actual_stat();
    }

    public void setBase(float multipler, float additive)
    {
        addMultiplier(multipler);
        addAdditive(additive);
    }

    private float calc_actual_stat()
    {
        float stat = 0;
        foreach(var additive in additives){
            stat += additive;
        }
        foreach(var multipler in multipliers){
            stat *= multipler;
        }
        if(stat < min) { stat = min; }
        if(stat > max) { stat = max; }

        actual_stat = stat;
        return stat;
    }



}
