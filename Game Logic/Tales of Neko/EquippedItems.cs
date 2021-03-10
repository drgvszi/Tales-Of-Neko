namespace Tales_of_Neko
{
    public class EquippedItems
    {
        public Item Helmet { get; set; }
        public Item Chestplate { get; set; }
        public Item Leggings { get; set; }
        public Item Boots { get; set; }
        
        public Item Weapon { get; set; }

        public EquippedItems()
        {
            Helmet=new Item("None",ItemType.Helmet);
            Chestplate=new Item("None",ItemType.Chestplate);
            Leggings=new Item("None",ItemType.Leggings);
            Boots=new Item("None",ItemType.Boots);
            Weapon=new Item("None",ItemType.Weapon);
            
        }
        public bool ReplaceHelmet(Item helmet)
        {
            if (helmet.ItemType != ItemType.Helmet)
            {
                return false;
            }

            Helmet = helmet;
            return true;
        }
        public bool ReplaceChestplate(Item chestplate) {
            if (chestplate.ItemType != ItemType.Chestplate)
            {
                return false;
            }

            Chestplate = chestplate;
            return true;

        }
        public bool ReplaceLeggings(Item leggings)
        {
            if (leggings.ItemType != ItemType.Leggings)
            {
                return false;
            }

            Leggings = leggings;
            return true;
        }
        public bool ReplaceBoots(Item boots)
        {
            if (boots.ItemType != ItemType.Boots)
            {
                return false;
            }

            Boots = boots;
            return true;
        }
        public bool ReplaceWeapon(Item weapon)
        {
            if (weapon.ItemType != ItemType.Weapon)
            {
                return false;
            }

            Weapon = weapon;
            return true;
        }

        public Stats GetBonusStats()
        {
            return Helmet.Stats + Chestplate.Stats + Leggings.Stats + Boots.Stats + Weapon.Stats;
        }
    }
}