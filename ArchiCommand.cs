using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System.Collections.Generic;
using UnityEngine;

namespace ArchitectureInfo
{
    public class ArchiCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "archinfo";

        public string Help => "Show the information of a certain architecture";

        public string Syntax => "/archinfo";

        public List<string> Aliases => new List<string> { "archi" };

        public List<string> Permissions => new List<string> { "archinfo" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            var player = (UnturnedPlayer)caller;

            RaycastHit hit;
            Ray ray = new Ray(player.Player.look.aim.position, player.Player.look.aim.forward);
            bool isHit = Physics.Raycast(ray, out hit, 10f, RayMasks.BARRICADE_INTERACT);

            if (isHit)
            {
                Transform targetTransform = hit.transform;
                if (targetTransform.GetComponent<InteractableDoorHinge>() != null)
                {
                    targetTransform = targetTransform.parent.parent;
                }

                BarricadeDrop barricadeDrop = BarricadeManager.FindBarricadeByRootTransform(targetTransform);
                if (barricadeDrop != null)
                {
                    BarricadeData bd = barricadeDrop.GetServersideData();
                    InfoTeller.TellInfo(player, bd);
                    return;
                }

                StructureDrop structureDrop = StructureManager.FindStructureByRootTransform(targetTransform);
                if (structureDrop != null)
                {
                    StructureData sd = structureDrop.GetServersideData();
                    InfoTeller.TellInfo(player, sd);
                    return;
                }

                InteractableVehicle vehicle = targetTransform.GetComponent<InteractableVehicle>();
                if (vehicle != null)
                {
                    InfoTeller.TellInfo(player, vehicle);
                    return;
                }
            }
        }
    }
}
