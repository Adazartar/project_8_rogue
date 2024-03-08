using Sandbox;
public sealed class Stat : Component 
{
    public float min { get; set; }
    public float max { get; set; }
    private List<float> multipliers = new List<float>();
    private List<float> additives = new List<float>();

    public float actual_stat;

    protected override void OnStart()
    {

    }

    public void addMultiplier(float multiplier)
    {
        multipliers.Add(multiplier);
        calcActualStat();
    }

    public void addAdditive(float additive)
    {
        additives.Add(additive);
        calcActualStat();
    }

    public void setBase(float multipler, float additive)
    {
        addMultiplier(multipler);
        addAdditive(additive);
    }

    private void calcActualStat()
    {
        float stat = 0;
        foreach(var additive in additives){
            stat += additive;
        }
        foreach(var multiplier in multipliers){
            stat *= multiplier;
        }
        if(stat < min) { stat = min; }
        if(stat > max) { stat = max; }

        actual_stat = stat;
    }



}
