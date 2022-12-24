using Lerawin.Classes;
using Lerawin.Utilities;
using System.Threading;

namespace Lerawin.Features
{
    public class SkinChanger
    {
        public static int PaintKit = 0;
        public static int StatTrack = 0;
        public static int oldKills = 0;
        public static float Wear = 0;
        public static int Seed = 0;
        public static int EntityQuality = 0;
        public static bool PlayerSpawned = false;

        public static void Run(/*int KnifeModel*/)
        {
            while (true)
            {
                if (Main.S.SCEnabled && Menu.formIsClosed == false && Menu.CSGOActive && Menu.InGame) 
                {
                    //int ActiveWeapon = Memory.ReadMemory<int>(Local + Main.O.netvars.m_hActiveWeapon) & 0xFFF;
                    //ActiveWeapon = Memory.ReadMemory<int>((int)Memory.Client + Main.O.signatures.dwEntityList + (ActiveWeapon - 1) * 0x10);
                    //short ActiveWeaponID = Memory.ReadMemory<short>(ActiveWeapon + Main.O.netvars.m_iItemDefinitionIndex);
                    //var CSGOActiveWeapon = (enumWeaponID)ActiveWeaponID;
                    //int ActiveWeaponViewModelID = Memory.ReadMemory<int>(ActiveWeapon + Main.O.netvars.m_iViewModelIndex);
                    //int LocalViewModel = Memory.ReadMemory<int>(Local + Main.O.netvars.m_hViewModel);
                    //int WEAPON_KNIFECHANGER_ID = (int)Config.WEAPON_KNIFECHANGER;

                    for (int i = 0; i < 8; i++)
                    {
                        int indexMyWeapons = Memory.ReadMemory<int>(G.Engine.LocalPlayer.Local + Main.O.netvars.m_hMyWeapons + i * 0x4) & 0xFFF;
                        int currentMyWeapons = Memory.ReadMemory<int>((int)Memory.Client + Main.O.signatures.dwEntityList + (indexMyWeapons - 1) * 0x10);
                        short intWeaponID = Memory.ReadMemory<short>(currentMyWeapons + Main.O.netvars.m_iItemDefinitionIndex);
                        if (intWeaponID == 0) { continue; }
                        var enumWeaponID = (enumWeaponID)intWeaponID;

                        PaintKit = 0;
                        int StatTrack = Main.S.SCStatTrackAmount;
                        float Wear = Main.S.SCWearAmount;
                        if (Main.S.SCWearAmount == 0)
                            Wear = 0.0000000000000001f;
                        int Seed = Main.S.SCSeedAmount;
                        int EntityQuality = Menu.entQuality;
                        //string CustomName = "Name";

                        switch (enumWeaponID)
                        {
                            case enumWeaponID.GLOCK:
                                PaintKit = Menu.glockSkin;
                                break;
                            case enumWeaponID.P2000:
                                PaintKit = Menu.p2000Skin;
                                break;
                            case enumWeaponID.USPS:
                                PaintKit = Menu.uspsSkin;
                                break;
                            case enumWeaponID.BERETTAS:
                                PaintKit = Menu.berettasSkin;
                                break;
                            case enumWeaponID.P250:
                                PaintKit = Menu.p250Skin;
                                break;
                            case enumWeaponID.TEC9:
                                PaintKit = Menu.tec9Skin;
                                break;
                            case enumWeaponID.FIVESEVEN:
                                PaintKit = Menu.fivesevenSkin;
                                break;
                            case enumWeaponID.CZ75A:
                                PaintKit = Menu.cz75aSkin;
                                break;
                            case enumWeaponID.REVOLVER:
                                PaintKit = Menu.revolverSkin;
                                break;
                            case enumWeaponID.DEAGLE:
                                PaintKit = Menu.deagleSkin;
                                break;
                            case enumWeaponID.NOVA:
                                PaintKit = Menu.novaSkin;
                                break;
                            case enumWeaponID.XM1014:
                                PaintKit = Menu.xm1014Skin;
                                break;
                            case enumWeaponID.SAWEDOFF:
                                PaintKit = Menu.sawedoffSkin;
                                break;
                            case enumWeaponID.MAG7:
                                PaintKit = Menu.mag7Skin;
                                break;
                            case enumWeaponID.NEGEV:
                                PaintKit = Menu.negevSkin;
                                break;
                            case enumWeaponID.M249:
                                PaintKit = Menu.m249Skin;
                                break;
                            case enumWeaponID.MAC10:
                                PaintKit = Menu.mac10Skin;
                                break;
                            case enumWeaponID.MP9:
                                PaintKit = Menu.mp9Skin;
                                break;
                            case enumWeaponID.MP7:
                                PaintKit = Menu.mp7Skin;
                                break;
                            case enumWeaponID.MP5SD:
                                PaintKit = Menu.mp5sdSkin;
                                break;
                            case enumWeaponID.UMP45:
                                PaintKit = Menu.ump45Skin;
                                break;
                            case enumWeaponID.P90:
                                PaintKit = Menu.p90Skin;
                                break;
                            case enumWeaponID.BIZON:
                                PaintKit = Menu.bizonSkin;
                                break;
                            case enumWeaponID.GALIL:
                                PaintKit = Menu.galilSkin;
                                break;
                            case enumWeaponID.FAMAS:
                                PaintKit = Menu.famasSkin;
                                break;
                            case enumWeaponID.AK47:
                                PaintKit = Menu.ak47Skin;
                                break;
                            case enumWeaponID.M4A1S:
                                PaintKit = Menu.m4a1sSkin;
                                break;
                            case enumWeaponID.M4A4:
                                PaintKit = Menu.m4a4Skin;
                                break;
                            case enumWeaponID.SG556:
                                PaintKit = Menu.sg553Skin;
                                break;
                            case enumWeaponID.AUG:
                                PaintKit = Menu.augSkin;
                                break;
                            case enumWeaponID.SSG08:
                                PaintKit = Menu.ssg08Skin;
                                break;
                            case enumWeaponID.AWP:
                                PaintKit = Menu.awpSkin;
                                break;
                            case enumWeaponID.G3SG1:
                                PaintKit = Menu.g3sg1Skin;
                                break;
                            case enumWeaponID.SCAR20:
                                PaintKit = Menu.scar20Skin;
                                break;
                            default:
                                PaintKit = 0;
                                break;
                        }

                        int currentPaint = Memory.ReadMemory<int>(currentMyWeapons + Main.O.netvars.m_nFallbackPaintKit);
                        int currentSeed = Memory.ReadMemory<int>(currentMyWeapons + Main.O.netvars.m_nFallbackSeed);
                        int currentStatTrack = Memory.ReadMemory<int>(currentMyWeapons + Main.O.netvars.m_nFallbackStatTrak);
                        float currentWear = Memory.ReadMemory<float>(currentMyWeapons + Main.O.netvars.m_flFallbackWear);
                        int currentQuality = Memory.ReadMemory<int>(currentMyWeapons + Main.O.netvars.m_iEntityQuality);

                        if (PaintKit != 0 && (currentPaint != PaintKit || currentSeed != Seed || currentWear != Wear || (Main.S.SCStatTrackEnabled && !Main.S.SCRealStatTrackEnabled && currentStatTrack != StatTrack && Main.S.SCStatTrackAmount != 0)))
                        {
                            if (Memory.ReadMemory<int>(currentMyWeapons + Main.O.netvars.m_iItemIDHigh) != -1)
                                Memory.WriteMemory<int>(currentMyWeapons + Main.O.netvars.m_iItemIDHigh, -1);

                            Memory.WriteMemory<int>(currentMyWeapons + Main.O.netvars.m_OriginalOwnerXuidLow, 0);
                            Memory.WriteMemory<int>(currentMyWeapons + Main.O.netvars.m_OriginalOwnerXuidHigh, 0);
                            Memory.WriteMemory<int>(currentMyWeapons + Main.O.netvars.m_nFallbackPaintKit, PaintKit);
                            Memory.WriteMemory<int>(currentMyWeapons + Main.O.netvars.m_nFallbackSeed, Seed);
                            if (Main.S.SCStatTrackEnabled)
                                Memory.WriteMemory<int>(currentMyWeapons + Main.O.netvars.m_nFallbackStatTrak, StatTrack);
                            Memory.WriteMemory<float>(currentMyWeapons + Main.O.netvars.m_flFallbackWear, Wear);
                            //Memory.WriteString(currentMyWeapons + Main.O.netvars.m_szCustomName, CustomName);

                            if (Main.S.SCStatTrackEnabled)
                                Memory.WriteMemory<int>(currentMyWeapons + Main.O.netvars.m_iEntityQuality, 9);
                            else
                                Memory.WriteMemory<int>(currentMyWeapons + Main.O.netvars.m_iEntityQuality, EntityQuality);

                            if (Main.S.SCBuyZoneOnlyEnabled)
                            {
                                if (G.Engine.LocalPlayer.InBuyZone)
                                {
                                    if (!PlayerSpawned)
                                    {
                                        Thread.Sleep(1000);
                                        PlayerSpawned = true;
                                    }

                                    G.Engine.FullUpdate();
                                }
                                else
                                {
                                    PlayerSpawned = false;
                                }
                            }
                            else if (!PlayerSpawned)
                            {
                                G.Engine.FullUpdate();
                            }
                        }
                    }
                }
                Thread.Sleep(1);
            }
        }

