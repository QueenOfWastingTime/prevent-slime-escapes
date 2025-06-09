using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Netcode;
using StardewModdingAPI;
using StardewValley;

namespace PreventSlimeEscapes
{
    /// <summary>The mod entry point.</summary>
    internal sealed class ModEntry : Mod
    {
        /*********
         ** Public methods
         *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            var harmony = new Harmony(this.ModManifest.UniqueID);

            harmony.Patch(
                original: AccessTools.Method(typeof(Farm), nameof(Farm.DayUpdate)),
                transpiler: new HarmonyMethod(typeof(ModEntry), nameof(skipSlimeEsc_Transpile))
            );
        }

        public static IEnumerable<CodeInstruction> skipSlimeEsc_Transpile(IEnumerable<CodeInstruction> instructions)
        {
            CodeMatcher matcher = new(instructions);
            MethodInfo getCount =
                AccessTools.PropertyGetter(typeof(NetCollection<NPC>), nameof(NetCollection<NPC>.Count));

            matcher.MatchStartForward(
                    new CodeMatch(OpCodes.Ldarg_0),
                    new CodeMatch(CodeInstruction.LoadField(typeof(GameLocation), nameof(GameLocation.characters))),
                    new CodeMatch(OpCodes.Callvirt, getCount),
                    new CodeMatch(OpCodes.Ldc_I4_5)
                )
                .ThrowIfNotMatch($"Could not find entry point for {nameof(skipSlimeEsc_Transpile)}")
                .Advance(3);

            matcher.RemoveInstruction()
                .Insert(
                    new CodeInstruction(OpCodes.Ldc_I4, Int32.MaxValue)
                );

            return matcher.InstructionEnumeration();
        }
    }
}