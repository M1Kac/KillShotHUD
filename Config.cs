using CounterStrikeSharp.API.Core;

namespace KillShotHUD
{
    public class Config : BasePluginConfig
    {
        public bool MessageBackground { get; set; } = true; 
        public string HeadshotColor { get; set; } = "Aqua"; 
        public string ChestShotColor { get; set; } = "Yellow";
        public string StomachShotColor { get; set; } = "Yellow";
        public string ArmShotColor { get; set; } = "Lime";
        public string LegShotColor { get; set; } = "White"; 
        public string DefaultKillColor { get; set; } = "White"; 

        public string HeadshotMessage { get; set; } = "HEADSHOT!";
        public string ChestShotMessage { get; set; } = "CHEST SHOT!";
        public string StomachShotMessage { get; set; } = "STOMACH SHOT!";
        public string LeftArmShotMessage { get; set; } = "LEFT ARM SHOT!";
        public string RightArmShotMessage { get; set; } = "RIGHT ARM SHOT!";
        public string LeftLegShotMessage { get; set; } = "LEFT LEG SHOT!";
        public string RightLegShotMessage { get; set; } = "RIGHT LEG SHOT!";
        public string DefaultKillMessage { get; set; } = "KILL!";
    }
}