        public static bool realStattrack
        {
            get
            {
                int kills = Memory.ReadMemory<int>(G.Engine.PlayerResource + Main.O.netvars.m_iKills + 1 * 4);
                if (kills > oldKills)
                {
                    //Console.WriteLine("killed");
                    //Console.WriteLine("Kills = " + kills);
                    //Console.WriteLine("Old kills = " + oldKills);
                    oldKills = kills;

                    return true;
                }
                else
                    return false;
            }
        }

        // WORKING, BUT GUT KNIFE AND AFTER FORCE RELOAD.
        //if (NeedIndexes)
        //{
        //    int hWeapon = Memory.ReadMemory<int>(Local + Main.O.netvars.m_hViewModel);
        //    int KnifeBase = Memory.ReadMemory<int>((int)Memory.Client + Main.O.signatures.dwEntityList + ((hWeapon & 0xFFF) - 1) * 0x10);

        //    switch (CSGOWeaponID)
        //    {
        //        case enumWeaponID.USPS:
        //            {
        //                StartPoint = Memory.ReadMemory<int>(KnifeBase + Main.O.netvars.m_nModelIndex);
        //                KnifeID = StartPoint + 28 + (3 * KnifeModel - 3);
        //                NeedIndexes = false;
        //                break;
        //            }

