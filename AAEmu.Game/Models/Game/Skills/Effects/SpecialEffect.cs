﻿using System;
using AAEmu.Game.Models.Game.Skills.Templates;
using AAEmu.Game.Models.Game.Units;
using NLog;

namespace AAEmu.Game.Models.Game.Skills.Effects
{
    public interface ISpecialEffect
    {
        void Execute(Unit caster,
            SkillCaster casterObj,
            BaseUnit target,
            SkillCastTarget targetObj,
            CastAction castObj,
            Skill skill,
            SkillObject skillObject,
            DateTime time,
            int value1,
            int value2,
            int value3,
            int value4);
    }

    public enum SpecialEffectType
    {
        Charge = 1,
        DisturbCasting = 5,
        LoseTarget = 8,
        Unk9 = 9,
        Interaction = 10,
        Blink = 11,
        KnockBack = 13,
        SpawnDoodad = 15,
        BuffSteal = 16,
        Track = 17,
        FakeDeath = 18,
        SpawnBomb = 19,
        PhysicalEnchantWeapon = 20,
        PhysicalEnchantArmor = 21,
        Resurrection = 22,
        CapturePet = 23,
        SpawnPet = 24,
        Return = 25,
        GainItem = 27,
        AddExp = 28,
        AddLaborPower = 29,
        SavePortal = 30,
        OpenPortal = 31,
        GainItemWithPosImprint = 32,
        SkillUse = 33,
        Anim = 34,
        FxGroup = 35,
        FxGroupAnim = 36,
        Projectile = 37,
        ProjectileAnim = 38,
        ManaCost = 39,
        Cooldown = 40,
        GlobalCooldown = 41,
        OpacityControl = 42,
        ResetCooldown = 43,
        RedeemBuff = 44,
        GainItemWithEmblemImprint = 45,
        ExplodeBuff = 46,
        TeleportToUnit = 47,
        Combo = 48,
        ItemConversion = 49,
        DeclareDominion = 50,
        MateMakeGetUp = 51,
        StopManaRegen = 52,
        AttachTo = 53,
        AddBreath = 54,
        Detach = 55,
        HealPet = 56,
        RenewEquipment = 57,
        RemoveDoodad = 58,
        CancelStealth = 59,
        SpawnSlave = 60,
        CancelOngoingBuff = 61,
        HealSlave = 62,
        CombatText = 63,
        GetSiegeTicket = 64,
        TeleportToSiegeHq = 65,
        AutoAttack = 66,
        CombatDice = 67,
        ApplyReagents = 68,
        SextantPos = 69,
        NotifyQuest = 70,
        DeclareIndependence = 71,
        DestroyAndSpawnSlave = 72,
        MoveToGround = 73,
        Escape = 74,
        ReportBot = 75,
        ClearProjectile = 76,
        RetrieveProjectile = 77,
        AddFxToProjectile = 78,
        FishingLoot = 79,
        StopChanneling = 80,
        FinishChanneling = 81,
        SetVariable = 82,
        ReportBotExpired = 83,
        ReportBotArrested = 84,
        EngraveOnGuardTower = 85,
        ConsumeLaborPower = 86,
        GiveLivingPoint = 87,
        ApplyBotTrial = 88,
        ArrestBot = 89,
        EscapeMySlave = 90,
        AddCharacterSlot = 91,
        GradeEnchant = 92,
        PlayUserMusic = 93,
        PauseUserMusic = 94,
        RechargeItemBuff = 95,
        ExpToItem = 96,
        UserMusicSaveNotes = 97,
        Dyeing = 98,
        GiveBmMileage = 99,
        GiveHonorPoint = 100,
        EnterBeautyshop = 101,
        GiveCrimePoint = 102,
        CleanupLookConvert = 103,
        AggroCopy = 104,
        AggroReset = 105,
        ItemSocketing = 106,
        GenderTransfer = 107,
        Skinize = 108,
        NpcDespawn = 109,
        StartDominionNonPvpDuration = 110,
        ItemCapScaleReset = 111,
        ItemCapScale = 112,
        AddPStat = 113,
        GiveAppellation = 114,
        ExitArchemall = 115,
        WeaponDisplay = 116,
        AuctionPostAuthority = 117
    }

    public class SpecialEffect : EffectTemplate
    {
        public SpecialEffectType SpecialEffectTypeId { get; set; } // TODO inspect
        public int Value1 { get; set; }
        public int Value2 { get; set; }
        public int Value3 { get; set; }
        public int Value4 { get; set; }

        public override bool OnActionTime => false;

        public override void Apply(Unit caster,
            SkillCaster casterObj,
            BaseUnit target,
            SkillCastTarget targetObj,
            CastAction castObj,
            Skill skill,
            SkillObject skillObject,
            DateTime time)
        {
            Log.Debug(
                "SpecialEffect, Special: {0}, Value1: {1}, Value2: {2}, Value3: {3}, Value4: {4}",
                SpecialEffectTypeId, Value1, Value2, Value3, Value4
                );

            var classType = Type.GetType("AAEmu.Game.Models.Game.Skills.Effects.SpecialEffects." + SpecialEffectTypeId);
            if (classType == null)
            {
                Log.Error("Unknown special effect: {0}", SpecialEffectTypeId);
                return;
            }

            var action = (ISpecialEffect)Activator.CreateInstance(classType);
            action.Execute(caster, casterObj, target, targetObj, castObj, skill, skillObject, time, Value1, Value2, Value3, Value4);
        }
    }
}
