using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using UnityEngine;

namespace ArchitectureInfo
{
    public class InfoTeller
    {
        public static void TellInfo(UnturnedPlayer caller, BarricadeData barricade)
        {
            CSteamID sid = (CSteamID)barricade.owner;
            CSteamID gid = (CSteamID)barricade.group;

            TellSteamIDInfo(caller, "此建筑拥有者", sid);
            TellGroupInfo(caller, "此建筑拥有者", gid);
            TellVector(caller, "此建筑坐标", barricade.point);

            ProfileViewManager.SetViewPotential(caller, sid);
            UnturnedChat.Say(caller, "可使用指令/spconfirm以查看此建筑拥有者的Steam档案。");
        }

        public static void TellInfo(UnturnedPlayer caller, StructureData structure)
        {
            CSteamID sid = (CSteamID)structure.owner;
            CSteamID gid = (CSteamID)structure.group;

            TellSteamIDInfo(caller, "此建筑拥有者", sid);
            TellGroupInfo(caller, "此建筑拥有者", gid);
            TellVector(caller, "此建筑坐标", structure.point);

            ProfileViewManager.SetViewPotential(caller, sid);
            UnturnedChat.Say(caller, "可使用指令/spconfirm以查看此建筑拥有者的Steam档案。");
        }

        public static void TellInfo(UnturnedPlayer caller, InteractableVehicle vehicle)
        {
            CSteamID sid = vehicle.lockedOwner;
            CSteamID gid = vehicle.lockedGroup;

            TellSteamIDInfo(caller, "此载具上锁者", sid);
            TellGroupInfo(caller, "此载具上锁者", gid);
            TellVector(caller, "此载具坐标", vehicle.transform.position);

            ProfileViewManager.SetViewPotential(caller, sid);
            UnturnedChat.Say(caller, "可使用指令/spconfirm以查看此载具上锁者的Steam档案。");
        }

        private static void TellSteamIDInfo(IRocketPlayer caller, string prefix, CSteamID sid)
        {
            UnturnedChat.Say(caller, string.Format("{1}SteamID: {0}", sid.ToString(), prefix));
            UnturnedPlayer player = UnturnedPlayer.FromCSteamID(sid);
            if (player != null)
            {
                UnturnedChat.Say(caller, string.Format("{1}名称：{0}", player.DisplayName, prefix));
                UnturnedChat.Say(caller, string.Format("{1}此次登录IP：{0}", player.IP, prefix));
            }
            else
            {
                UnturnedChat.Say(caller, string.Format("{0}不在线，因此无法获取{0}详细信息", prefix));
            }
        }

        private static void TellGroupInfo(IRocketPlayer caller, string prefix, CSteamID gid)
        {
            UnturnedChat.Say(caller, string.Format("{1}游戏组ID: {0}", gid.ToString(), prefix));

            GroupInfo info = GroupManager.getGroupInfo(gid);
            if (info != null)
            {
                UnturnedChat.Say(caller, string.Format("{1}游戏组名称：{0}", info.name, prefix));
                UnturnedChat.Say(caller, string.Format("{1}游戏组成员人数：{0}", info.members, prefix));
            }
            else
            {
                UnturnedChat.Say(caller, string.Format("未搜索到{0}游戏组组相关信息", prefix));
            }
        }

        private static void TellVector(IRocketPlayer caller, string prefix, Vector3 vector)
        {
            UnturnedChat.Say(caller, string.Format("{3}三维分量：X={0}, Y={1}, Z={2}", vector.x, vector.y, vector.z, prefix));
        }
    }
}