        //        case enumWeaponID.GLOCK:
        //            {
        //                StartPoint = Memory.ReadMemory<int>(KnifeBase + Main.O.netvars.m_nModelIndex);
        //                KnifeID = StartPoint + 273 + (3 * KnifeModel - 3);
        //                NeedIndexes = false;
        //                break;
        //            }

        //        case enumWeaponID.P2000:
        //            {
        //                StartPoint = Memory.ReadMemory<int>(KnifeBase + Main.O.netvars.m_nModelIndex);
        //                KnifeID = StartPoint + 128 + (3 * KnifeModel - 3);
        //                NeedIndexes = false;
        //                break;
        //            }

        //        case enumWeaponID.DEAGLE:
        //            {
        //                StartPoint = Memory.ReadMemory<int>(KnifeBase + Main.O.netvars.m_nModelIndex);
        //                KnifeID = StartPoint + 300 + (3 * KnifeModel - 3);
        //                NeedIndexes = false;
        //                break;
        //            }
        //    }
        //}
        //else if (G.Engine.LocalPlayer.Ammo == -1 & G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.KNIFE & !NeedIndexes & Weapon >= 1000)
        //{
        //    int hWeapon = Memory.ReadMemory<int>(Local + Main.O.netvars.m_hViewModel);
        //    int KnifeBase = Memory.ReadMemory<int>((int)Memory.Client + Main.O.signatures.dwEntityList + ((hWeapon & 0xFFF) - 1) * 0x10);

        //    if (KnifeBase >= 1000)
        //    {
        //        setKnifeData(KnifeID);

        //        if (G.Engine.LocalPlayer.Ammo == -1 & G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.KNIFE & Memory.ReadMemory<int>(KnifeBase + Main.O.netvars.m_nModelIndex) != KnifeID)
        //            Memory.WriteMemory<int>(KnifeBase + Main.O.netvars.m_nModelIndex, KnifeID);

        //        tSkinModel tmpSkinModel = Memory.ReadMemory<tSkinModel>(Weapon + Main.O.netvars.m_iWorldModelIndex);

        //        if (G.Engine.LocalPlayer.Ammo == -1 & G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.KNIFE & tmpSkinModel.off1 != KnifeID + 1 | tmpSkinModel.off2 != KnifeID)
        //        {
        //            Memory.WriteMemory<int>(Weapon + Main.O.netvars.m_nModelIndex, SkinModel.off1);
        //            Memory.WriteMemory<int>(Weapon + Main.O.netvars.m_iWorldModelIndex, SkinModel.off1);
        //            Memory.WriteMemory<int>(Weapon + Main.O.netvars.m_iWorldModelIndex + 0x4, SkinModel.off2);
        //        }

        //        if (G.Engine.LocalPlayer.Ammo == -1 & G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.KNIFE & Memory.ReadMemory<short>(Weapon + Main.O.netvars.m_iItemDefinitionIndex) != GetKnifeID(KnifeModel))
        //            Memory.WriteMemory<short>(Weapon + Main.O.netvars.m_iItemDefinitionIndex, GetKnifeID(KnifeModel));
        //    }
        //}


        // WORKING KNIFECHANGER, BUT ONLY ICON
        //int currentWeapon = Memory.ReadMemory<int>(Local + Main.O.netvars.m_hMyWeapons + ((1 - 1) * 0x04));
        //int currentWeaponEntityID = (currentWeapon & 0xfff);

