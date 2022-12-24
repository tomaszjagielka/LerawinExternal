using Lerawin.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lerawin.Classes
{
    public class Entity
    {
        public int EntityBase;

        public Entity(int Base)
        {
            this.EntityBase = Base;
        }

        public int Health
        {
            get
            {
                return Memory.ReadMemory<int>(EntityBase + Main.O.netvars.m_iHealth);
            }
        }

        public float MapBrightness
        {
            set
            {
                int brightnessbase = Memory.ReadMemory<int>(EntityBase + Main.O.netvars.m_hTonemapController);
                int yes = brightnessbase & 0xFFF;
                int ok = Memory.ReadMemory<int>((int)Memory.Client + Main.O.signatures.dwEntityList + (yes - 1) * 16);
                Memory.WriteMemory<int>(ok + Main.O.netvars.m_bUseCustomAutoExposureMin, 1);
                Memory.WriteMemory<int>(ok + Main.O.netvars.m_bUseCustomAutoExposureMax, 1);
                Memory.WriteMemory<int>(ok + Main.O.netvars.m_flCustomAutoExposureMin, value);
                Memory.WriteMemory<int>(ok + Main.O.netvars.m_flCustomAutoExposureMax, value);
            }
        }

        public bool InReload // not networked
        {
            get
            {
                return Memory.ReadMemory<bool>(EntityBase + Main.O.netvars.m_bInReload);
            }
        }

        public bool InBuyZone
        {
            get
            {
                return Memory.ReadMemory<bool>(EntityBase + Main.O.netvars.m_bInBuyZone);
            }
        }


        public int GlowIndex
        {
            get
            {
                return Memory.ReadMemory<int>(EntityBase + Main.O.netvars.m_iGlowIndex);
            }
        }

        public int Team
        {
            get
            {
                return Memory.ReadMemory<int>(EntityBase + Main.O.netvars.m_iTeamNum);
            }
        }

        public int Kills
        {
            get
            {
                return Memory.ReadMemory<int>(EntityBase + Main.O.netvars.m_iKills);
            }
        }

        public int ObserverMode
        {
            get
            {
                return Memory.ReadMemory<int>(EntityBase + Main.O.netvars.m_iObserverMode);
            }
            set
            {
                Memory.WriteMemory<bool>(EntityBase + Main.O.netvars.m_iObserverMode, Convert.ToByte(value));
            }
        }

        public int Flags
        {
            get
            {
                return Memory.ReadMemory<int>(EntityBase + Main.O.netvars.m_fFlags);
            }
        }

        public int FOV
        {
            get
            {
                return Memory.ReadMemory<int>(EntityBase + Main.O.netvars.m_iFOV);
            }
            set
            {
                Memory.WriteMemory<bool>(EntityBase + Main.O.netvars.m_iFOV, Convert.ToByte(value));
            }
        }

        public int FOVStart
        {
            get
            {
                return Memory.ReadMemory<int>(EntityBase + Main.O.netvars.m_iFOVStart);
            }
            set
            {
                Memory.WriteMemory<bool>(EntityBase + Main.O.netvars.m_iFOVStart, Convert.ToByte(value));
            }
        }

        //public float DetectedByEnemySensorTime
        //{
        //    get
        //    {
        //        return Memory.ReadMemory<float>(EntityBase + 0x3960);
        //    }
        //    set
        //    {
        //        Memory.WriteMemory<float>(EntityBase + 0x3960, Convert.ToByte(value));
        //    }
        //}

        public int FOVDefault
        {
            get
            {
                return Memory.ReadMemory<int>(EntityBase + Main.O.netvars.m_iDefaultFOV);
            }
            set
            {
                Memory.WriteMemory<int>(EntityBase + Main.O.netvars.m_iDefaultFOV, Convert.ToByte(value));
            }
        }

        public float FOVRate
        {
            get
            {
                return Memory.ReadMemory<int>(EntityBase + Main.O.netvars.m_flFOVRate);
            }
            set
            {
                Memory.WriteMemory<int>(EntityBase + Main.O.netvars.m_flFOVRate, Convert.ToByte(value));
            }
        }

        public int TotalHitsOnServer
        {
            get
            {
                return Memory.ReadMemory<int>(EntityBase + Main.O.netvars.m_totalHitsOnServer);
            }
        }

        public bool IsTeammate
        {
            get
            {
                return Team == G.Engine.LocalPlayer.Team;
            }
        }

        public bool IsWalking
        {
            get
            {
                return Memory.ReadMemory<bool>(EntityBase + Main.O.netvars.m_bIsWalking);
            }
        }

        public Vector3 Position
        {
            get
            {
                return Memory.ReadMemory<Vector3>(EntityBase + Main.O.netvars.m_vecOrigin);
            }
        }

        public Vector3 EyePosition
        {
            get
            {
                return Position + Memory.ReadMemory<Vector3>(EntityBase + Main.O.netvars.m_vecViewOffset);
            }
        }

        public Vector3 HeadPosition
        {
            get
            {
                return GetBonePosition(8);
            }
        }

        public Vector3 NeckPosition
        {
            get
            {
                return GetBonePosition(7);
            }
        }

        public Vector3 ChestPosition
        {
            get
            {
                return GetBonePosition(6);
            }
        }

        public Vector3 LowerChestPosition
        {
            get
            {
                return GetBonePosition(5);
            }
        }

        public Vector3 StomachPosition
        {
            get
            {
                return GetBonePosition(4);
            }
        }

        public Vector3 LegsPosition
        {
            get
            {
                return GetBonePosition(3);
            }
        }

        public Vector3 GetBonePosition(int BoneID)
        {
            int bonematrix = Memory.ReadMemory<int>(EntityBase + Main.O.netvars.m_dwBoneMatrix);
            float x = Memory.ReadMemory<float>(bonematrix + 0x30 * BoneID + 0x0C);
            float y = Memory.ReadMemory<float>(bonematrix + 0x30 * BoneID + 0x1C);
            float z = Memory.ReadMemory<float>(bonematrix + 0x30 * BoneID + 0x2C);
            return new Vector3(x, y, z);
        }

        public bool Spotted
        {
            get
            {
                return Memory.ReadMemory<bool>(EntityBase + Main.O.netvars.m_bSpotted);
            }
            set
            {
                Memory.WriteMemory<bool>(EntityBase + Main.O.netvars.m_bSpotted, Convert.ToByte(value));
            }
        }

        public bool IsLocalPlayer
        {
            get
            {
                return G.Engine.LocalPlayer.EntityBase == EntityBase;
            }
        }

        public int Local
        {
            get
            {
                return Memory.ReadMemory<int>((int)Memory.Client + Main.O.signatures.dwLocalPlayer);
            }
        }

        public int FlashDuration
        {
            get
            {
                return Memory.ReadMemory<int>(EntityBase + Main.O.netvars.m_flFlashDuration);
            }
            set
            {
                Memory.WriteMemory<int>(EntityBase + Main.O.netvars.m_flFlashDuration, value);
            }
        }

        public float FlashMaxAlpha
        {
            get
            {
                return Memory.ReadMemory<int>(EntityBase + Main.O.netvars.m_flFlashMaxAlpha);
            }
            set
            {
                Memory.WriteMemory<int>(EntityBase + Main.O.netvars.m_flFlashMaxAlpha, value);
            }
        }

        public bool Dormant
        {
            get
            {
                return Memory.ReadMemory<bool>(EntityBase + Main.O.signatures.m_bDormant);
            }
        }

        public int CrosshairID
        {
            get
            {
                return Memory.ReadMemory<int>(EntityBase + Main.O.netvars.m_iCrosshairId);
            }
        }

        public int ClassID
        {
            get
            {
                int one = Memory.ReadMemory<int>(EntityBase + 0x8);
                int two = Memory.ReadMemory<int>(one + 2 * 0x4);
                int three = Memory.ReadMemory<int>(two + 0x1);
                return Memory.ReadMemory<int>(three + 0x14); // ClassID
            }
        }

        public void Glow(Color color)
        {
            GlowStruct GlowObj = new GlowStruct();

            GlowObj = Memory.ReadMemory<GlowStruct>(G.Engine.GlowObjectManager + GlowIndex * 0x38 + 0x4);

            GlowObj.r = (float)color.R / 255;
            GlowObj.g = (float)color.G / 255;
            GlowObj.b = (float)color.B / 255;
            GlowObj.a = (float)color.A / 255;
            GlowObj.m_bRenderWhenOccluded = true;
            GlowObj.m_bRenderWhenUnoccluded = false;
            GlowObj.m_bFullBloom = false;

            Memory.WriteMemory<GlowStruct>(G.Engine.GlowObjectManager + GlowIndex * 0x38 + 0x4, GlowObj);

            //memory.write((glowPointer) + ((playerAddress * 0x38) + 0x4), r);
            //memory.write((glowPointer) + ((playerAddress * 0x38) + 0x8), g);
            //memory.write((glowPointer) + ((playerAddress * 0x38) + 0xC), b);
            //memory.write((glowPointer) + ((playerAddress * 0x38) + 0x10), a);
            //memory.write((glowPointer) + ((playerAddress * 0x38) + 0x24), true;
            //memory.write((glowPointer) + ((playerAddress * 0x38) + 0x25), false);

            // Cody's glow:
            //Memory.WriteMemory<float>(glowObject + ((entity + 0x38) + 0x4), 2)
            //Memory.WriteMemory<float>(glowObject + ((entity + 0x38) + 0x8), 0)
            //Memory.WriteMemory<float>(glowObject + ((entity + 0x38) + 0xC), 0)
            //Memory.WriteMemory<float>(glowObject + ((entity + 0x38) + 0x10), 1.7)
            //Memory.WriteMemory<float>(glowObject + ((entity + 0x38) + 0x24), 2)
            //Memory.WriteMemory<float>(glowObject + ((entity + 0x38) + 0x25), 1.7)
        }

        public void Cham(Color color)
        {
            // these only show while visible
            Memory.WriteMemory<int>(EntityBase + Main.O.netvars.m_clrRender, color.R);
            Memory.WriteMemory<int>(EntityBase + Main.O.netvars.m_clrRender + 1, color.G);
            Memory.WriteMemory<int>(EntityBase + Main.O.netvars.m_clrRender + 2, color.B);
            Memory.WriteMemory<int>(EntityBase + Main.O.netvars.m_clrRender + 3, color.A);
        }

        public void ResetChams()
        {
            Cham(Color.FromArgb(255, 255, 255, 0));
        }

        public bool Alive
        {
            get
            {
                if (Health > 0 && Health <= 100)
                    return true;
                return false;
            }
        }

        public bool Dead
        {
            get
            {
                if (!Alive)
                    return true;
                return false;
            }
        }

        public bool IsPlayer
        {
            get
            {
                if (Team == 2 || Team == 3)
                    return true;
                else if (Team == 0 || Team == 1)
                    return false;
                else
                    return false;
            }
        }

        public bool IsStill
        {
            get
            {
                if (Velocity < Main.S.ONMVelocity)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //public int MyWeaponsHandle(int i)
        //{
        //    return Memory.ReadMemory<int>(EntityBase + Main.O.netvars.m_hMyWeapons + i * 0x4);
        //}

        //public int MyWeaponsIndex(int i)
        //{
        //    return MyWeaponsHandle(i) + 0xFFF;
        //}

        //public int CurrentMyWeapons(int i)
        //{
        //    return Memory.ReadMemory<int>((int)Memory.Client + Main.O.signatures.dwEntityList + (MyWeaponsIndex(i) - 1) * 16);
        //}

        public int handleActiveWeapon
        {
            get
            {
                return Memory.ReadMemory<int>(EntityBase + Main.O.netvars.m_hActiveWeapon);
            }
        }

        public int indexActiveWeapon
        {
            get
            {
                return handleActiveWeapon & 0xFFF;
            }
        }

        public int currentActiveWeapon
        {
            get
            {
                return Memory.ReadMemory<int>((int)Memory.Client + Main.O.signatures.dwEntityList + (indexActiveWeapon - 1) * 16);
            }
        }

        public int intWeaponID
        {
            get
            {
                return Memory.ReadMemory<int>(currentActiveWeapon + Main.O.netvars.m_iItemDefinitionIndex);
            }
        }

        public enumWeaponID enumWeaponID
        {
            get
            {
                return (enumWeaponID)Memory.ReadMemory<int>(currentActiveWeapon + Main.O.netvars.m_iItemDefinitionIndex);
            }
        }

        public int FallbackPaintKit
        {
            get
            {
                return Memory.ReadMemory<int>(currentActiveWeapon + Main.O.netvars.m_nFallbackPaintKit);
            }
            set
            {
                Memory.WriteMemory<int>(currentActiveWeapon + Main.O.netvars.m_nFallbackPaintKit, value);
            }
        }

        public int OriginalOwnerXuidLow
        {
            get
            {
                return Memory.ReadMemory<int>(currentActiveWeapon + Main.O.netvars.m_OriginalOwnerXuidLow);
            }
        }

        public bool NotReloading
        {
            get
            {
                return Memory.ReadMemory<bool>(currentActiveWeapon + Main.O.netvars.m_bReloadVisuallyComplete);
            }
        }

        public bool SilencerOn
        {
            get
            {
                return Memory.ReadMemory<bool>(currentActiveWeapon + Main.O.netvars.m_bSilencerOn);
            }
        }

        public int ZoomLevel
        {
            get
            {
                return Memory.ReadMemory<int>(currentActiveWeapon + Main.O.netvars.m_zoomLevel);
            }
        }

        public int Ammo
        {
            get
            {
                return Memory.ReadMemory<int>(currentActiveWeapon + Main.O.netvars.m_iClip1);
            }
        }

        public int FreezePeriod
        {
            get
            {
                return Memory.ReadMemory<int>(EntityBase + Main.O.netvars.m_bFreezePeriod);
            }
        }

        public int LocalViewmodel
        {
            get
            {
                return Memory.ReadMemory<int>(Local + Main.O.netvars.m_hViewModel);
            }
        }

        public string WeaponName
        {
            get
            {
                switch (this.enumWeaponID)
                {
                    case enumWeaponID.DEAGLE:
                        return "Desert Eagle";
                    case enumWeaponID.BERETTAS:
                        return "Dual Berettas";
                    case enumWeaponID.FIVESEVEN:
                        return "Five-SeveN";
                    case enumWeaponID.GLOCK:
                        return "Glock-18";
                    case enumWeaponID.AK47:
                        return "AK-47";
                    case enumWeaponID.AUG:
                        return "AUG";
                    case enumWeaponID.AWP:
                        return "AWP";
                    case enumWeaponID.FAMAS:
                        return "FAMAS";
                    case enumWeaponID.G3SG1:
                        return "G3SG1";
                    case enumWeaponID.GALIL:
                        return "Galil";
                    case enumWeaponID.M249:
                        return "M249";
                    case enumWeaponID.M4A4:
                        return "M4A1";
                    case enumWeaponID.MAC10:
                        return "MAC-10";
                    case enumWeaponID.P90:
                        return "P90";
                    case enumWeaponID.UMP45:
                        return "UMP-45";
                    case enumWeaponID.XM1014:
                        return "XM1014";
                    case enumWeaponID.BIZON:
                        return "PP-Bizon";
                    case enumWeaponID.MAG7:
                        return "MAG-7";
                    case enumWeaponID.NEGEV:
                        return "Negev";
                    case enumWeaponID.SAWEDOFF:
                        return "Sawed-Off";
                    case enumWeaponID.TEC9:
                        return "Tec-9";
                    case enumWeaponID.TASER:
                        return "Taser";
                    case enumWeaponID.P2000:
                        return "P2000";
                    case enumWeaponID.MP7:
                        return "MP7";
                    case enumWeaponID.MP9:
                        return "MP9";
                    case enumWeaponID.NOVA:
                        return "Nova";
                    case enumWeaponID.P250:
                        return "P250";
                    case enumWeaponID.MP5SD:
                        return "MP5-SD";
                    case enumWeaponID.SCAR20:
                        return "SCAR-20";
                    case enumWeaponID.SG556:
                        return "SG 553";
                    case enumWeaponID.SSG08:
                        return "SSG 08";
                    case enumWeaponID.KNIFE:
                        return "Knife";
                    case enumWeaponID.FLASHBANG:
                        return "Flashbang";
                    case enumWeaponID.HEGRENADE:
                        return "Grenade";
                    case enumWeaponID.SMOKEGRENADE:
                        return "Smoke Grenade";
                    case enumWeaponID.MOLOTOV:
                        return "Molotov";
                    case enumWeaponID.DECOY:
                        return "Decoy";
                    case enumWeaponID.INCGRENADE:
                        return "Incendiary Grenade";
                    case enumWeaponID.C4:
                        return "C4";
                    case enumWeaponID.M4A1S:
                        return "M4A1-S";
                    case enumWeaponID.USPS:
                        return "USP-S";
                    case enumWeaponID.CZ75A:
                        return "CZ75-Auto";
                    case enumWeaponID.REVOLVER:
                        return "R8 Revolver";
                    default:
                        return "Knife";
                }
            }
        }

        public bool Valid
        {
            get
            {
                if (Dormant)
                    return false;
                if (Dead)
                    return false;
                if (!IsPlayer)
                    return false;
                return true;
            }
        }

        public Vector2 AimPunchAngle
        {
            get
            {
                return Memory.ReadMemory<Vector2>(EntityBase + Main.O.netvars.m_aimPunchAngle);
            }
        }

        public int ShotsFired
        {
            get
            {
                return Memory.ReadMemory<int>(EntityBase + Main.O.netvars.m_iShotsFired);
            }
        }

        public float Velocity
        {
            get
            {
                Vector3 vel = Memory.ReadMemory<Vector3>(EntityBase + Main.O.netvars.m_vecVelocity);
                return (float)Math.Sqrt(vel.X * vel.X + vel.Y * vel.Y);
            }
        }
        public bool Scoped
        {
            get
            {
                return Memory.ReadMemory<bool>(EntityBase + Main.O.netvars.m_bIsScoped);
            }
        }

        public float Distance
        {
            get
            {
                float dist = Vector3.Distance(G.Engine.LocalPlayer.Position, Position);
                return dist;
            }
        }

        public bool HasArmor
        {
            get
            {
                return Memory.ReadMemory<bool>(EntityBase + Main.O.netvars.m_ArmorValue);
            }
        }

        public bool HasHelmet
        {
            get
            {
                return Memory.ReadMemory<bool>(EntityBase + Main.O.netvars.m_bHasHelmet);
            }
        }

        public bool HasDefuser
        {
            get
            {
                return Memory.ReadMemory<bool>(EntityBase + Main.O.netvars.m_bHasDefuser);
            }
        }

        public bool HasC4
        {
            get
            {
                return Memory.ReadMemory<bool>(G.Engine.PlayerResource + Main.O.netvars.m_iPlayerC4);
            }
        }

        public bool StartedArming
        {
            get
            {
                return Memory.ReadMemory<bool>(G.Engine.PlayerResource + Main.O.netvars.m_iPlayerC4);
            }
        }

        public bool IsDefusing
        {
            get
            {
                return Memory.ReadMemory<bool>(EntityBase + Main.O.netvars.m_bIsDefusing);
            }
        }

        public int Ping
        {
            get
            {
                return Memory.ReadMemory<int>(G.Engine.PlayerResource + Main.O.netvars.m_iPing + GlowIndex * 4);
            }
        }

        //public float NextAttack
        //{
        //    get
        //    {
        //        return Memory.ReadMemory<int>(EntityBase + Main.O.netvars.m_flNextAttack);
        //    }
        //}

        //public int Rank
        //{
        //    get
        //    {
        //        return Memory.ReadMemory<int>(G.Engine.PlayerResource + Main.O.netvars.m_iCompetitiveRanking);
        //    }
        //}

        //public int Wins
        //{
        //    get
        //    {
        //        return Memory.ReadMemory<int>(G.Engine.PlayerResource + Main.O.netvars.m_iCompetitiveWins);
        //    }
        //}

        //public int Kills
        //{
        //    get
        //    {
        //        return Memory.ReadMemory<int>(G.Engine.PlayerResource + Main.O.netvars.m_iKills);
        //    }
        //}

        //public int Score
        //{
        //    get
        //    {
        //        return Memory.ReadMemory<int>(G.Engine.PlayerResource + Main.O.netvars.m_iScore);
        //    }
        //}
    }
}
