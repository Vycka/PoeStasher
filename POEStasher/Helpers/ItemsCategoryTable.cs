using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POEStasher.ItemsManager;
/*
 *                                                           ####         ####      +2
        private static string[] PoeItemMarauderCatNames = { "1H Mace", "2H Mace", "Chest", "Helment", "Gloves", "Shield", "Unknown" };
 *                                                         ####     #############      #####      +3
        private static string[] PoeItemRangerCatNames = { "Bow", "1H Sword (Rapier)", "Quiver", "Boots", "Chest", "Helment", "Gloves", "Shield", "Unknown" };
 *                                                       ######   +1
        private static string[] PoeItemWitchCatNames = { "Wand", "Boots", "Chest", "Helment", "Gloves", "Shield", "Unknown" };
 *                                                          ####      ####        ####        ####       +4
        private static string[] PoeItemDuelistCatNames = { "1H Axe", "2H Axe", "1H Sword", "2H Sword", "Boots", "Chest", "Helment", "Gloves", "Shield", "Unknown" };
 *                                                         ###################  ###############    +2    
 *      private static string[] PoeItemTemplarCatNames = { "1H Mace (Sceptre)", "Staff (Stave)", "Boots", "Chest", "Helment", "Gloves", "Shield", "Unknown" };
 *                                                        ######   ######     +2
        private static string[] PoeItemShadowCatNames = { "Claw", "Dagger", "Boots", "Chest", "Helment", "Gloves", "Shield", "Unknown" };
 * 
 * 
        private static string[] PoeItemOtherCatNames = {  "Unknown", "Blue Gems", "Blue Modifiers", // 0 1 2
                                                         "Green Gems", "Green Modifiers", // 3 4
                                                         "Red Gems", "Red Modifiers",  // 5 6
                                                         "Amulets", "Rings" ,"Belts","HP Flasks", "Mana Flasks", "Other Flasks", "Maps", "Uniques" };
        //                                                 7          8          9       10           11              12           13       14
 * 
 * 
 * ue regex empty line: ^p$
 * 
 * ^(.+)$
 * 
 * ItemTree.Add("\1", new CatSubCat(PoeItem.ClassIndex.Misc,5));
 */

namespace POEStasher.Helpers
{
    public class ItemsCategoryTable
    {

        private static SortedDictionary<string, CatSubCat> ItemTree = null;
        public static readonly CatSubCat DefaultCat = new CatSubCat(PoeItem.ClassIndex.Misc, 0);
        public static readonly CatSubCat UniqueCat = new CatSubCat(PoeItem.ClassIndex.Misc, 14);
        public static readonly CatSubCat MapCat = new CatSubCat(PoeItem.ClassIndex.Misc, 13);
        public static readonly CatSubCat HpCat = new CatSubCat(PoeItem.ClassIndex.Misc, 10);
        public static readonly CatSubCat ManaCat = new CatSubCat(PoeItem.ClassIndex.Misc, 11);
        public static readonly CatSubCat OtherCat = new CatSubCat(PoeItem.ClassIndex.Misc, 12);
        public static readonly CatSubCat BeltCat = new CatSubCat(PoeItem.ClassIndex.Misc, 9);
        public static readonly CatSubCat RingCat = new CatSubCat(PoeItem.ClassIndex.Misc, 8);
        public static readonly CatSubCat AmuletCat = new CatSubCat(PoeItem.ClassIndex.Misc, 7);

