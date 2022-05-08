using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peglin.BepInEx
{
    class MyAttribute
    {
        public const string PLAGIN_NAME = "PeglinBepInEx";
        public const string PLAGIN_VERSION = "22.5.9";
        public const string PLAGIN_FULL_NAME = "Peglin.PeglinBepInEx.plugin";
    }

    [BepInPlugin(MyAttribute.PLAGIN_FULL_NAME, MyAttribute.PLAGIN_NAME, MyAttribute.PLAGIN_VERSION)]// 버전 규칙 잇음. 반드시 2~4개의 숫자구성으로 해야함. 미준수시 못읽어들임
    public class PeglinBepInEx : BaseUnityPlugin
    {
        public static ManualLogSource log;
        public static Harmony harmony;

        public void Awake()
        {
            log = Logger;
            log.LogMessage("Awake");
            harmony = Harmony.CreateAndPatchAll(typeof(PeglinBepInEx));
        }

        public void OnEnable()
        {
            log.LogMessage("OnEnable");
            // 하모니 패치
            //harmony = Harmony.CreateAndPatchAll(typeof(PeglinBepInEx));
        }

        // 이 게임에선 강제 비활성화??
        public void OnDisable()
        {
            log.LogMessage("OnDisable");
            //harmony?.UnpatchSelf();// ==harmony.UnpatchAll(harmony.Id);
            //harmony.UnpatchAll(); // 정대 사용 금지. 다름 플러그인이 패치한것까지 다 풀려버림

        }

        [HarmonyPrefix, HarmonyPatch(typeof(FloatVariable), "Subtract")]
        // public float Add(float val)
        public static bool Prefix(FloatVariable __instance, ref float __result, float val)
        {
            log.LogMessage($"Prefix {__instance.Value} , {val}");
            __result = __instance.Value;
            return false;
        }

    }
}
