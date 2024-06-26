using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Server;
using Server.Commands;
using Server.ContextMenus;
using Server.Gumps;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Network;
using Server.Regions;

namespace Server.Items
{
    public class DugUpZinc : Item
    {
        [Constructable]
        public DugUpZinc()
            : this(1) { }

        public override double DefaultWeight
        {
            get { return 2.0; }
        }

        [Constructable]
        public DugUpZinc(int amount)
            : base(0x19B9)
        {
            Name = "zinc";
            Stackable = true;
            Hue = 0x9C4;
            Amount = amount;
        }

        public DugUpZinc(Serial serial)
            : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

        public static bool CheckForDugUpZinc(Mobile from, int qty, bool remove)
        {
            bool pass = false;
            int carry = 0;

            if (qty > 0)
            {
                List<Item> belongings = new List<Item>();
                foreach (Item i in from.Backpack.Items)
                {
                    if (i is CopperOre)
                    {
                        carry = carry + i.Amount;
                    }
                }

                if (carry >= qty)
                {
                    pass = true;
                    Container pack = from.Backpack;
                    if (remove == true)
                    {
                        pack.ConsumeTotal(typeof(CopperOre), qty);
                    }
                }
            }

            return pass;
        }
    }
}