        /// <summary>
        /// Fills ItemTree with values
        /// </summary>
        static ItemsCategoryTable()
        {
            ItemTree = new SortedDictionary<string, CatSubCat>();

            #region Red Gems
            ItemTree.Add("Anger", new CatSubCat(PoeItem.ClassIndex.Misc, 5));
            ItemTree.Add("Cleave", new CatSubCat(PoeItem.ClassIndex.Misc, 5));
            ItemTree.Add("Decoy Totem", new CatSubCat(PoeItem.ClassIndex.Misc, 5));
            ItemTree.Add("Determination", new CatSubCat(PoeItem.ClassIndex.Misc, 5));
            ItemTree.Add("Devouring Totem", new CatSubCat(PoeItem.ClassIndex.Misc, 5));
            ItemTree.Add("Dominating Blow", new CatSubCat(PoeItem.ClassIndex.Misc, 5));
            ItemTree.Add("Enduring Cry", new CatSubCat(PoeItem.ClassIndex.Misc, 5));
            ItemTree.Add("Flame Totem", new CatSubCat(PoeItem.ClassIndex.Misc, 5));
            ItemTree.Add("Glacial Hammer", new CatSubCat(PoeItem.ClassIndex.Misc, 5));
            ItemTree.Add("Ground Slam", new CatSubCat(PoeItem.ClassIndex.Misc, 5));
            ItemTree.Add("Heavy Strike", new CatSubCat(PoeItem.ClassIndex.Misc, 5));
            ItemTree.Add("Immortal Call", new CatSubCat(PoeItem.ClassIndex.Misc, 5));
            ItemTree.Add("Infernal Blow", new CatSubCat(PoeItem.ClassIndex.Misc, 5));
            ItemTree.Add("Leap Slam", new CatSubCat(PoeItem.ClassIndex.Misc, 5));
            ItemTree.Add("Lightning Strike", new CatSubCat(PoeItem.ClassIndex.Misc, 5));
            ItemTree.Add("Molten Shell", new CatSubCat(PoeItem.ClassIndex.Misc, 5));
            ItemTree.Add("Punishment", new CatSubCat(PoeItem.ClassIndex.Misc, 5));
            ItemTree.Add("Rejuvenation Totem", new CatSubCat(PoeItem.ClassIndex.Misc, 5));
            ItemTree.Add("Shield Charge", new CatSubCat(PoeItem.ClassIndex.Misc, 5));
            ItemTree.Add("Shockwave Totem", new CatSubCat(PoeItem.ClassIndex.Misc, 5));
            ItemTree.Add("Sweep", new CatSubCat(PoeItem.ClassIndex.Misc, 5));
            ItemTree.Add("Vitality", new CatSubCat(PoeItem.ClassIndex.Misc, 5));
            ItemTree.Add("Warlord's Mark", new CatSubCat(PoeItem.ClassIndex.Misc, 5));
            #endregion
            #region Green Gems
            ItemTree.Add("Bear Trap", new CatSubCat(PoeItem.ClassIndex.Misc, 3));
            ItemTree.Add("Blood Rage", new CatSubCat(PoeItem.ClassIndex.Misc, 3));
            ItemTree.Add("Burning Arrow", new CatSubCat(PoeItem.ClassIndex.Misc, 3));
            ItemTree.Add("Detonate Dead", new CatSubCat(PoeItem.ClassIndex.Misc, 3));
            ItemTree.Add("Double Strike", new CatSubCat(PoeItem.ClassIndex.Misc, 3));
            ItemTree.Add("Dual Strike", new CatSubCat(PoeItem.ClassIndex.Misc, 3));
            ItemTree.Add("Elemental Hit", new CatSubCat(PoeItem.ClassIndex.Misc, 3));
            ItemTree.Add("Ethereal Knives", new CatSubCat(PoeItem.ClassIndex.Misc, 3));
            ItemTree.Add("Explosive Arrow", new CatSubCat(PoeItem.ClassIndex.Misc, 3));
            ItemTree.Add("Fire Trap", new CatSubCat(PoeItem.ClassIndex.Misc, 3));
            ItemTree.Add("Flicker Strike", new CatSubCat(PoeItem.ClassIndex.Misc, 3));
            ItemTree.Add("Freeze Mine", new CatSubCat(PoeItem.ClassIndex.Misc, 3));
            ItemTree.Add("Frenzy", new CatSubCat(PoeItem.ClassIndex.Misc, 3));
            ItemTree.Add("Grace", new CatSubCat(PoeItem.ClassIndex.Misc, 3));
            ItemTree.Add("Haste", new CatSubCat(PoeItem.ClassIndex.Misc, 3));
            ItemTree.Add("Hatred", new CatSubCat(PoeItem.ClassIndex.Misc, 3));
            ItemTree.Add("Ice Shot", new CatSubCat(PoeItem.ClassIndex.Misc, 3));
            ItemTree.Add("Lightning Arrow", new CatSubCat(PoeItem.ClassIndex.Misc, 3));
            ItemTree.Add("Phase Run", new CatSubCat(PoeItem.ClassIndex.Misc, 3));
            ItemTree.Add("Poison Arrow", new CatSubCat(PoeItem.ClassIndex.Misc, 3));
            ItemTree.Add("Projectile Weakness", new CatSubCat(PoeItem.ClassIndex.Misc, 3));
            ItemTree.Add("Puncture", new CatSubCat(PoeItem.ClassIndex.Misc, 3));
            ItemTree.Add("Rain of Arrows", new CatSubCat(PoeItem.ClassIndex.Misc, 3));
            ItemTree.Add("Split Arrow", new CatSubCat(PoeItem.ClassIndex.Misc, 3));
            ItemTree.Add("Temporal Chains", new CatSubCat(PoeItem.ClassIndex.Misc, 3));
            ItemTree.Add("Viper Strike", new CatSubCat(PoeItem.ClassIndex.Misc, 3));
            ItemTree.Add("Whirling Blades", new CatSubCat(PoeItem.ClassIndex.Misc, 3));
            #endregion
            #region Blue Gems
            ItemTree.Add("Arc", new CatSubCat(PoeItem.ClassIndex.Misc, 1));
            ItemTree.Add("Arctic Breath", new CatSubCat(PoeItem.ClassIndex.Misc, 1));
            ItemTree.Add("Clarity", new CatSubCat(PoeItem.ClassIndex.Misc, 1));
            ItemTree.Add("Cold Snap", new CatSubCat(PoeItem.ClassIndex.Misc, 1));
            ItemTree.Add("Conductivity", new CatSubCat(PoeItem.ClassIndex.Misc, 1));
            ItemTree.Add("Conversion Trap", new CatSubCat(PoeItem.ClassIndex.Misc, 1));
            ItemTree.Add("Critical Weakness", new CatSubCat(PoeItem.ClassIndex.Misc, 1));
            ItemTree.Add("Discharge", new CatSubCat(PoeItem.ClassIndex.Misc, 1));
            ItemTree.Add("Discipline", new CatSubCat(PoeItem.ClassIndex.Misc, 1));
            ItemTree.Add("Elemental Weakness", new CatSubCat(PoeItem.ClassIndex.Misc, 1));
            ItemTree.Add("Enfeeble", new CatSubCat(PoeItem.ClassIndex.Misc, 1));
            ItemTree.Add("Fireball", new CatSubCat(PoeItem.ClassIndex.Misc, 1));
            ItemTree.Add("Firestorm", new CatSubCat(PoeItem.ClassIndex.Misc, 1));
            ItemTree.Add("Flammability", new CatSubCat(PoeItem.ClassIndex.Misc, 1));
            ItemTree.Add("Freezing Pulse", new CatSubCat(PoeItem.ClassIndex.Misc, 1));
            ItemTree.Add("Frostbite", new CatSubCat(PoeItem.ClassIndex.Misc, 1));
            ItemTree.Add("Frost Wall", new CatSubCat(PoeItem.ClassIndex.Misc, 1));
            ItemTree.Add("Ice Nova", new CatSubCat(PoeItem.ClassIndex.Misc, 1));
            ItemTree.Add("Ice Spear", new CatSubCat(PoeItem.ClassIndex.Misc, 1));
            ItemTree.Add("Incinerate", new CatSubCat(PoeItem.ClassIndex.Misc, 1));
            ItemTree.Add("Lightning Warp", new CatSubCat(PoeItem.ClassIndex.Misc, 1));
            ItemTree.Add("Power Siphon", new CatSubCat(PoeItem.ClassIndex.Misc, 1));
            ItemTree.Add("Purity", new CatSubCat(PoeItem.ClassIndex.Misc, 1));
            ItemTree.Add("Raise Spectre", new CatSubCat(PoeItem.ClassIndex.Misc, 1));
            ItemTree.Add("Raise Zombie", new CatSubCat(PoeItem.ClassIndex.Misc, 1));
            ItemTree.Add("Righteous Fire", new CatSubCat(PoeItem.ClassIndex.Misc, 1));
            ItemTree.Add("Shock Nova", new CatSubCat(PoeItem.ClassIndex.Misc, 1));
            ItemTree.Add("Spark", new CatSubCat(PoeItem.ClassIndex.Misc, 1));
            ItemTree.Add("Summon Skeletons", new CatSubCat(PoeItem.ClassIndex.Misc, 1));
            ItemTree.Add("Tempest Shield", new CatSubCat(PoeItem.ClassIndex.Misc, 1));
            ItemTree.Add("Vulnerability", new CatSubCat(PoeItem.ClassIndex.Misc, 1));
            ItemTree.Add("Wrath", new CatSubCat(PoeItem.ClassIndex.Misc, 1));
            #endregion
            #region Red Support Gems
            ItemTree.Add("Added Fire Damage", new CatSubCat(PoeItem.ClassIndex.Misc, 6));
            ItemTree.Add("Blood Magic", new CatSubCat(PoeItem.ClassIndex.Misc, 6));
            ItemTree.Add("Cold to Fire", new CatSubCat(PoeItem.ClassIndex.Misc, 6));
            ItemTree.Add("Fire Penetration", new CatSubCat(PoeItem.ClassIndex.Misc, 6));
            ItemTree.Add("Increased Duration", new CatSubCat(PoeItem.ClassIndex.Misc, 6));
            ItemTree.Add("Iron Grip", new CatSubCat(PoeItem.ClassIndex.Misc, 6));
            ItemTree.Add("Iron Will", new CatSubCat(PoeItem.ClassIndex.Misc, 6));
            ItemTree.Add("Item Quantity", new CatSubCat(PoeItem.ClassIndex.Misc, 6));
            ItemTree.Add("Knockback", new CatSubCat(PoeItem.ClassIndex.Misc, 6));
            ItemTree.Add("Life Gain on Hit", new CatSubCat(PoeItem.ClassIndex.Misc, 6));
            ItemTree.Add("Life Leech", new CatSubCat(PoeItem.ClassIndex.Misc, 6));
            ItemTree.Add("Melee Damage on Full Life", new CatSubCat(PoeItem.ClassIndex.Misc, 6));
            ItemTree.Add("Melee Physical Damage", new CatSubCat(PoeItem.ClassIndex.Misc, 6));
            ItemTree.Add("Ranged Attack Totem", new CatSubCat(PoeItem.ClassIndex.Misc, 6));
            ItemTree.Add("Reduced Mana", new CatSubCat(PoeItem.ClassIndex.Misc, 6));
            ItemTree.Add("Spell Totem", new CatSubCat(PoeItem.ClassIndex.Misc, 6));
            ItemTree.Add("Stun", new CatSubCat(PoeItem.ClassIndex.Misc, 6));
            ItemTree.Add("Weapon Elemental Damage", new CatSubCat(PoeItem.ClassIndex.Misc, 6));
            #endregion
            #region Green Support Gems
            ItemTree.Add("Added Cold Damage", new CatSubCat(PoeItem.ClassIndex.Misc, 4));
            ItemTree.Add("Additional Accuracy", new CatSubCat(PoeItem.ClassIndex.Misc, 4));
            ItemTree.Add("Blind", new CatSubCat(PoeItem.ClassIndex.Misc, 4));
            ItemTree.Add("Chain", new CatSubCat(PoeItem.ClassIndex.Misc, 4));
            ItemTree.Add("Chance to Flee", new CatSubCat(PoeItem.ClassIndex.Misc, 4));
            ItemTree.Add("Cold Penetration", new CatSubCat(PoeItem.ClassIndex.Misc, 4));
            ItemTree.Add("Culling Strike", new CatSubCat(PoeItem.ClassIndex.Misc, 4));
            ItemTree.Add("Faster Attacks", new CatSubCat(PoeItem.ClassIndex.Misc, 4));
            ItemTree.Add("Faster Projectiles", new CatSubCat(PoeItem.ClassIndex.Misc, 4));
            ItemTree.Add("Fork", new CatSubCat(PoeItem.ClassIndex.Misc, 4));
            ItemTree.Add("Greater Multiple Projectiles", new CatSubCat(PoeItem.ClassIndex.Misc, 4));
            ItemTree.Add("Lesser Multiple Projectiles", new CatSubCat(PoeItem.ClassIndex.Misc, 4));
            ItemTree.Add("Mana Leech", new CatSubCat(PoeItem.ClassIndex.Misc, 4));
            ItemTree.Add("Pierce", new CatSubCat(PoeItem.ClassIndex.Misc, 4));
            ItemTree.Add("Point Blank", new CatSubCat(PoeItem.ClassIndex.Misc, 4));
            ItemTree.Add("Trap", new CatSubCat(PoeItem.ClassIndex.Misc, 4));
            #endregion
            #region Blue Support Gems
            ItemTree.Add("Added Chaos Damage", new CatSubCat(PoeItem.ClassIndex.Misc, 2));
            ItemTree.Add("Added Lightning Damage", new CatSubCat(PoeItem.ClassIndex.Misc, 2));
            ItemTree.Add("Chance to Ignite", new CatSubCat(PoeItem.ClassIndex.Misc, 2));
            ItemTree.Add("Concentrated Effect", new CatSubCat(PoeItem.ClassIndex.Misc, 2));
            ItemTree.Add("Elemental Proliferation", new CatSubCat(PoeItem.ClassIndex.Misc, 2));
            ItemTree.Add("Faster Casting", new CatSubCat(PoeItem.ClassIndex.Misc, 2));
            ItemTree.Add("Increased Area of Effect", new CatSubCat(PoeItem.ClassIndex.Misc, 2));
            ItemTree.Add("Increased Critical Damage", new CatSubCat(PoeItem.ClassIndex.Misc, 2));
            ItemTree.Add("Increased Critical Strikes", new CatSubCat(PoeItem.ClassIndex.Misc, 2));
            ItemTree.Add("Item Rarity", new CatSubCat(PoeItem.ClassIndex.Misc, 2));
            ItemTree.Add("Lightning Penetration", new CatSubCat(PoeItem.ClassIndex.Misc, 2));
            ItemTree.Add("Minion Damage", new CatSubCat(PoeItem.ClassIndex.Misc, 2));
            ItemTree.Add("Minion Life", new CatSubCat(PoeItem.ClassIndex.Misc, 2));
            ItemTree.Add("Minion Speed", new CatSubCat(PoeItem.ClassIndex.Misc, 2));
            ItemTree.Add("Remote Mine", new CatSubCat(PoeItem.ClassIndex.Misc, 2));
            #endregion

            #region Helments
            ItemTree.Add("Rusted Casque", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Footman's Casque", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Gladiator Casque", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Executioner Casque", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Imperial Casque", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Reaver Casque", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Mercenary Casque", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Vaal Casque", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Destroyer Casque", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Annihilator Casque", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Armageddon Casque", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Helment));

            ItemTree.Add("Battered Cap", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Leather Cap", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Leather Hood", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Studded Hood", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Hunter Cap", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Forest Hood", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Grove Hood", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Huntress Cap", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Thicket Hood", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Jungle Hood", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Helment));

            ItemTree.Add("Vine Circlet", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Copper Circlet", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Tribal Circlet", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Gemmed Circlet", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Gilded Circlet", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Carved Circlet", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Encrusted Circlet", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Gleaming Circlet", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Petrified Circlet", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Royal Circlet", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Ezomyte Circlet", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Helment));

            ItemTree.Add("Rusted Helmet", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Flanged Helmet", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Ribbed Helmet", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Gilded Helmet", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Coolus Helmet", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Finned Helmet", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Ornate Helmet", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Polished Helmet", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Bladed Helmet", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Crested Helmet", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Helment));

            ItemTree.Add("Corroded Chain Coif", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Chainmail Coif", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Ringmail Coif", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Inlaid Chain Coif", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Woven Ring Coif", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Mesh Chain Coif", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Zealot's Chain Coif", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Commander's Ring Coif", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Crusader's Chain Coif", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Imperial Chain Coif", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Helment));

            ItemTree.Add("Tarnished Mask", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Wooden Mask", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Copper Mask", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Ceremonial Mask", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Bandit Mask", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Iron Mask", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Heathen Mask", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Assassin Mask", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Steel Mask", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Pagan Mask", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Helment));
            ItemTree.Add("Murder Mask", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Helment));
            #endregion
            #region Chest
            ItemTree.Add("Plate Vest", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Chestplate", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Copper Plate", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("War Plate", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Full Plate", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Arena Plate", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Lordly Plate", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Bronze Plate", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Battle Plate", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Sun Plate", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Colosseum Plate", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Majestic Plate", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Golden Plate", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Crusader Plate", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Astral Plate", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Gladiator Plate", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Glorious Plate", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Chest));


            ItemTree.Add("Shabby Jerkin", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Strapped Leather", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Buckskin Tunic", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Wild Leather", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Full Leather", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Sun Leather", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Thief's Garb", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Eelskin Tunic", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Frontier Leather", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Glorious Leather", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Coronal Leather", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Cutthroat's Garb", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Sharkskin Tunic", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Destiny Leather", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Exquisite Leather", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Zodiac Leather", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Assassin's Garb", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Chest));


            ItemTree.Add("Simple Robe", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Silken Vest", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Scholar's Robe", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Silken Garb", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Mage's Vestment", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Silk Robe", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Cabalist Regalia", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Sage's Robe", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Silken Wrap", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Conjurer's Vestment", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Spidersilk Robe", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Destroyer Regalia", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Savant's Robe", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Necromancer Silks", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Occultist's Vestment", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Widowsilk Robe", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Vaal Regalia", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Chest));

            ItemTree.Add("Scale Vest", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Light Brigandine", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Scale Doublet", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Infantry Brigandine", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Full Scale Armor", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Soldier's Brigandine", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Field Lamellar", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Wyrmscale Doublet", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Hussar Brigandine", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Full Wyrmscale", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Commander's Brigandine", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Battle Lamellar", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Dragonscale Doublet", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Desert Brigandine", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Full Dragonscale", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("General's Brigandine", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Triumphant Lamellar", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Chest));

            ItemTree.Add("Chainmail Vest", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Chainmail Tunic", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Ringmail Coat", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Chainmail Doublet", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Full Ringmail", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Full Chainmail", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Holy Chainmail", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Latticed Ringmail", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Crusader Chainmail", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Ornate Ringmail", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Chain Hauberk", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Devout Chainmail", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Loricated Ringmail", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Conquest Chainmail", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Elegant Ringmail", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Saint's Hauberk", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Chest));

            ItemTree.Add("Padded Vest", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Oiled Vest", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Padded Jacket", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Oiled Coat", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Scarlet Raiment", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Waxed Garb", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Bone Armor", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Quilted Jacket", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Sleek Coat", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Crimson Raiment", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Lacquered Garb", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Crypt Armor", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Sentinel Jacket", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Varnished Coat", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Blood Raiment", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Sadist Garb", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Chest));
            ItemTree.Add("Carnal Armor", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Chest));
            #endregion
            #region Gloves
            ItemTree.Add("Iron Gauntlets", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Plated Gauntlets", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Bronze Gauntlets", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Steel Gauntlets", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Antique Gauntlets", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Ancient Gauntlets", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Goliath Gauntlets", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Vaal Gauntlets", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Titan Gauntlets", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Gloves));

            ItemTree.Add("Rawhide Gloves", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Goathide Gloves", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Deerskin Gloves", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Nubuck Gloves", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Eelskin Gloves", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Sharkskin Gloves", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Shagreen Gloves", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Stealth Gloves", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Slink Gloves", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Gloves));

            ItemTree.Add("Wool Gloves", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Velvet Gloves", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Silk Gloves", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Embroidered Gloves", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Satin Gloves", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Samite Gloves", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Conjurer Gloves", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Arcanist Gloves", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Sorcerer Gloves", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Gloves));

            ItemTree.Add("Fishscale Gauntlets", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Ironscale Gauntlets", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Bronzescale Gauntlets", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Steelscale Gauntlets", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Serpentscale Gauntlets", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Wyrmscale Gauntlets", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Hydrascale Gauntlets", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Dragonscale Gauntlets", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Gloves));

            ItemTree.Add("Chain Gloves", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Ringmail Gloves", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Mesh Gloves", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Riveted Gloves", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Zealot Gloves", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Soldier Gloves", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Legion Gloves", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Crusader Gloves", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Gloves));

            ItemTree.Add("Wrapped Mitts", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Strapped Mitts", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Clasped Mitts", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Trapper Mitts", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Ambush Mitts", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Carnal Mitts", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Assassin's Mitts", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Gloves));
            ItemTree.Add("Murder Mitts", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Gloves));
            #endregion
            #region Boots
            ItemTree.Add("Iron Greaves", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Steel Greaves", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Plated Greaves", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Reinforced Greaves", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Antique Greaves", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Ancient Greaves", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Goliath Greaves", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Vaal Greaves", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Titan Greaves", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Boots));

            ItemTree.Add("Rawhide Boots", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Goathide Boots", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Deerskin Boots", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Nubuck Boots", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Eelskin Boots", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Sharkskin Boots", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Shagreen Boots", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Stealth Boots", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Slink Boots", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Boots));

            ItemTree.Add("Wool Shoes", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Velvet Slippers", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Silk Slippers", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Scholar Boots", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Satin Slippers", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Samite Slippers", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Conjurer Boots", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Arcanist Slippers", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Sorcerer Boots", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Boots));

