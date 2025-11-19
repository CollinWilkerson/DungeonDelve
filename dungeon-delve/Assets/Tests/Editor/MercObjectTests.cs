using UnityEngine;
using NUnit.Framework;

public class MercObjectTests 
{
    MercObject testMerc;
    Equipment testArmor;
    Equipment testWeapon;
    Equipment negativeEquipment;

    [SetUp]
    public void SetUp()
    {
        DataFiles.Heroes = "Class 0,Health 1,Damage 2,Speed 3,AbilityText 4,FlavorText 5,Cost 6,Filepath 7,TavernFilepath 8,Warrior 9,Ranger 10,Mage 11\r\nHuman Warrior,8,1,2,,Anyone with a sword can do something,5,HumanHeroes/HumanWarrior,HumanHeroes/Tavern/HumanWarrior,1,0,0\r\nHuman Ranger,4,2,3,,Just be glad he can hit things,5,HumanHeroes/HumanRanger,HumanHeroes/Tavern/HumanRanger,0,1,0\r\nHuman Mage,4,8,1,,Incredible that your guild attracts people who can read,5,HumanHeroes/HumanMage,HumanHeroes/Tavern/HumanMage,0,0,1\r\nWeapon Master,8,4,4,,Years of training make the masters of blade bow and tome alike,20,Legends/WeaponMaster,Legends/Tavern/WeaponMaster,1,1,1\r\nArcane Ranger,4,8,6,,They move swiftly through unnatural shadows,20,Legends/ArcaneRanger,Legends/Tavern/ArcaneRanger,0,1,1\r\nPaladin,24,2,2,-1 Damage from all sources,The bastion of a good team,30,Legends/Paladin,Legends/Tavern/Paladin,1,0,1\r\nBlack Knight,24,8,2,,Sacrifice has turned him into a killing machine,40,Legends/BlackKnight,Legends/Tavern/BlackKnight,2,0,0\r\nAssassin,8,4,10,Assassin has a 1 in 2 chance to receive no damage,If you hear an abnormal amount of nothing - watch yourself,40,Legends/Assassin,Legends/Tavern/Assassin,1,1,0\r\nFinal Magus,10,20,10,,The laws of the universe are simply suggestions to him,80,Legends/FinalMagus,Legends/Tavern/FinalMagus,0,0,2".Split("\r\n");
        DataFiles.Eq = "Name,Type,Class,HP,DMG,SPD,SpritePath\r\nPool Noodle,Weapon,Warrior,0,1,1,EqSprites/eqSpriteNotFound\r\nCool Stick,Weapon,Warrior,0,2,0,EqSprites/eqSpriteNotFound\r\nShortbow,Weapon,Ranger,0,1,1,EqSprites/eqSpriteNotFound\r\nLongbow,Weapon,Ranger,0,2,0,EqSprites/eqSpriteNotFound\r\nfushigi,Weapon,Mage,0,1,1,EqSprites/eqSpriteNotFound\r\nStaff,Weapon,Mage,0,2,0,EqSprites/eqSpriteNotFound\r\nMacoroni Platemail,Armor,Warrior,8,0,-1,EqSprites/eqSpriteNotFound\r\nPaper armor,Armor,Warrior,4,0,1,EqSprites/eqSpriteNotFound\r\nCowl of shadow,Armor,Ranger,2,0,2,EqSprites/eqSpriteNotFound\r\n\"\"\"Cool\"\" bandages\",Armor,Ranger,4,0,1,EqSprites/eqSpriteNotFound\r\nBrand Tshirt,Armor,Mage,0,2,1,EqSprites/eqSpriteNotFound\r\nBath towel,Armor,Mage,2,1,1,EqSprites/eqSpriteNotFound\r\nBear hands,weapon,warrior,-2,2,2,EqSprites/eqSpriteNotFound\r\ndesk catapult,Weapon,Ranger,2,3,-1,EqSprites/eqSpriteNotFound\r\nPrincess wand,Weapon,Mage,-2,3,1,EqSprites/eqSpriteNotFound\r\nEmo piercings,Armor,Warrior,2,0,2,EqSprites/eqSpriteNotFound\r\nracing stripes,Armor,Ranger,0,-1,4,EqSprites/eqSpriteNotFound\r\nItchy sweater,Armor,Mage,-2,1,3,EqSprites/eqSpriteNotFound\r\nRave wand,Weapon,Mage,2,3,2,EqSprites/eqSpriteNotFound\r\nThesaurus,Weapon,Mage,0,3,3,EqSprites/eqSpriteNotFound\r\nKunai,Weapon,Ranger,0,2,4,EqSprites/eqSpriteNotFound\r\nThrowing axe,Weapon,Ranger,0,5,1,EqSprites/eqSpriteNotFound\r\nFoam Sword,Weapon,Warrior,2,4,1,EqSprites/eqSpriteNotFound\r\nPaper clip mail,Armor,Warrior,10,0,1,EqSprites/eqSpriteNotFound\r\nLoincloth,Armor,Warrior,-4,5,3,EqSprites/eqSpriteNotFound\r\nCardboard box,Armor,Ranger,6,0,3,EqSprites/eqSpriteNotFound\r\nCasual Robe,Armor,Mage,6,2,1,EqSprites/eqSpriteNotFound\r\ntrash can lid,Weapon,Warrior,12,-2,2,EqSprites/eqSpriteNotFound\r\nBarbed wire bow ,Weapon,Ranger,-4,3,5,EqSprites/eqSpriteNotFound\r\nBox fan,Weapon,Mage,-2,-2,9,EqSprites/eqSpriteNotFound\r\nTrash can,Armor,Warrior,6,1,2,EqSprites/eqSpriteNotFound\r\nMocap suit,Armor,Ranger,2,3,2,EqSprites/eqSpriteNotFound\r\nHoly robe,Armor,Mage,6,-2,5,EqSprites/eqSpriteNotFound\r\nClaymore,Weapon,Warrior,12,10,-1,EqSprites/eqSpriteNotFound\r\nBlessed Platemail,Armor,Warrior,20,2,3,EqSprites/eqSpriteNotFound\r\nHand Balista,Weapon,Ranger,0,10,5,EqSprites/eqSpriteNotFound\r\nSimmer Cloak,Armor,Ranger,10,0,10,EqSprites/eqSpriteNotFound\r\nTomes of 9,Weapon,Mage,-10,15,5,EqSprites/eqSpriteNotFound\r\nSpirit Robes,Armor,Mage,10,10,0,EqSprites/eqSpriteNotFound\r\ncursed blade,Weapon,Warrior,-6,10,8,EqSprites/eqSpriteNotFound\r\n Chieftain's attire,Armor,Warrior,10,5,5,EqSprites/eqSpriteNotFound\r\nRepeater,Weapon,Ranger,0,5,10,EqSprites/eqSpriteNotFound\r\nSniper glasses,Armor,Ranger,6,10,2,EqSprites/eqSpriteNotFound\r\n North wind,Weapon,Mage,0,5,10,EqSprites/eqSpriteNotFound\r\n Equipment of Dispair,Weapon,Mage,-99,-99,-99,EqSprites/eqSpriteNotFound".Split("\r\n");
        testMerc = new MercObject(1); //Human Warrior
        testArmor = new Equipment(41); //Cheiftans attire
        testWeapon = new Equipment(23); //foam Sword
        negativeEquipment = new Equipment(45); //Eq that only exists in this script
    }

