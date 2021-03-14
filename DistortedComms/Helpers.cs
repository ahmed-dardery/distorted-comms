using Reactor;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace DistortedComms
{
    public static class Helpers
    {
        public readonly static System.Random RNG = new System.Random();
        public static Color32 ColorFromHex(string hex)
        {
            int rgba = int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            return new Color32((byte)(rgba >> 24), (byte)(rgba >> 16), (byte)(rgba >> 8), (byte)rgba);
        }
        public static Color ChangeAlphaTo(Color color, float newAlpha)
        {
            color.a = newAlpha;
            return color;
        }
        public static string GlitchedText(string name, float alpha)
        {
            if (alpha > 0.95f) return name;
            if (alpha < 0.05f) return "";

            //To ensure name becomes shorter as time goes on
            int len = Mathf.CeilToInt(alpha * name.Length);
            var sb = new StringBuilder(name.Substring(0, len));


            for (int i = 0; i < len; ++i)
            {
                //garble up random characters, probability increases as alpha decreases
                if (RNG.NextDouble() > alpha) sb[i] = "@#=+-*&^%$"[RNG.Next(0, 10)];

            }
            return sb.ToString();
        }
    }
}
