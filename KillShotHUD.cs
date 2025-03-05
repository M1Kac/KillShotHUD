using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Entities;
using CounterStrikeSharp.API.Modules.Utils;
using CS2_GameHUDAPI;
using CounterStrikeSharp.API.Core.Capabilities;
using System;
using System.Drawing;

namespace KillShotHUD
{
    public class KillShotHUD : BasePlugin, IPluginConfig<Config>
    {
        public override string ModuleName => "KillShot HUD Indicator";
        public override string ModuleAuthor => "M1K@c";
        public override string ModuleVersion => "1.0.2";
        public override string ModuleDescription => "Displays a HUD message based on where a player landed a killshot.";

        private static IGameHUDAPI? _api;
        public required Config Config { get; set; }

        public void OnConfigParsed(Config config)
        {
            Config = config;
        }

        public override void OnAllPluginsLoaded(bool hotReload)
        {
            try
            {
                PluginCapability<IGameHUDAPI> CapabilityCP = new("gamehud:api");
                _api = IGameHUDAPI.Capability.Get();

                if (_api == null)
                {
                    PrintToConsole("GameHUDAPI not found. Make sure CS2-GameHUD is installed!");
                }
            }
            catch (Exception)
            {
                _api = null;
                PrintToConsole("Error loading GameHUDAPI.");
            }
        }

        [GameEventHandler]
        public HookResult OnPlayerDeath(EventPlayerDeath ev, GameEventInfo info)
        {
            if (ev.Attacker == null || ev.Userid == null || !ev.Attacker.IsValid)
                return HookResult.Continue;

            var attacker = ev.Attacker;
            if (attacker == null || !attacker.IsValid || _api == null) return HookResult.Continue;

            // Determine the hit location
            string message = Config.DefaultKillMessage;
            Color messageColor = Color.White;
            Vector position = new Vector(15, 10, 80);

            switch (ev.Hitgroup)
            {
                case 1:
                    message = Config.HeadshotMessage;
                    messageColor = Color.FromName(Config.HeadshotColor);
                    position = new Vector(20, 10, 100);
                    break;
                case 2:
                    message = Config.ChestShotMessage;
                    messageColor = Color.FromName(Config.ChestShotColor);
                    position = new Vector(15, 15, 100);
                    break;
                case 3:
                    message = Config.StomachShotMessage;
                    messageColor = Color.FromName(Config.StomachShotColor);
                    position = new Vector(10, 20, 85);
                    break;
                case 4:
                    message = Config.LeftArmShotMessage;
                    messageColor = Color.FromName(Config.ArmShotColor);
                    position = new Vector(5, 25, 100);
                    break;
                case 5:
                    message = Config.RightArmShotMessage;
                    messageColor = Color.FromName(Config.ArmShotColor);
                    position = new Vector(25, 5, 100);
                    break;
                case 6:
                    message = Config.LeftLegShotMessage;
                    messageColor = Color.FromName(Config.LegShotColor);
                    position = new Vector(10, 30, 100);
                    break;
                case 7:
                    message = Config.RightLegShotMessage;
                    messageColor = Color.FromName(Config.LegShotColor);
                    position = new Vector(30, 10, 100);
                    break;
                default:
                    messageColor = Color.FromName(Config.DefaultKillColor);
                    position = new Vector(15, 10, 100);
                    break;
            }

            // Display the HUD message
            if (Config.MessageBackground)
            {
                _api.Native_GameHUD_SetParams(attacker, 2, position, messageColor, 40, "Arial Bold", 0.10f, PointWorldTextJustifyHorizontal_t.POINT_WORLD_TEXT_JUSTIFY_HORIZONTAL_CENTER, PointWorldTextJustifyVertical_t.POINT_WORLD_TEXT_JUSTIFY_VERTICAL_BOTTOM, PointWorldTextReorientMode_t.POINT_WORLD_TEXT_REORIENT_NONE, 0.6f, 1.3f);
            }
            else
                 _api.Native_GameHUD_SetParams(attacker, 2, position, messageColor, 40, "Arial Bold", 0.10f);

            _api.Native_GameHUD_Show(attacker, 2, message, 2.0f);

            return HookResult.Continue;
        }

        public static void PrintToConsole(string sMessage)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[KillShotHUD] " + sMessage);
            Console.ResetColor();
        }
    }
}