using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureInfo
{
    public class ConfirmViewCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "spconfirm";

        public string Help => "Confirm to view Steam Profile";

        public string Syntax => "/spconfirm";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string> { "archinfo" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            var player = (UnturnedPlayer)caller;
            CSteamID sid = CSteamID.Nil;
            if (ProfileViewManager.PullViewPotential(player, ref sid))
            {
                player.Player.sendBrowserRequest("SteamProfile", string.Format("http://steamcommunity.com/profiles/{0}", sid.ToString()));
            }
            else
            {
                UnturnedChat.Say(caller, "你没有待查看的Steam档案！");
            }
        }
    }
}
