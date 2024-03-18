using System;
using System.IO;
using System.Text;
using System.Xml;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Network;
using Server.Spells;
using Server.Spells.Chivalry;
using Server.Spells.Fourth;
using Server.Spells.Seventh;
using Server.Spells.Sixth;

namespace Server.Regions
{
    public class SavageRegion : BaseRegion
    {
        public SavageRegion(XmlElement xml, Map map, Region parent)
            : base(xml, map, parent) { }

        public override bool AllowHousing(Mobile from, Point3D p)
        {
            return false;
        }

        public override TimeSpan GetLogoutDelay(Mobile m)
        {
            return TimeSpan.Zero;
        }

        public override void AlterLightLevel(Mobile m, ref int global, ref int personal)
        {
            global = LightCycle.NightLevel;
        }

        public override bool AllowHarmful(Mobile from, Mobile target)
        {
            if ((from is PlayerMobile) && (target is PlayerMobile))
                return false;
            else
                return base.AllowHarmful(from, target);
        }

        public override bool OnBeginSpellCast(Mobile m, ISpell s)
        {
            m.SendMessage("That does not seem to work here.");
            return false;
        }

        public override void OnEnter(Mobile m)
        {
            base.OnEnter(m);
            if (m is PlayerMobile)
            {
                m.SendMessage("You find yourself in a primitive hut.");
            }
            if (
                m is PlayerMobile
                && m.AccessLevel < AccessLevel.GameMaster
                && ((PlayerMobile)m).SkillStart != 11000
            )
            {
                Server.Misc.PlayerSettings.SetSavage(m);
            }

            Server.Misc.RegionMusic.MusicRegion(m, this);
        }
    }
}