    [Test]
    public void AddheroToPartyAddsHeroToFirstOpenSlot()
    {
        MercObject.AddHeroToParty(testMerc);
        Assert.AreEqual(MercObject.Party[0], testMerc);
    }

    [Test]
    public void DeletePartyMemberSetsPartyIndexToNull()
    {
        MercObject.AddHeroToParty(testMerc);
        MercObject.DeletePartyMemeber(0);
        Assert.IsNull(MercObject.Party[0]);
    }

    [Test]
    public void GetTotalDamageReturnsSumOfHeroDamage()
    {
        MercObject secondMerc = new MercObject(2);
        MercObject.AddHeroToParty(testMerc);
        MercObject.AddHeroToParty(secondMerc);
        Assert.AreEqual(MercObject.GetTotalDamage(), secondMerc.GetDamage() + testMerc.GetDamage());
    }

    [Test]
    public void ClearPartySetsHeroesToNull()
    {
        MercObject.AddHeroToParty(testMerc);
        MercObject.ClearParty();
        Assert.IsNull(MercObject.Party[0]);
    }

    [Test]
    public void SwapPartyMemebersSwapsTheIndexOfMercObjectsInParty()
    {
        MercObject secondMerc = new MercObject(2);
        MercObject.AddHeroToParty(testMerc);
        MercObject.AddHeroToParty(secondMerc);
        MercObject.SwapPartyMembers(0, 1);
        Assert.IsTrue(MercObject.Party[0].Equals(secondMerc) && MercObject.Party[1].Equals(testMerc));
    }