            ItemTree.Add("Leatherscale Boots", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Ironscale Boots", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Bronzescale Boots", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Steelscale Boots", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Serpentscale Boots", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Wyrmscale Boots", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Hydrascale Boots", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Dragonscale Boots", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Boots));

            ItemTree.Add("Chain Boots", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Ringmail Boots", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Mesh Boots", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Riveted Boots", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Zealot Boots", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Soldier Boots", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Legion Boots", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Crusader Boots", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Boots));

            ItemTree.Add("Wrapped Boots", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Strapped Boots", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Clasped Boots", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Shackled Boots", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Trapper Boots", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Ambush Boots", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Carnal Boots", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Assassin's Boots", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Boots));
            ItemTree.Add("Murder Boots", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Boots));
            #endregion
            #region Shield
            ItemTree.Add("Splintered Tower Shield", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Corroded Tower Shield", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Rawhide Tower Shield", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Cedar Tower Shield", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Copper Tower Shield", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Reinforced Tower Shield", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Painted Tower Shield", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Buckskin Tower Shield", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Mahogany Tower Shield", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Bronze Tower Shield", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Girded Tower Shield", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Crested Tower Shield", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Shagreen Tower Shield", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Ebony Tower Shield", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Ezomyte Tower Shield", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Colossal Tower Shield", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Pinnacle Tower Shield", new CatSubCat(PoeItem.ClassIndex.Marauder, CatSubCat.InternalArmorCat.Shield));

            ItemTree.Add("Goathide Buckler", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Pine Buckler", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Painted Buckler", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Hammered Buckler", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("War Buckler", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Gilded Buckler", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Oak Buckler", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Enameled Buckler", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Corrugated Buckler", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Battle Buckler", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Golden Buckler", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Ironwood Buckler", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Lacquered Buckler", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Hunting Buckler", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Vaal Buckler", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Crusader Buckler", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Imperial Buckler", new CatSubCat(PoeItem.ClassIndex.Ranger, CatSubCat.InternalArmorCat.Shield));

            ItemTree.Add("Twig Spirit Shield", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Yew Spirit Shield", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Bone Spirit Shield", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Tarnished Spirit Shield", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Jingling Spirit Shield", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Brass Spirit Shield", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Walnut Spirit Shield", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Ivory Spirit Shield", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Ancient Spirit Shield", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Chiming Spirit Shield", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Thorium Spirit Shield", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Lacewood Spirit Shield", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Fossilized Spirit Shield", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Vaal Spirit Shield", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Harmonic Spirit Shield", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Titanium Spirit Shield", new CatSubCat(PoeItem.ClassIndex.Witch, CatSubCat.InternalArmorCat.Shield));


            ItemTree.Add("Rotted Round Shield", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Fir Round Shield", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Studded Round Shield", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Scarlet Round Shield", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Splendid Round Shield", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Maple Round Shield", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Spiked Round Shield", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Crimson Round Shield", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Baroque Round Shield", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Teak Round Shield", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Spiny Round Shield", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Cardinal Round Shield", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Elegant Round Shield", new CatSubCat(PoeItem.ClassIndex.Duelist, CatSubCat.InternalArmorCat.Shield));

            ItemTree.Add("Spiked Bundle", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Driftwood Spiked Shield", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Alloyed Spike Shield", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Burnished Spike Shield", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Ornate Spiked Shield", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Redwood Spiked Shield", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Compound Spike Shield", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Polished Spiked Shield", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Sovereign Spiked Shield", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Alder Spike Shield", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Ezomyte Spiked Shield", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Mirrored Spiked Shield", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Supreme Spiked Shield", new CatSubCat(PoeItem.ClassIndex.Templar, CatSubCat.InternalArmorCat.Shield));

            ItemTree.Add("Plank Kite Shield", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Linden Kite Shield", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Reinforced Kite Shield", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Layered Kite Shield", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Ceremonial Kite Shield", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Etched Shield", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Steel Kite Shield", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Laminated Kite Shield", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Angelic Kite Shield", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Branded Kite Shield", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Champion Kite Shield", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Mosaic Kite Shield", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Shield));
            ItemTree.Add("Archon Kite Shield", new CatSubCat(PoeItem.ClassIndex.Shadow, CatSubCat.InternalArmorCat.Shield));
            #endregion

            #region Quivers
            ItemTree.Add("Rugged Quiver", new CatSubCat(CatSubCat.InternalWeaponCat.Quiver));
            ItemTree.Add("Cured Quiver", new CatSubCat(CatSubCat.InternalWeaponCat.Quiver));
            ItemTree.Add("Conductive Quiver", new CatSubCat(CatSubCat.InternalWeaponCat.Quiver));
            ItemTree.Add("Heavy Quiver", new CatSubCat(CatSubCat.InternalWeaponCat.Quiver));
            ItemTree.Add("Light Quiver", new CatSubCat(CatSubCat.InternalWeaponCat.Quiver));
            #endregion
            #region Bows
            ItemTree.Add("Crude Bow", new CatSubCat(CatSubCat.InternalWeaponCat.Bow));
            ItemTree.Add("Short Bow", new CatSubCat(CatSubCat.InternalWeaponCat.Bow));
            ItemTree.Add("Long Bow", new CatSubCat(CatSubCat.InternalWeaponCat.Bow));
            ItemTree.Add("Composite Bow", new CatSubCat(CatSubCat.InternalWeaponCat.Bow));
            ItemTree.Add("Recurve Bow", new CatSubCat(CatSubCat.InternalWeaponCat.Bow));
            ItemTree.Add("Bone Bow", new CatSubCat(CatSubCat.InternalWeaponCat.Bow));
            ItemTree.Add("Royal Bow", new CatSubCat(CatSubCat.InternalWeaponCat.Bow));
            ItemTree.Add("Death Bow", new CatSubCat(CatSubCat.InternalWeaponCat.Bow));
            ItemTree.Add("Grove Bow", new CatSubCat(CatSubCat.InternalWeaponCat.Bow));
            ItemTree.Add("Decurve Bow", new CatSubCat(CatSubCat.InternalWeaponCat.Bow));
            ItemTree.Add("Compound Bow", new CatSubCat(CatSubCat.InternalWeaponCat.Bow));
            ItemTree.Add("Sniper Bow", new CatSubCat(CatSubCat.InternalWeaponCat.Bow));
            ItemTree.Add("Ivory Bow", new CatSubCat(CatSubCat.InternalWeaponCat.Bow));
            ItemTree.Add("Highborn Bow", new CatSubCat(CatSubCat.InternalWeaponCat.Bow));
            ItemTree.Add("Decimation Bow", new CatSubCat(CatSubCat.InternalWeaponCat.Bow));
            ItemTree.Add("Thicket Bow", new CatSubCat(CatSubCat.InternalWeaponCat.Bow));
            ItemTree.Add("Citadel Bow", new CatSubCat(CatSubCat.InternalWeaponCat.Bow));
            ItemTree.Add("Ranger Bow", new CatSubCat(CatSubCat.InternalWeaponCat.Bow));
            ItemTree.Add("Maraketh Bow", new CatSubCat(CatSubCat.InternalWeaponCat.Bow));
            ItemTree.Add("Spine Bow", new CatSubCat(CatSubCat.InternalWeaponCat.Bow));
            ItemTree.Add("Imperial Bow", new CatSubCat(CatSubCat.InternalWeaponCat.Bow));
            ItemTree.Add("Armageddon Bow", new CatSubCat(CatSubCat.InternalWeaponCat.Bow));
            #endregion
            #region Claws
            ItemTree.Add("Nailed Fist", new CatSubCat(CatSubCat.InternalWeaponCat.Claw));
            ItemTree.Add("Sharktooth Claw", new CatSubCat(CatSubCat.InternalWeaponCat.Claw));
            ItemTree.Add("Awl", new CatSubCat(CatSubCat.InternalWeaponCat.Claw));
            ItemTree.Add("Cat's Paw", new CatSubCat(CatSubCat.InternalWeaponCat.Claw));
            ItemTree.Add("Blinder", new CatSubCat(CatSubCat.InternalWeaponCat.Claw));
            ItemTree.Add("Timeworn Claw", new CatSubCat(CatSubCat.InternalWeaponCat.Claw));
            ItemTree.Add("Sparkling Claw", new CatSubCat(CatSubCat.InternalWeaponCat.Claw));
            ItemTree.Add("Fright Claw", new CatSubCat(CatSubCat.InternalWeaponCat.Claw));
            ItemTree.Add("Thresher Claw", new CatSubCat(CatSubCat.InternalWeaponCat.Claw));
            ItemTree.Add("Gouger", new CatSubCat(CatSubCat.InternalWeaponCat.Claw));
            ItemTree.Add("Tiger's Paw", new CatSubCat(CatSubCat.InternalWeaponCat.Claw));
            ItemTree.Add("Gut Ripper", new CatSubCat(CatSubCat.InternalWeaponCat.Claw));
            ItemTree.Add("Prehistoric Claw", new CatSubCat(CatSubCat.InternalWeaponCat.Claw));
            ItemTree.Add("Noble Claw", new CatSubCat(CatSubCat.InternalWeaponCat.Claw));
            ItemTree.Add("Eagle Claw", new CatSubCat(CatSubCat.InternalWeaponCat.Claw));
            ItemTree.Add("Great White Claw", new CatSubCat(CatSubCat.InternalWeaponCat.Claw));
            ItemTree.Add("Throat Stabber", new CatSubCat(CatSubCat.InternalWeaponCat.Claw));
            ItemTree.Add("Hellion's Paw", new CatSubCat(CatSubCat.InternalWeaponCat.Claw));
            ItemTree.Add("Eye Gouger", new CatSubCat(CatSubCat.InternalWeaponCat.Claw));
            ItemTree.Add("Vaal Claw", new CatSubCat(CatSubCat.InternalWeaponCat.Claw));
            ItemTree.Add("Imperial Claw", new CatSubCat(CatSubCat.InternalWeaponCat.Claw));
            ItemTree.Add("Terror Claw", new CatSubCat(CatSubCat.InternalWeaponCat.Claw));
            #endregion
            #region Sceptres (1H Maces)
            ItemTree.Add("Driftwood Sceptre", new CatSubCat(CatSubCat.InternalWeaponCat.Sceptre));
            ItemTree.Add("Darkwood Sceptre", new CatSubCat(CatSubCat.InternalWeaponCat.Sceptre));
            ItemTree.Add("Bronze Sceptre", new CatSubCat(CatSubCat.InternalWeaponCat.Sceptre));
            ItemTree.Add("Quartz Sceptre", new CatSubCat(CatSubCat.InternalWeaponCat.Sceptre));
            ItemTree.Add("Iron Sceptre", new CatSubCat(CatSubCat.InternalWeaponCat.Sceptre));
            ItemTree.Add("Ochre Sceptre", new CatSubCat(CatSubCat.InternalWeaponCat.Sceptre));
            ItemTree.Add("Ritual Sceptre", new CatSubCat(CatSubCat.InternalWeaponCat.Sceptre));
            ItemTree.Add("Shadow Sceptre", new CatSubCat(CatSubCat.InternalWeaponCat.Sceptre));
            ItemTree.Add("Grinning Fetish", new CatSubCat(CatSubCat.InternalWeaponCat.Sceptre));
            ItemTree.Add("Sekhem", new CatSubCat(CatSubCat.InternalWeaponCat.Sceptre));
            ItemTree.Add("Crystal Sceptre", new CatSubCat(CatSubCat.InternalWeaponCat.Sceptre));
            ItemTree.Add("Lead Sceptre", new CatSubCat(CatSubCat.InternalWeaponCat.Sceptre));
            ItemTree.Add("Blood Sceptre", new CatSubCat(CatSubCat.InternalWeaponCat.Sceptre));
            ItemTree.Add("Royal Sceptre", new CatSubCat(CatSubCat.InternalWeaponCat.Sceptre));
            ItemTree.Add("Abyssal Sceptre", new CatSubCat(CatSubCat.InternalWeaponCat.Sceptre));
            ItemTree.Add("Karui Sceptre", new CatSubCat(CatSubCat.InternalWeaponCat.Sceptre));
            ItemTree.Add("Tyrant's Sekhem", new CatSubCat(CatSubCat.InternalWeaponCat.Sceptre));
            ItemTree.Add("Opal Sceptre", new CatSubCat(CatSubCat.InternalWeaponCat.Sceptre));
            ItemTree.Add("Platinum Sceptre", new CatSubCat(CatSubCat.InternalWeaponCat.Sceptre));
            ItemTree.Add("Carnal Sceptre", new CatSubCat(CatSubCat.InternalWeaponCat.Sceptre));
            ItemTree.Add("Vaal Sceptre", new CatSubCat(CatSubCat.InternalWeaponCat.Sceptre));
            ItemTree.Add("Void Sceptre", new CatSubCat(CatSubCat.InternalWeaponCat.Sceptre));
            #endregion
            #region Maces (1H/2H)
            ItemTree.Add("Driftwood Club", new CatSubCat(CatSubCat.InternalWeaponCat.Mace1H));
            ItemTree.Add("Tribal Club", new CatSubCat(CatSubCat.InternalWeaponCat.Mace1H));
            ItemTree.Add("Spiked Club", new CatSubCat(CatSubCat.InternalWeaponCat.Mace1H));
            ItemTree.Add("Stone Hammer", new CatSubCat(CatSubCat.InternalWeaponCat.Mace1H));
            ItemTree.Add("War Hammer", new CatSubCat(CatSubCat.InternalWeaponCat.Mace1H));
            ItemTree.Add("Bladed Mace", new CatSubCat(CatSubCat.InternalWeaponCat.Mace1H));
            ItemTree.Add("Ceremonial Mace", new CatSubCat(CatSubCat.InternalWeaponCat.Mace1H));
            ItemTree.Add("Dream Mace", new CatSubCat(CatSubCat.InternalWeaponCat.Mace1H));
            ItemTree.Add("Barbed Club", new CatSubCat(CatSubCat.InternalWeaponCat.Mace1H));
            ItemTree.Add("Rock Breaker", new CatSubCat(CatSubCat.InternalWeaponCat.Mace1H));
            ItemTree.Add("Battle Hammer", new CatSubCat(CatSubCat.InternalWeaponCat.Mace1H));
            ItemTree.Add("Flanged Mace", new CatSubCat(CatSubCat.InternalWeaponCat.Mace1H));
            ItemTree.Add("Ornate Mace", new CatSubCat(CatSubCat.InternalWeaponCat.Mace1H));
            ItemTree.Add("Phantom Mace", new CatSubCat(CatSubCat.InternalWeaponCat.Mace1H));
            ItemTree.Add("Ancestral Club", new CatSubCat(CatSubCat.InternalWeaponCat.Mace1H));
            ItemTree.Add("Tenderizer", new CatSubCat(CatSubCat.InternalWeaponCat.Mace1H));
            ItemTree.Add("Gavel", new CatSubCat(CatSubCat.InternalWeaponCat.Mace1H));
            ItemTree.Add("Legion Hammer", new CatSubCat(CatSubCat.InternalWeaponCat.Mace1H));
            ItemTree.Add("Pernarch", new CatSubCat(CatSubCat.InternalWeaponCat.Mace1H));
            ItemTree.Add("Auric Mace", new CatSubCat(CatSubCat.InternalWeaponCat.Mace1H));
            ItemTree.Add("Nightmare Mace", new CatSubCat(CatSubCat.InternalWeaponCat.Mace1H));

            ItemTree.Add("Driftwood Maul", new CatSubCat(CatSubCat.InternalWeaponCat.Mace2H));
            ItemTree.Add("Tribal Maul", new CatSubCat(CatSubCat.InternalWeaponCat.Mace2H));
            ItemTree.Add("Great Mallet", new CatSubCat(CatSubCat.InternalWeaponCat.Mace2H));
            ItemTree.Add("Mallet", new CatSubCat(CatSubCat.InternalWeaponCat.Mace2H));
            ItemTree.Add("Sledgehammer", new CatSubCat(CatSubCat.InternalWeaponCat.Mace2H));
            ItemTree.Add("Spiked Maul", new CatSubCat(CatSubCat.InternalWeaponCat.Mace2H));
            ItemTree.Add("Brass Maul", new CatSubCat(CatSubCat.InternalWeaponCat.Mace2H));
            ItemTree.Add("Fright Maul", new CatSubCat(CatSubCat.InternalWeaponCat.Mace2H));
            ItemTree.Add("Totemic Maul", new CatSubCat(CatSubCat.InternalWeaponCat.Mace2H)); 
            ItemTree.Add("Steelhead", new CatSubCat(CatSubCat.InternalWeaponCat.Mace2H));
            ItemTree.Add("Spiny Maul", new CatSubCat(CatSubCat.InternalWeaponCat.Mace2H));
            ItemTree.Add("Plated Maul", new CatSubCat(CatSubCat.InternalWeaponCat.Mace2H));
            ItemTree.Add("Dread Maul", new CatSubCat(CatSubCat.InternalWeaponCat.Mace2H));
            ItemTree.Add("Karui Maul", new CatSubCat(CatSubCat.InternalWeaponCat.Mace2H));
            ItemTree.Add("Colossus Mallet", new CatSubCat(CatSubCat.InternalWeaponCat.Mace2H));
            ItemTree.Add("Piledriver", new CatSubCat(CatSubCat.InternalWeaponCat.Mace2H));
            ItemTree.Add("Meatgrinder", new CatSubCat(CatSubCat.InternalWeaponCat.Mace2H));
            ItemTree.Add("Imperial Maul", new CatSubCat(CatSubCat.InternalWeaponCat.Mace2H));
            ItemTree.Add("Terror Maul", new CatSubCat(CatSubCat.InternalWeaponCat.Mace2H));
            
            Dictionary<string, CatSubCat> CatByProperty = new Dictionary<string, CatSubCat>();
            CatByProperty.Add("One Handed Mace", new CatSubCat(CatSubCat.InternalWeaponCat.Mace1H));
            CatByProperty.Add("Two Handed Mace", new CatSubCat(CatSubCat.InternalWeaponCat.Mace2H));
            ItemTree.Add("Petrified Club", new CatSubCat(CatByProperty));
            #endregion
            #region Axes(1H)
            ItemTree.Add("Rusted Hatchet", new CatSubCat(CatSubCat.InternalWeaponCat.Axe1H));
            ItemTree.Add("Jade Hatchet", new CatSubCat(CatSubCat.InternalWeaponCat.Axe1H));
            ItemTree.Add("Boarding Axe", new CatSubCat(CatSubCat.InternalWeaponCat.Axe1H));
            ItemTree.Add("Cleaver", new CatSubCat(CatSubCat.InternalWeaponCat.Axe1H));
            ItemTree.Add("Broad Axe", new CatSubCat(CatSubCat.InternalWeaponCat.Axe1H));
            ItemTree.Add("Arming Axe", new CatSubCat(CatSubCat.InternalWeaponCat.Axe1H));
            ItemTree.Add("Decorative Axe", new CatSubCat(CatSubCat.InternalWeaponCat.Axe1H));
            ItemTree.Add("Spectral Axe", new CatSubCat(CatSubCat.InternalWeaponCat.Axe1H));
            ItemTree.Add("Jasper Axe", new CatSubCat(CatSubCat.InternalWeaponCat.Axe1H));
            ItemTree.Add("Tomahawk", new CatSubCat(CatSubCat.InternalWeaponCat.Axe1H));
            ItemTree.Add("Wrist Chopper", new CatSubCat(CatSubCat.InternalWeaponCat.Axe1H));
            ItemTree.Add("War Axe", new CatSubCat(CatSubCat.InternalWeaponCat.Axe1H));
            ItemTree.Add("Chest Splitter", new CatSubCat(CatSubCat.InternalWeaponCat.Axe1H));
            ItemTree.Add("Ceremonial Axe", new CatSubCat(CatSubCat.InternalWeaponCat.Axe1H));
            ItemTree.Add("Wraith Axe", new CatSubCat(CatSubCat.InternalWeaponCat.Axe1H));
            ItemTree.Add("Karui Axe", new CatSubCat(CatSubCat.InternalWeaponCat.Axe1H));
            ItemTree.Add("Siege Axe", new CatSubCat(CatSubCat.InternalWeaponCat.Axe1H));
            ItemTree.Add("Reaver Axe", new CatSubCat(CatSubCat.InternalWeaponCat.Axe1H));
            ItemTree.Add("Butcher Axe", new CatSubCat(CatSubCat.InternalWeaponCat.Axe1H));
            ItemTree.Add("Vaal Hatchet", new CatSubCat(CatSubCat.InternalWeaponCat.Axe1H));
            ItemTree.Add("Royal Axe", new CatSubCat(CatSubCat.InternalWeaponCat.Axe1H));
            ItemTree.Add("Infernal Axe", new CatSubCat(CatSubCat.InternalWeaponCat.Axe1H));
            #endregion
            #region Axes (2H)
            ItemTree.Add("Stone Axe",new CatSubCat(CatSubCat.InternalWeaponCat.Axe2H));
            ItemTree.Add("Jade Chopper",new CatSubCat(CatSubCat.InternalWeaponCat.Axe2H));
            ItemTree.Add("Woodsplitter",new CatSubCat(CatSubCat.InternalWeaponCat.Axe2H));
            ItemTree.Add("Poleaxe",new CatSubCat(CatSubCat.InternalWeaponCat.Axe2H));
            ItemTree.Add("Double Axe",new CatSubCat(CatSubCat.InternalWeaponCat.Axe2H));
            ItemTree.Add("Gilded Axe",new CatSubCat(CatSubCat.InternalWeaponCat.Axe2H));
            ItemTree.Add("Shadow Axe",new CatSubCat(CatSubCat.InternalWeaponCat.Axe2H));
            ItemTree.Add("Jasper Chopper",new CatSubCat(CatSubCat.InternalWeaponCat.Axe2H));
            ItemTree.Add("Timber Axe",new CatSubCat(CatSubCat.InternalWeaponCat.Axe2H));
            ItemTree.Add("Headsman Axe",new CatSubCat(CatSubCat.InternalWeaponCat.Axe2H));
            ItemTree.Add("Labrys",new CatSubCat(CatSubCat.InternalWeaponCat.Axe2H));
            ItemTree.Add("Noble Axe",new CatSubCat(CatSubCat.InternalWeaponCat.Axe2H));
            ItemTree.Add("Abyssal Axe",new CatSubCat(CatSubCat.InternalWeaponCat.Axe2H));
            ItemTree.Add("Karui Chopper",new CatSubCat(CatSubCat.InternalWeaponCat.Axe2H));
            ItemTree.Add("Sundering Axe",new CatSubCat(CatSubCat.InternalWeaponCat.Axe2H));
            ItemTree.Add("Ezomyte Axe",new CatSubCat(CatSubCat.InternalWeaponCat.Axe2H));
            ItemTree.Add("Vaal Axe",new CatSubCat(CatSubCat.InternalWeaponCat.Axe2H));
            ItemTree.Add("Despot Axe",new CatSubCat(CatSubCat.InternalWeaponCat.Axe2H));
            ItemTree.Add("Void Axe", new CatSubCat(CatSubCat.InternalWeaponCat.Axe2H));
            #endregion
            #region Rapiers
            ItemTree.Add("Rusted Spike",new CatSubCat(CatSubCat.InternalWeaponCat.Rapier));
            ItemTree.Add("Whalebone Rapier",new CatSubCat(CatSubCat.InternalWeaponCat.Rapier));
            ItemTree.Add("Battered Foil",new CatSubCat(CatSubCat.InternalWeaponCat.Rapier));
            ItemTree.Add("Basket Rapier",new CatSubCat(CatSubCat.InternalWeaponCat.Rapier));
            ItemTree.Add("Jagged Foil",new CatSubCat(CatSubCat.InternalWeaponCat.Rapier));
            ItemTree.Add("Antique Rapier",new CatSubCat(CatSubCat.InternalWeaponCat.Rapier));
            ItemTree.Add("Elegant Foil",new CatSubCat(CatSubCat.InternalWeaponCat.Rapier));
            ItemTree.Add("Thorn Rapier",new CatSubCat(CatSubCat.InternalWeaponCat.Rapier));
            ItemTree.Add("Wyrmbone Rapier",new CatSubCat(CatSubCat.InternalWeaponCat.Rapier));
            ItemTree.Add("Burnished Foil",new CatSubCat(CatSubCat.InternalWeaponCat.Rapier));
            ItemTree.Add("Estoc",new CatSubCat(CatSubCat.InternalWeaponCat.Rapier));
            ItemTree.Add("Serrated Foil",new CatSubCat(CatSubCat.InternalWeaponCat.Rapier));
            ItemTree.Add("Primeval Rapier",new CatSubCat(CatSubCat.InternalWeaponCat.Rapier));
            ItemTree.Add("Fancy Foil",new CatSubCat(CatSubCat.InternalWeaponCat.Rapier));
            ItemTree.Add("Apex Rapier",new CatSubCat(CatSubCat.InternalWeaponCat.Rapier));
            ItemTree.Add("Dragonbone Rapier",new CatSubCat(CatSubCat.InternalWeaponCat.Rapier));
            ItemTree.Add("Tempered Foil",new CatSubCat(CatSubCat.InternalWeaponCat.Rapier));
            ItemTree.Add("Pecoraro",new CatSubCat(CatSubCat.InternalWeaponCat.Rapier));
            ItemTree.Add("Spiraled Foil",new CatSubCat(CatSubCat.InternalWeaponCat.Rapier));
            ItemTree.Add("Vaal Rapier",new CatSubCat(CatSubCat.InternalWeaponCat.Rapier));
            ItemTree.Add("Jeweled Foil",new CatSubCat(CatSubCat.InternalWeaponCat.Rapier));
            ItemTree.Add("Harpy Rapier", new CatSubCat(CatSubCat.InternalWeaponCat.Rapier));
            #endregion
            #region Swords (1H)
            ItemTree.Add("Rusted Sword",new CatSubCat(CatSubCat.InternalWeaponCat.Sword1H));
            ItemTree.Add("Copper Sword",new CatSubCat(CatSubCat.InternalWeaponCat.Sword1H));
            ItemTree.Add("Sabre",new CatSubCat(CatSubCat.InternalWeaponCat.Sword1H));
            ItemTree.Add("Broad Sword",new CatSubCat(CatSubCat.InternalWeaponCat.Sword1H));
            ItemTree.Add("War Sword",new CatSubCat(CatSubCat.InternalWeaponCat.Sword1H));
            ItemTree.Add("Ancient Sword",new CatSubCat(CatSubCat.InternalWeaponCat.Sword1H));
            ItemTree.Add("Elegant Sword",new CatSubCat(CatSubCat.InternalWeaponCat.Sword1H));
            ItemTree.Add("Dusk Blade",new CatSubCat(CatSubCat.InternalWeaponCat.Sword1H));
            ItemTree.Add("Variscite Blade",new CatSubCat(CatSubCat.InternalWeaponCat.Sword1H));
            ItemTree.Add("Cutlass",new CatSubCat(CatSubCat.InternalWeaponCat.Sword1H));
            ItemTree.Add("Baselard",new CatSubCat(CatSubCat.InternalWeaponCat.Sword1H));
            ItemTree.Add("Elder Sword",new CatSubCat(CatSubCat.InternalWeaponCat.Sword1H));
            ItemTree.Add("Graceful Sword",new CatSubCat(CatSubCat.InternalWeaponCat.Sword1H));
            ItemTree.Add("Twilight Blade",new CatSubCat(CatSubCat.InternalWeaponCat.Sword1H));
            ItemTree.Add("Gemstone Sword",new CatSubCat(CatSubCat.InternalWeaponCat.Sword1H));
            ItemTree.Add("Corsair Sword",new CatSubCat(CatSubCat.InternalWeaponCat.Sword1H));
            ItemTree.Add("Gladius",new CatSubCat(CatSubCat.InternalWeaponCat.Sword1H));
            ItemTree.Add("Legion Sword",new CatSubCat(CatSubCat.InternalWeaponCat.Sword1H));
            ItemTree.Add("Vaal Blade",new CatSubCat(CatSubCat.InternalWeaponCat.Sword1H));
            ItemTree.Add("Eternal Sword",new CatSubCat(CatSubCat.InternalWeaponCat.Sword1H));
            ItemTree.Add("Midnight Blade", new CatSubCat(CatSubCat.InternalWeaponCat.Sword1H));
            ItemTree.Add("Battle Sword", new CatSubCat(CatSubCat.InternalWeaponCat.Sword1H));
            #endregion
            #region Swords (2H)
            ItemTree.Add("Corroded Blade", new CatSubCat(CatSubCat.InternalWeaponCat.Sword2H));
            ItemTree.Add("Long Sword", new CatSubCat(CatSubCat.InternalWeaponCat.Sword2H));
            ItemTree.Add("Bastard Sword", new CatSubCat(CatSubCat.InternalWeaponCat.Sword2H));
            ItemTree.Add("Two-Handed Sword", new CatSubCat(CatSubCat.InternalWeaponCat.Sword2H));
            ItemTree.Add("Etched Greatsword", new CatSubCat(CatSubCat.InternalWeaponCat.Sword2H));
            ItemTree.Add("Ornate Sword", new CatSubCat(CatSubCat.InternalWeaponCat.Sword2H));
            ItemTree.Add("Spectral Sword", new CatSubCat(CatSubCat.InternalWeaponCat.Sword2H));
            ItemTree.Add("Butcher Sword", new CatSubCat(CatSubCat.InternalWeaponCat.Sword2H));
            ItemTree.Add("Footman Sword", new CatSubCat(CatSubCat.InternalWeaponCat.Sword2H));
            ItemTree.Add("Highland Blade", new CatSubCat(CatSubCat.InternalWeaponCat.Sword2H));
            ItemTree.Add("Engraved Greatsword", new CatSubCat(CatSubCat.InternalWeaponCat.Sword2H));
            ItemTree.Add("Tiger Sword", new CatSubCat(CatSubCat.InternalWeaponCat.Sword2H));
            ItemTree.Add("Wraith Sword", new CatSubCat(CatSubCat.InternalWeaponCat.Sword2H));
            ItemTree.Add("Headman's Sword", new CatSubCat(CatSubCat.InternalWeaponCat.Sword2H));
            ItemTree.Add("Reaver Sword", new CatSubCat(CatSubCat.InternalWeaponCat.Sword2H));
            ItemTree.Add("Ezomyte Sword", new CatSubCat(CatSubCat.InternalWeaponCat.Sword2H));
            ItemTree.Add("Vaal Greatsword", new CatSubCat(CatSubCat.InternalWeaponCat.Sword2H));
            ItemTree.Add("Lion Sword", new CatSubCat(CatSubCat.InternalWeaponCat.Sword2H));
            ItemTree.Add("Infernal Sword", new CatSubCat(CatSubCat.InternalWeaponCat.Sword2H));
            #endregion
            #region Daggers
            ItemTree.Add("Glass Shank",new CatSubCat(CatSubCat.InternalWeaponCat.Dagger));
            ItemTree.Add("Skinning Knife",new CatSubCat(CatSubCat.InternalWeaponCat.Dagger));
            ItemTree.Add("Carving Knife",new CatSubCat(CatSubCat.InternalWeaponCat.Dagger));
            ItemTree.Add("Stiletto",new CatSubCat(CatSubCat.InternalWeaponCat.Dagger));
            ItemTree.Add("Boot Knife",new CatSubCat(CatSubCat.InternalWeaponCat.Dagger));
            ItemTree.Add("Copper Kris",new CatSubCat(CatSubCat.InternalWeaponCat.Dagger));
            ItemTree.Add("Skean",new CatSubCat(CatSubCat.InternalWeaponCat.Dagger));
            ItemTree.Add("Imp Dagger",new CatSubCat(CatSubCat.InternalWeaponCat.Dagger));
            ItemTree.Add("Flaying Knife",new CatSubCat(CatSubCat.InternalWeaponCat.Dagger));
            ItemTree.Add("Butcher Knife",new CatSubCat(CatSubCat.InternalWeaponCat.Dagger));
            ItemTree.Add("Poignard",new CatSubCat(CatSubCat.InternalWeaponCat.Dagger));
            ItemTree.Add("Boot Blade",new CatSubCat(CatSubCat.InternalWeaponCat.Dagger));
            ItemTree.Add("Golden Kris",new CatSubCat(CatSubCat.InternalWeaponCat.Dagger));
            ItemTree.Add("Royal Skean",new CatSubCat(CatSubCat.InternalWeaponCat.Dagger));
            ItemTree.Add("Fiend Dagger",new CatSubCat(CatSubCat.InternalWeaponCat.Dagger));
            ItemTree.Add("Gutting Knife",new CatSubCat(CatSubCat.InternalWeaponCat.Dagger));
            ItemTree.Add("Slaughter Knife",new CatSubCat(CatSubCat.InternalWeaponCat.Dagger));
            ItemTree.Add("Ambusher",new CatSubCat(CatSubCat.InternalWeaponCat.Dagger));
            ItemTree.Add("Ezomyte Dagger",new CatSubCat(CatSubCat.InternalWeaponCat.Dagger));
            ItemTree.Add("Platinum Kris",new CatSubCat(CatSubCat.InternalWeaponCat.Dagger));
            ItemTree.Add("Imperial Skean",new CatSubCat(CatSubCat.InternalWeaponCat.Dagger));
            ItemTree.Add("Demon Dagger", new CatSubCat(CatSubCat.InternalWeaponCat.Dagger));
            #endregion
            #region Staffs
            ItemTree.Add("Gnarled Branch",new CatSubCat(CatSubCat.InternalWeaponCat.Staff));
            ItemTree.Add("Primitive Staff",new CatSubCat(CatSubCat.InternalWeaponCat.Staff));
            ItemTree.Add("Long Staff",new CatSubCat(CatSubCat.InternalWeaponCat.Staff));
            ItemTree.Add("Iron Staff",new CatSubCat(CatSubCat.InternalWeaponCat.Staff));
            ItemTree.Add("Coiled Staff",new CatSubCat(CatSubCat.InternalWeaponCat.Staff));
            ItemTree.Add("Royal Staff",new CatSubCat(CatSubCat.InternalWeaponCat.Staff));
            ItemTree.Add("Vile Staff",new CatSubCat(CatSubCat.InternalWeaponCat.Staff));
            ItemTree.Add("Woodful Staff",new CatSubCat(CatSubCat.InternalWeaponCat.Staff));
            ItemTree.Add("Quarterstaff",new CatSubCat(CatSubCat.InternalWeaponCat.Staff));
            ItemTree.Add("Military Staff",new CatSubCat(CatSubCat.InternalWeaponCat.Staff));
            ItemTree.Add("Serpentine Staff",new CatSubCat(CatSubCat.InternalWeaponCat.Staff));
            ItemTree.Add("Highborn Staff",new CatSubCat(CatSubCat.InternalWeaponCat.Staff));
            ItemTree.Add("Foul Staff",new CatSubCat(CatSubCat.InternalWeaponCat.Staff));
            ItemTree.Add("Primordial Staff",new CatSubCat(CatSubCat.InternalWeaponCat.Staff));
            ItemTree.Add("Lathi",new CatSubCat(CatSubCat.InternalWeaponCat.Staff));
            ItemTree.Add("Ezoymte Staff",new CatSubCat(CatSubCat.InternalWeaponCat.Staff));
            ItemTree.Add("Maelström Staff",new CatSubCat(CatSubCat.InternalWeaponCat.Staff));
            ItemTree.Add("Imperial Staff",new CatSubCat(CatSubCat.InternalWeaponCat.Staff));
            ItemTree.Add("Judgement Staff", new CatSubCat(CatSubCat.InternalWeaponCat.Staff));
            #endregion
            #region Wands
            ItemTree.Add("Driftwood Wand",new CatSubCat(CatSubCat.InternalWeaponCat.Wand));
            ItemTree.Add("Goat's Horn",new CatSubCat(CatSubCat.InternalWeaponCat.Wand));
            ItemTree.Add("Carved Wand",new CatSubCat(CatSubCat.InternalWeaponCat.Wand));
            ItemTree.Add("Quartz Wand",new CatSubCat(CatSubCat.InternalWeaponCat.Wand));
            ItemTree.Add("Spiraled Wand",new CatSubCat(CatSubCat.InternalWeaponCat.Wand));
            ItemTree.Add("Sage Wand",new CatSubCat(CatSubCat.InternalWeaponCat.Wand));
            ItemTree.Add("Faun's Horn",new CatSubCat(CatSubCat.InternalWeaponCat.Wand));
            ItemTree.Add("Engraved Wand",new CatSubCat(CatSubCat.InternalWeaponCat.Wand));
            ItemTree.Add("Crystal Wand",new CatSubCat(CatSubCat.InternalWeaponCat.Wand));
            ItemTree.Add("Serpent Wand",new CatSubCat(CatSubCat.InternalWeaponCat.Wand));
            ItemTree.Add("Omen Wand",new CatSubCat(CatSubCat.InternalWeaponCat.Wand));
            ItemTree.Add("Demon's Horn",new CatSubCat(CatSubCat.InternalWeaponCat.Wand));
            ItemTree.Add("Imbued Wand",new CatSubCat(CatSubCat.InternalWeaponCat.Wand));
            ItemTree.Add("Opal Wand",new CatSubCat(CatSubCat.InternalWeaponCat.Wand));
            ItemTree.Add("Tornado Wand",new CatSubCat(CatSubCat.InternalWeaponCat.Wand));
            ItemTree.Add("Prophecy Wand",new CatSubCat(CatSubCat.InternalWeaponCat.Wand));
            #endregion
        }

        public static CatSubCat Find(PoeItem i)
        {

            try
            {
                string typeLine = i.TypeLine;
                if (i.FrameType == PoeItem.FrameTypeIndex.Unique)
                    return UniqueCat;
                if (typeLine.StartsWith("Superior "))
                    typeLine = typeLine.Remove(0, 9);

                if (ItemTree.ContainsKey(typeLine))
                {
                    var csc = ItemTree[typeLine];
                    if (csc.MultiIndexByProperty != null)
                    {
                        if (i.ItemProperties.Count == 0)
                            return DefaultCat;

                        string firstProperty = i.ItemProperties.First().Name;
                        if (csc.MultiIndexByProperty.ContainsKey(firstProperty))
                            return csc.MultiIndexByProperty[firstProperty];
                        else
                            return DefaultCat;
                    }
                    else
                        return csc;
                }

                if (i.TypeLine.EndsWith("Belt") || i.TypeLine == "Rustic Sash")
                    return BeltCat;
                else if (i.TypeLine.EndsWith("Ring"))
                    return RingCat;
                else if (i.TypeLine.EndsWith("Amulet"))
                    return AmuletCat;

                if (i.ItemProperties.Count != 0 && i.ItemProperties[0].Name == "Map Level")
                    return MapCat;
                if (i.TypeLine.Contains("Life Flask"))
                    return HpCat;
                if (i.TypeLine.Contains("Mana Flask"))
                    return ManaCat;
                if (i.TypeLine.Contains("Flask"))
                    return OtherCat;
            }
            catch (Exception)
            {
            }
            return null;
        }

        public class CatSubCat
        {
            internal enum InternalArmorCat
            { 
                Boots,
                Chest,
                Helment,
                Gloves,
                Shield
            }
            
            internal enum InternalWeaponCat
            {
                Mace1H,
                Mace2H,
                Axe1H,
                Axe2H,
                Sword1H,
                Sword2H,
                Bow,
                Wand,
                Claw,
                Staff,
                Dagger,
                Quiver,
                Sceptre,
                Rapier,
                LENGTH
            }

            //TODO: Move category initializers to static constructor.
            private readonly int[] InternalArmorCatOffset = { 2, 3, 1, 4, 2, 2 };
            private readonly PoeItem.ClassIndex[] WeaponCatToClassIndex =  { PoeItem.ClassIndex.Marauder,  //1H Mace
                                                                             PoeItem.ClassIndex.Marauder,  //2H Mace
                                                                             PoeItem.ClassIndex.Duelist,   //1H Axe 
                                                                             PoeItem.ClassIndex.Duelist,   //2H Axe 
                                                                             PoeItem.ClassIndex.Duelist,   //1H Sword
                                                                             PoeItem.ClassIndex.Duelist,   //2H Sword
                                                                             PoeItem.ClassIndex.Ranger,    //Bow
                                                                             PoeItem.ClassIndex.Witch,     //Wand
                                                                             PoeItem.ClassIndex.Shadow,    //Claw
                                                                             PoeItem.ClassIndex.Templar,   //Staff
                                                                             PoeItem.ClassIndex.Shadow,    //Dagger
                                                                             PoeItem.ClassIndex.Ranger,    //Quiver
                                                                             PoeItem.ClassIndex.Templar,   //Sceptre
                                                                             PoeItem.ClassIndex.Ranger };  //Rapier
            private readonly int[] WeaponCatToCatIndex =    { 0,   //1H Mace
                                                              1,   //2H Mace
                                                              0,   //1H Axe 
                                                              1,   //2H Axe 
                                                              2,   //1H Sword 
                                                              3,   //2H Sword
                                                              0,   //Bow
                                                              0,   //Wand
                                                              0,   //Claw
                                                              1,   //Staff
                                                              1,   //Dagger
                                                              2,   //Quiver
                                                              0,   //Sceptre
                                                              1 }; //Rapier

            public PoeItem.ClassIndex ClassIndex { get; private set; }
            public int SlotIndex { get; private set; }
            public Dictionary<string, CatSubCat> MultiIndexByProperty = null;
            static CatSubCat()
            { 
            
            }
            public CatSubCat(PoeItem.ClassIndex classIndex = PoeItem.ClassIndex.Misc, int slotIndex = 0)
            {
                ClassIndex = classIndex;
                SlotIndex = slotIndex;
            }

            internal CatSubCat(PoeItem.ClassIndex classIndex, InternalArmorCat cat)
            {
                ClassIndex = classIndex;
                SlotIndex = (int)cat + InternalArmorCatOffset[(int)ClassIndex];
            }
            internal CatSubCat(InternalWeaponCat cat)
            {
                ClassIndex = WeaponCatToClassIndex[(int)cat];
                SlotIndex = WeaponCatToCatIndex[(int)cat];
            }
            internal CatSubCat(Dictionary<string, CatSubCat> multiIndex)
            {
                MultiIndexByProperty = multiIndex;
            }
        }
    }
}
