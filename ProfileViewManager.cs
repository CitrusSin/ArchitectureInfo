using Rocket.Unturned.Player;
using Steamworks;
using System.Collections.Generic;

namespace ArchitectureInfo
{
    public class ProfileViewManager
    {
        private static Dictionary<UnturnedPlayer, CSteamID> sidViewMap = new Dictionary<UnturnedPlayer, CSteamID>();

        public static void SetViewPotential(UnturnedPlayer player, CSteamID targetSid)
        {
            if (sidViewMap.ContainsKey(player))
            {
                sidViewMap[player] = targetSid;
            }
            else
            {
                sidViewMap.Add(player, targetSid);
            }
        }

        public static bool PullViewPotential(UnturnedPlayer player, ref CSteamID outputSid)
        {
            if (sidViewMap.ContainsKey(player) && sidViewMap[player] != null)
            {
                outputSid = sidViewMap[player];
                return true;
            }
            return false;
        }
    }
}
