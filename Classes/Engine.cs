using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lerawin.Utilities;

// NOTE TO MYSELF:  engine.dll is EnginePtr

namespace Lerawin.Classes
{
    public class Engine
    {
        public int EnginePtr;

        public Engine(int Base)
        {
            EnginePtr = Base;
        }

        public Entity LocalPlayer
        {
            get
            {
                return new Entity(Memory.ReadMemory<int>((int)Memory.Client + Main.O.signatures.dwLocalPlayer));
            }
        }

        public int RadarBase
        {
            get
            {
                return Memory.ReadMemory<int>((int)Memory.Client + Main.O.signatures.dwRadarBase);
            }
        }

        public int Radar
        {
            get
            {
                return Memory.ReadMemory<int>(RadarBase + 0x50);
            }
        }

        public int PlayerResource
        {
            get
            {
                return Memory.ReadMemory<int>((int)Memory.Client + Main.O.signatures.dwPlayerResource);
            }
        }

        public int GlowObjectManager
        {
            get
            {
                return Memory.ReadMemory<int>((int)Memory.Client + Main.O.signatures.dwGlowObjectManager);
            }
        }

        // Viewangles should never have a roll value.
        public Vector2 ViewAngles
        {
            get
            {
                Vector3 v3 = Memory.ReadMemory<Vector3>(ClientState + Main.O.signatures.dwClientState_ViewAngles);
                return new Vector2(v3.X, v3.Y);
            }
            set
            {
                Memory.WriteMemory<Vector3>(ClientState + Main.O.signatures.dwClientState_ViewAngles, new Vector3(value.X, value.Y, 0));
            }
        }

        public float[] ViewMatrix
        {
            get
            {
                float[] temp = new float[16];
                for (int i = 0; i < 16; i++)
                    temp[i] = Memory.ReadMemory<float>((int)Memory.Client + Main.O.signatures.dwViewMatrix + (i * 0x4));
                return temp;
            }
        }

        public int ClientState
        {
            get
            {
                return Memory.ReadMemory<int>(EnginePtr + Main.O.signatures.dwClientState);
            }
        }

        public void FullUpdate()
        {
            //g_pProcess->Write<int>(g_pEngine->GetClientStatePtr() + 0x174, -1);
            Memory.WriteMemory<int>(ClientState + 0x174, -1);
        }



        public int GameState
        {
            get
            {
                return Memory.ReadMemory<int>(ClientState + 0x108);
            }
        }

        public float ModelBrightness
        {
            get
            {
                return Memory.ReadMemory<int>(EnginePtr + Main.O.signatures.model_ambient_min);
            }
            set
            {
                int thisPtr = (int)(Memory.Engine + Main.O.signatures.model_ambient_min - 0x2c);
                byte[] bytearray = BitConverter.GetBytes(value);
                int intbrightness = BitConverter.ToInt32(bytearray, 0);
                int xored = intbrightness ^ thisPtr;
                Memory.WriteMemory<int>(EnginePtr + Main.O.signatures.model_ambient_min, xored);
            }
        }

        public int SendPackets
        {
            get
            {
                return Memory.ReadMemory<int>(EnginePtr + Main.O.signatures.dwbSendPackets);
            }
            set
            {
                Memory.WriteMemory<int>(EnginePtr + Main.O.signatures.dwbSendPackets, Convert.ToByte(value));
            }
        }

        public bool BombPlanted
        {
            get
            {
                int dwGameRulesProxy = Memory.ReadMemory<int>((int)Memory.Client + Main.O.signatures.dwGameRulesProxy);
                return Memory.ReadMemory<bool>(dwGameRulesProxy + Main.O.netvars.m_bBombPlanted);
            }
        }

        //public int IsBombTicking
        //{
        //    get
        //    {
        //        return Memory.ReadMemory<int>((int)Memory.Client + Main.O.netvars.m_bBombTicking);
        //    }
        //}

        //public int IsDefused
        //{
        //    get
        //    {
        //        return Memory.ReadMemory<int>((int)Memory.Client + Main.O.netvars.m_bBombDefused);

        //    }
        //}

        public void Shoot()
        {
            Memory.WriteMemory<int>((int)Memory.Client + Main.O.signatures.dwForceAttack, 5);
            Thread.Sleep(20);
            Memory.WriteMemory<int>((int)Memory.Client + Main.O.signatures.dwForceAttack, 4);
        }

        public void Shoot2()
        {
            Memory.WriteMemory<int>((int)Memory.Client + Main.O.signatures.dwForceAttack2, 5);
            Thread.Sleep(200);
            Memory.WriteMemory<int>((int)Memory.Client + Main.O.signatures.dwForceAttack2, 4);
        }

        public void Jump()
        {
            Memory.WriteMemory<int>((int)Memory.Client + Main.O.signatures.dwForceJump, 6);
            //Thread.Sleep(15);
            //Memory.WriteMemory<int>((int)Memory.Client + Main.O.signatures.dwForceJump, 4);
        }

        //public int MouseActive
        //{
        //    get
        //    {
        //        return Memory.ReadMemory<int>(((int)Memory.Client + 0xA91D70 + 0x30) ^ 0xA91D70);
        //    }
        //}

        public int MouseActive
        {
            get
            {
                return Memory.ReadMemory<int>((int)Memory.Scaleformui + 0x343F3C);
            }
        }
    }
}
