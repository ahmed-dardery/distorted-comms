using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using HarmonyLib;
using Reactor;
using System.Collections;
using System.Text;
using UnityEngine;

namespace DistortedComms
{
    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(ReactorPlugin.Id)]
    public class DistortedCommsPlugin : BasePlugin
    {
        public const string Id = "mod.dardy.distortedcomms";

        public Harmony Harmony { get; } = new Harmony(Id);
        public bool IsCommsActive { get; private set; }
        private IEnumerator LastCoroutine { get; set; }
        public float AnimationDuration { get; set; }
        public Color32 DistortionBackColor {get; set;}
        public Color32 DistortionBodyColor { get; set; }
        public override void Load()
        {
            var duration = new ConfigDefinition("Distortion", "Duration");
            var back = new ConfigDefinition("Distortion", "Back");
            var body = new ConfigDefinition("Distortion", "Body");

            Config.Bind(duration, 3.0f);
            Config.Bind(back, "7F7F7FFF");
            Config.Bind(body, "7F7F7FFF");

            AnimationDuration = (float)Config[duration].BoxedValue;
            DistortionBackColor = Helpers.ColorFromHex((string)Config[back].BoxedValue);
            DistortionBodyColor = Helpers.ColorFromHex((string)Config[body].BoxedValue);

            Reactor.Patches.ReactorVersionShower.TextUpdated += (TextRenderer text) => text.Text += "\n[" + (string)Config[body].BoxedValue + "]DistortedComms[] v1.0 by Dardy : D";
            Harmony.PatchAll();
        }
        public void ActivateComms()
        {
            if (IsCommsActive) return;
            if(LastCoroutine != null)
                Coroutines.Stop(LastCoroutine);
            IsCommsActive = true;
            Coroutines.Start(LastCoroutine = CoDistortColor(true));
        }
        public void DeactivateComms()
        {
            if (!IsCommsActive) return;
            if (LastCoroutine != null)
                Coroutines.Stop(LastCoroutine);
            IsCommsActive = false;
            Coroutines.Start(LastCoroutine = CoDistortColor(false));
        }
        public IEnumerator CoDistortColor(bool fadeOff)
        {
            for (float t = 0f; ;)
            {
                float alpha = t / AnimationDuration;
                if (fadeOff) alpha = 1 - alpha;
                alpha = Mathf.Clamp(alpha, 0f, 1f);
                
                DeadBody[] allDeadBodies = UnityEngine.Object.FindObjectsOfType<DeadBody>();
                foreach (var deadBody in allDeadBodies)
                {
                    var material = deadBody.gameObject.GetComponent<Renderer>().material;

                    //the keyword allows other mods to programmatically detect if a material needs not to be disturbed
                    if (fadeOff)
                        material.EnableKeyword("Distorted");
                    else
                        material.DisableKeyword("Distorted");

                    int colorId = GameData.Instance.GetPlayerById(deadBody.ParentId).ColorId;

                    material.SetColor("_BackColor", Color32.Lerp(DistortionBackColor, Palette.ShadowColors[colorId], alpha));
                    material.SetColor("_BodyColor", Color32.Lerp(DistortionBodyColor, Palette.PlayerColors[colorId], alpha));
                }

                foreach (var pl in PlayerControl.AllPlayerControls)
                {
                    if (fadeOff)
                        pl.myRend.material.EnableKeyword("Distorted");
                    else
                        pl.myRend.material.DisableKeyword("Distorted");

                    pl.nameText.Text = Helpers.GlitchedText(pl.gameObject.name, alpha);
                    pl.myRend.material.SetColor("_BackColor", Color32.Lerp(DistortionBackColor, Palette.ShadowColors[pl.Data.ColorId], alpha));
                    pl.myRend.material.SetColor("_BodyColor", Color32.Lerp(DistortionBodyColor, Palette.PlayerColors[pl.Data.ColorId], alpha));

                    //pets, skins and hats will be made transparent, some of them might be null.
                    var spritesList = new[]
                    {
                            pl.CurrentPet?.rend,
                            pl.CurrentPet?.shadowRend,
                            pl.MyPhysics?.Skin?.layer,
                            pl.HatRenderer?.BackLayer,
                            pl.HatRenderer?.FrontLayer
                    };
                    spritesList.DoIf(v => v != null, v => v.color = Helpers.ChangeAlphaTo(v.color, alpha));
                }
                yield return null;
                if (t < AnimationDuration) t += Time.deltaTime;
            }
        }


       
    }
}
