using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lerawin.Classes
{
    public class Settings
    {
        // Aim
        public bool ViewangleAimbotEnabled;
        public bool MouseAimbotEnabled;
        public bool AimEnemyEnabled;
        public bool AimTeamEnabled;
        public bool AimSilentEnabled;
        public bool AimITAEnabled;
        public bool AimTVEnabled;
        public bool AimNIAEnabled;
        public bool AimNMEnabled;
        public bool AimASEnabled;
        public bool AimScopedEnabled;
        public int AimFOVPixelDistance;
        public int AimSmooth;
        public int AimIgnoreBullets;
        public int AimNextTargetCooldown;
        public string AimBone;

        public bool TriggerEnemyEnabled;
        public bool TriggerTeamEnabled;
        public bool TriggerStickyEnabled;
        //public bool TriggerNMEnabled;
        //public bool TriggerScopedEnabled;
        public int TriggerMinFirerate;
        public int TriggerMaxFirerate;
        public int TriggerMinShotDelay;
        public int TriggerMaxShotDelay;
        public int TriggerStickyFOV;
        public int TriggerStickySmooth;

        // Visuals
        public bool GlowEnemyEnabled;
        public bool GlowTeamEnabled;
        public bool SndGlowEnemyEnabled;
        public bool SndGlowTeamEnabled;
        public bool GlowVisibleEnabled;
        public bool GlowHealthBEnabled;

        public bool RadarEnabled;
        public bool RadarOnKeyEnabled;
        public bool ChamsEnabled;
        public bool ChamsHealthBEnabled;
        public bool MapBrightnessEnabled;
        public int MapBrightnessAmount;

        public bool ESPEnemyEnabled;
        public bool ESPTeamEnabled;
        public bool SndESPEnabled;
        public bool ESPVisibleEnabled;
        public bool ESPHealthBEnabled;
        public bool RecoilCrosshairEnabled;
        public int RecoilCrosshairSize;
        public bool ESPBoxEnabled;
        public string ESPBoxType;
        public bool ESPHealthEnabled;
        public string ESPHealthType;
        public bool ESPAmmoEnabled;
        public string ESPAmmoType;
        public bool ESPBombTimerEnabled;
        public string ESPBombTimerType;
        public bool ESPDefuseTimerEnabled;
        public string ESPDefuseTimerType;
        public bool ESPDefusingEnabled;
        public bool ESPPlantingEnabled;
        public bool ESPDefuseKitOwnerEnabled;
        public bool ESPSniperCrosshairEnabled;
        public bool ESPDistanceEnabled;
        public bool ESPReloadingEnabled;
        public bool ESPArmorEnabled;
        public bool ESPWeaponEnabled;
        public bool ESPHeadEnabled;
        public bool ESPSnaplinesEnabled;
        public bool ESPC4OwnerEnabled;

        // Miscellaneous
        public bool BunnyhopEnabled;
        public bool BunnyhopVelocityEnabled;
        public bool ReduceFlashEnabled;
        public bool AutoPistolEnabled;
        public bool ThirdpersonEnabled;
        public bool AutoAcceptEnabled;
        public bool HitsoundEnabled;
        public bool HitsoundVisOnlyEnabled;
        public bool FakelagEnabled;
        public bool FOVChangerEnabled;
        public int FOVAmount;

        // Skin Changer
        public bool SCEnabled;
        public bool SCBuyZoneOnlyEnabled;
        public bool SCStatTrackEnabled;
        public bool SCRealStatTrackEnabled;
        public int SCStatTrackAmount;
        public float SCWearAmount;
        public int SCSeedAmount;
        public string SCQuality; // Doesn't load up with the config.

        public string glockSkin;
        public string p2000Skin;
        public string uspsSkin;
        public string berettasSkin;
        public string p250Skin;
        public string tec9Skin;
        public string fivesevenSkin;
        public string cz75aSkin;
        public string revolverSkin;
        public string deagleSkin;
        public string novaSkin;
        public string xm1014Skin;
        public string sawedoffSkin;
        public string mag7Skin;
        public string negevSkin;
        public string m249Skin;
        public string mac10Skin;
        public string mp9Skin;
        public string mp7Skin;
        public string mp5sdSkin;
        public string ump45Skin;
        public string p90Skin;
        public string bizonSkin;
        public string galilSkin;
        public string famasSkin;
        public string ak47Skin;
        public string m4a1sSkin;
        public string m4a4Skin;
        public string sg553Skin;
        public string augSkin;
        public string ssg08Skin;
        public string awpSkin;
        public string g3sg1Skin;
        public string scar20Skin;
        public string entQuality;

        // Settings
        public int ONMVelocity;
        public int GlowInterval;
        public int ESPInterval;
        public int RadarInterval;
        public int BunnyhopMinInterval;
        public int BunnyhopMaxInterval;
        public int ReduceFlashAmount;
        public int ChamsBrightness;
        public int FakelagAmount;
        public int FakelagInterval;
        public bool StreamMode;

        // Colors
        public Color GlowEnemyCLR;
        public Color GlowTeamCLR;
        public Color GlowSpottedCLR;
        public Color GlowScopedCLR;

        public Color ChamEnemyCLR;
        public Color ChamTeamCLR;
        public Color ChamScopedCLR;

        public Color ESPEnemyCLR;
        public Color ESPTeamCLR;
        public Color ESPSpottedCLR;
        public Color ESPScopedCLR;
        public Color ESPCrosshairCLR;

        // Experimental
        public bool ESPRanksEnabled;
        public bool SndESPEnemyEnabled;
        public bool SndESPTeamEnabled;

        //public string TriggerbotCurrentDropdown;
    }
}