        //int weaponEntity = Memory.ReadMemory<int>((int)Memory.Client + Main.O.signatures.dwEntityList + (currentWeaponEntityID - 1) * 0x10);

        //int accid = Memory.ReadMemory<int>(weaponEntity + Main.O.netvars.m_OriginalOwnerXuidLow);

        //Memory.WriteMemory<int>(weaponEntity + Main.O.netvars.m_nModelIndex, 388);
        //Memory.WriteMemory<int>(weaponEntity + Main.O.netvars.m_iViewModelIndex, 388);
        //Memory.WriteMemory<int>(Local + Main.O.netvars.m_iWorldModelIndex, 389);
        //Memory.WriteMemory<int>(weaponEntity + Main.O.netvars.m_iItemDefinitionIndex, 507);

        //Memory.WriteMemory<int>(weaponEntity + Main.O.netvars.m_iAccountID, accid);

        //https://www.unknowncheats.me/forum/counterstrike-global-offensive/169503-external-knife-changer.html
        //if (CSGOWeaponID == enumWeaponID.KNIFE)
        //{
        //    Memory.WriteMemory<int>(Weapon + Main.O.netvars.m_iItemDefinitionIndex, 507);
        //    Memory.WriteMemory<int>(Weapon + Main.O.netvars.m_iViewModelIndex, 326);
        //    Memory.WriteMemory<int>(Local + Main.O.netvars.m_iWorldModelIndex, 327);
        //    Memory.WriteMemory<int>(Weapon + Main.O.netvars.m_iItemIDHigh, -1);
        //    Memory.WriteMemory<int>(Weapon + Main.O.netvars.m_nFallbackStatTrak, 6262);
        //    Memory.WriteMemory<int>(Weapon + Main.O.netvars.m_nFallbackPaintKit, 38);
        //    Memory.WriteMemory<int>(Weapon + Main.O.netvars.m_flFallbackWear, 0.000001);
        //}

        //https://www.unknowncheats.me/forum/counterstrike-global-offensive/181693-csgo-external-knife-changer.html
        //int WeaponIndex = Memory.ReadMemory<int>(Local + Main.O.netvars.m_hActiveWeapon) & 0xFFFF;
        //int EntityList = Memory.ReadMemory<int>((int)Memory.Client + Main.O.signatures.dwEntityList + (WeaponIndex - 1) * 0x10);
        //int weaponid = Memory.ReadMemory<int>(EntityList + Main.O.netvars.m_iItemDefinitionIndex);

        //Memory.WriteMemory<int>(EntityList + Main.O.netvars.m_iItemDefinitionIndex, 511); //m9 bayonet
        //Memory.WriteMemory<int>(EntityList + Main.O.netvars.m_OriginalOwnerXuidLow, 0);
        //Memory.WriteMemory<int>(EntityList + Main.O.netvars.m_OriginalOwnerXuidHigh, 0);
        //Memory.WriteMemory<int>(EntityList + Main.O.netvars.m_iViewModelIndex, 326);
        //Memory.WriteMemory<int>(Local + Main.O.netvars.m_iWorldModelIndex, 327);
        //Memory.WriteMemory<int>(EntityList + Main.O.netvars.m_iItemIDLow, -1);
        //Memory.WriteMemory<int>(EntityList + Main.O.netvars.m_iItemIDHigh, -1);
        //Memory.WriteMemory<int>(EntityList + Main.O.netvars.m_nFallbackStatTrak, 420);
        //Memory.WriteMemory<int>(EntityList + Main.O.netvars.m_nFallbackPaintKit, 568); //gamma doppler
        //Memory.WriteMemory<float>(EntityList + Main.O.netvars.m_flFallbackWear, 0.000000001); //float

        //if (Memory.ReadMemory<int>(EntityList + Main.O.netvars.m_nModelIndex) != 388 - 1)
        //{
        //    Memory.WriteMemory<int>(EntityList + Main.O.netvars.m_iItemIDLow, -1);
        //    Memory.WriteMemory<int>(EntityList + Main.O.netvars.m_iItemIDHigh, -1);
        //    Memory.WriteMemory<int>(EntityList + Main.O.netvars.m_iViewModelIndex, 388);
        //    Memory.WriteMemory<int>(EntityList + Main.O.netvars.m_iWorldModelIndex, 389);
        //    Memory.WriteMemory<int>(EntityList + Main.O.netvars.m_iItemDefinitionIndex, 507);
        //    Memory.WriteMemory<int>(EntityList + Main.O.netvars.m_nModelIndex, 388 - 1);
        //}
    }
}
