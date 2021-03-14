using HarmonyLib;
using Reactor;
using System;
using System.Collections.Generic;
using System.Text;

namespace DistortedComms
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public static class HudManagerStartPatch
    {
        public static void Postfix(HudManager __instance)
        {
            DistortedCommsPlugin plugin = PluginSingleton<DistortedCommsPlugin>.Instance;

            if (AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.NotJoined || AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Ended)
            {
                plugin.DeactivateComms();
                return;
            }
            bool commsActive = false;

            foreach (PlayerTask task in PlayerControl.LocalPlayer.myTasks)
            {
                if (task.TaskType == TaskTypes.FixComms)
                {
                    commsActive = true;
                    break;
                }
            }
            if (commsActive)
                plugin.ActivateComms();
            else
                plugin.DeactivateComms();
        }
    }
}
