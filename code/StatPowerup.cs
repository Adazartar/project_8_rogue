using Sandbox;

public sealed class StatPowerup : Component
{
    public string name;
    public string[] stat_name;
    public float[] additive;
    public float[] multiplier;
    public string rarity;
    public bool interactable = false;

    GameObject box;
    GameObject item;
    Health health;
    Pool pool;
    protected override void OnStart(){
        
    }

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

    public void interact()
    {
        if(interactable){
            Log.Info("picked up " + name);
            pool.returnObject(GameObject);
        }
    }

    public void destroy()
    {
        Log.Info(rarity);
        if(rarity == "N"){
            pool.returnObject(GameObject);
        }
        else{
            interactable = true;
        
            box.Enabled = false;
            item.Enabled = true;
            setItemColor();
        }
        
    }

    public void initialiseBox(float in_health, Pool in_pool)
    {
        setChildren();
        interactable = false;
        box.Enabled = true;
        item.Enabled = false;

        health = box.Components.Get<Health>();
        health.alive = true;
        health.setBoxHealth(in_health);
        health.fullHeal();

        pool = in_pool;
    }

    public void setChildren()
    {
        foreach(var child in GameObject.Children){
            if(child.Tags.Has("powerup")){
                item = child;
            }
            else if(child.Tags.Has("box")){
                box = child;
            }
            else{
                Log.Info("unknown child found on powerup");
            }
        }
    }

    public void setItemColor(){
        // Try turning alpha to 1
        ModelRenderer model = item.Components.Get<ModelRenderer>();
        Color color = new Color();
        if(rarity == "C"){
            color.r = 139/255;
            color.g = 69/255;
            color.b = 19/255;
        }
        else if(rarity == "U"){
            color.r = 85/255;
            color.g = 107/255;
            color.b = 47/255;
        }
        else if(rarity == "R"){
            color.r = 72/255;
            color.g = 61/255;
            color.b = 139/255;
        }
        else if(rarity == "E"){
            color.r = 75/255;
            color.g = 0/255;
            color.b = 130/255;
        }
        else if(rarity == "L"){
            color.r = 255/255;
            color.g = 140/255;
            color.b = 0/255;
        }
        else{
            Log.Info("unknown rarity");
            color.r = 1;
            color.g = 1;
            color.b = 1;
        }
        model.Tint = color;
    }

}