    [Test]
    public void GetHealthReturnsProperHealth_NoEquipment()
    {
        Assert.AreEqual(8,testMerc.GetMaxHealth());
    }

    [Test]
    public void GetHealthReturnsProperHealth_Armor()
    {
        int baseHealth = testMerc.GetMaxHealth();
        testMerc.armor = testArmor;
        Assert.AreEqual(baseHealth + testArmor.GetHealth(), testMerc.GetMaxHealth());
    }

    [Test]
    public void GetHealthReturnsProperHealth_Weapon()
    {
        int baseHealth = testMerc.GetMaxHealth();
        testMerc.weapon = testWeapon;
        Assert.AreEqual(baseHealth + testWeapon.GetHealth(), testMerc.GetMaxHealth());
    }

    [Test]
    public void GetHealthReturnsProperHealth_ArmorAndWeapon()
    {
        int baseHealth = testMerc.GetMaxHealth();
        testMerc.weapon = testWeapon;
        testMerc.armor = testArmor;
        Assert.AreEqual(baseHealth + testWeapon.GetHealth() + testArmor.GetHealth(), testMerc.GetMaxHealth());
    }

    [Test]
    public void GetHealthReturnsProperHealth_Negative()
    {
        testMerc.weapon = negativeEquipment;
        Assert.AreEqual(1, testMerc.GetMaxHealth());
    }

    [Test]
    public void UpdateHealthUpdatesHelth()
    {
        testMerc.UpdateHealth(6);
        Assert.AreEqual(6, testMerc.GetHealth());
    }

    [Test]
    public void GetDamageReturnsProperDamage_NoEquipment()
    {
        Assert.AreEqual(1, testMerc.GetDamage());
    }

    [Test]
    public void GetDamageReturnsProperDamage_Armor()
    {
        int baseDamage = testMerc.GetDamage();
        testMerc.armor = testArmor;
        Assert.AreEqual(baseDamage + testArmor.GetDamage(), testMerc.GetDamage());
    }

    [Test]
    public void GetDamageReturnsProperDamage_Weapon()
    {
        int baseDamage = testMerc.GetDamage();
        testMerc.weapon = testWeapon;
        Assert.AreEqual(baseDamage + testWeapon.GetDamage(), testMerc.GetDamage());
    }

    [Test]
    public void GetDamageReturnsProperDamage_ArmorAndWeapon()
    {
        int baseDamage = testMerc.GetDamage();
        testMerc.weapon = testWeapon;
        testMerc.armor = testArmor; 
        Assert.AreEqual(baseDamage + testWeapon.GetDamage() + testArmor.GetDamage(), testMerc.GetDamage());
    }

    [Test]
    public void GetDamageReturnsProperDamage_Negative()
    {
        testMerc.weapon = negativeEquipment;
        Assert.AreEqual(1, testMerc.GetDamage());
    }

    [Test]
    public void GetSpeedReturnsProperSpeed_NoEquipment()
    {
        Assert.AreEqual(2, testMerc.GetSpeed());
    }

    [Test]
    public void GetSpeedReturnsProperSpeed_Armor()
    {
        int baseDamage = testMerc.GetSpeed();
        testMerc.armor = testArmor;
        Assert.AreEqual(baseDamage + testArmor.GetSpeed(), testMerc.GetSpeed());
    }

    [Test]
    public void GetSpeedReturnsProperSpeed_Weapon()
    {
        int baseDamage = testMerc.GetSpeed();
        testMerc.weapon = testWeapon;
        Assert.AreEqual(baseDamage + testWeapon.GetSpeed(), testMerc.GetSpeed());
    }

    [Test]
    public void GetSpeedReturnsProperSpeed_ArmorAndWeapon()
    {
        int baseDamage = testMerc.GetSpeed();
        testMerc.weapon = testWeapon;
        testMerc.armor = testArmor;
        Assert.AreEqual(baseDamage + testWeapon.GetSpeed() + testArmor.GetSpeed(), testMerc.GetSpeed());
    }

    [Test]
    public void GetSpeedReturnsProperSpeed_Negative()
    {
        testMerc.weapon = negativeEquipment;
        Assert.AreEqual(1, testMerc.GetSpeed());
    }

    [Test] 
    public void GetWarriorReturnsProperValue()
    {
        Assert.AreEqual(1,testMerc.GetWarrior());
    }

    [Test]
    public void GetRangerReturnsProperValue()
    {
        Assert.AreEqual(0, testMerc.GetRanger());
    }

    [Test]
    public void GetMageReturnsProperValue()
    {
        Assert.AreEqual(0, testMerc.GetMage());
    }
}
