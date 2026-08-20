using NecroLens.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecroLens.MobData;

public static partial class MobDatabase
{
    private static void Register_EO()
    {
        // Excalibur
        MobInformation[12100] = new MobInfo
        {
            Id = 12100,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Easy,
            BossOrAdd = true,
        };
        // Administrator
        MobInformation[12102] = new MobInfo
        {
            Id = 12102,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Easy,
            BossOrAdd = true,
        };
        // undead Orthos dragon
        MobInformation[12106] = new MobInfo
        {
            Id = 12106,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos Thanatos
        MobInformation[12107] = new MobInfo
        {
            Id = 12107,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sound,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos succubus
        MobInformation[12108] = new MobInfo
        {
            Id = 12108,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
            Patrol = true,
        };
        // Orthos behemoth
        MobInformation[12109] = new MobInfo
        {
            Id = 12109,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos grenade
        MobInformation[12110] = new MobInfo
        {
            Id = 12110,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos dahak
        MobInformation[12111] = new MobInfo
        {
            Id = 12111,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos imp
        MobInformation[12112] = new MobInfo
        {
            Id = 12112,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos demon
        MobInformation[12113] = new MobInfo
        {
            Id = 12113,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos fachan
        MobInformation[12114] = new MobInfo
        {
            Id = 12114,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
            Patrol = true,
        };
        // Orthos vassago
        MobInformation[12115] = new MobInfo
        {
            Id = 12115,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
            Patrol = true,
        };
        // Orthos bhoot
        MobInformation[12116] = new MobInfo
        {
            Id = 12116,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sound,
            DangerLevel = DangerLevel.Easy,
        };
        // orthoiron claw
        MobInformation[12117] = new MobInfo
        {
            Id = 12117,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos water sprite
        MobInformation[12118] = new MobInfo
        {
            Id = 12118,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Easy,
        };
        // orthohunter
        MobInformation[12119] = new MobInfo
        {
            Id = 12119,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthosystem β
        MobInformation[12120] = new MobInfo
        {
            Id = 12120,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos microsystem
        MobInformation[12121] = new MobInfo
        {
            Id = 12121,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Easy,
        };
        // orthosoldier
        MobInformation[12122] = new MobInfo
        {
            Id = 12122,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos wood golem
        MobInformation[12123] = new MobInfo
        {
            Id = 12123,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
            Patrol = true,
        };
        // Orthos groundskeeper
        MobInformation[12124] = new MobInfo
        {
            Id = 12124,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
            Patrol = true,
        };
        // Orthos keter
        MobInformation[12125] = new MobInfo
        {
            Id = 12125,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
            Patrol = true,
        };
        // Orthos rafflesia
        MobInformation[12126] = new MobInfo
        {
            Id = 12126,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos sawtooth
        MobInformation[12127] = new MobInfo
        {
            Id = 12127,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos netzach
        MobInformation[12128] = new MobInfo
        {
            Id = 12128,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos spirulina
        MobInformation[12129] = new MobInfo
        {
            Id = 12129,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // great Orthos morbol
        MobInformation[12130] = new MobInfo
        {
            Id = 12130,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos belladonna
        MobInformation[12131] = new MobInfo
        {
            Id = 12131,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // orthodemolisher
        MobInformation[12132] = new MobInfo
        {
            Id = 12132,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Easy,
        };
        // orthoknight
        MobInformation[12133] = new MobInfo
        {
            Id = 12133,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // orthodroid
        MobInformation[12134] = new MobInfo
        {
            Id = 12134,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // orthogiant
        MobInformation[12135] = new MobInfo
        {
            Id = 12135,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
            Patrol = true,
        };
        // Orthokaliya
        MobInformation[12136] = new MobInfo
        {
            Id = 12136,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos biast
        MobInformation[12137] = new MobInfo
        {
            Id = 12137,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // lesser Orthos dragon
        MobInformation[12138] = new MobInfo
        {
            Id = 12138,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
            Patrol = true,
        };
        // Orthos wyvern
        MobInformation[12139] = new MobInfo
        {
            Id = 12139,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // orthoblue dragon
        MobInformation[12140] = new MobInfo
        {
            Id = 12140,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos vouivre
        MobInformation[12141] = new MobInfo
        {
            Id = 12141,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos brobinyak
        MobInformation[12142] = new MobInfo
        {
            Id = 12142,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos vanara
        MobInformation[12143] = new MobInfo
        {
            Id = 12143,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
            Patrol = true,
        };
        // orthoshelled dragon
        MobInformation[12144] = new MobInfo
        {
            Id = 12144,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // orthobug
        MobInformation[12145] = new MobInfo
        {
            Id = 12145,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Easy,
        };
        // orthospider
        MobInformation[12146] = new MobInfo
        {
            Id = 12146,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sound,
            DangerLevel = DangerLevel.Easy,
        };
        // orthopredator
        MobInformation[12147] = new MobInfo
        {
            Id = 12147,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // phantom orthoray
        MobInformation[12148] = new MobInfo
        {
            Id = 12148,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Caution,
            Patrol = true,
        };
        // Orthos mirrorknight
        MobInformation[12149] = new MobInfo
        {
            Id = 12149,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
            Patrol = true,
        };
        // orthotaur
        MobInformation[12150] = new MobInfo
        {
            Id = 12150,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos reptoid
        MobInformation[12151] = new MobInfo
        {
            Id = 12151,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // orthochimera
        MobInformation[12152] = new MobInfo
        {
            Id = 12152,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Caution,
        };
        // Orthos shabti
        MobInformation[12153] = new MobInfo
        {
            Id = 12153,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // orthoiksalion
        MobInformation[12154] = new MobInfo
        {
            Id = 12154,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // orthonaga
        MobInformation[12155] = new MobInfo
        {
            Id = 12155,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
            Patrol = true,
        };
        // orthoempuse
        MobInformation[12156] = new MobInfo
        {
            Id = 12156,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sound,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos lamia
        MobInformation[12157] = new MobInfo
        {
            Id = 12157,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos gelato
        MobInformation[12158] = new MobInfo
        {
            Id = 12158,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos spriggan
        MobInformation[12159] = new MobInfo
        {
            Id = 12159,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos bergthurs
        MobInformation[12160] = new MobInfo
        {
            Id = 12160,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos apa
        MobInformation[12161] = new MobInfo
        {
            Id = 12161,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos Kelpie
        MobInformation[12162] = new MobInfo
        {
            Id = 12162,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
            Patrol = true,
        };
        // Orthos kukulkan
        MobInformation[12163] = new MobInfo
        {
            Id = 12163,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos hoarhound
        MobInformation[12164] = new MobInfo
        {
            Id = 12164,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
            Patrol = true,
        };
        // Orthos huwasi
        MobInformation[12165] = new MobInfo
        {
            Id = 12165,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos Acheron
        MobInformation[12166] = new MobInfo
        {
            Id = 12166,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
            Patrol = true,
        };
        // Orthos abaia
        MobInformation[12167] = new MobInfo
        {
            Id = 12167,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos goobbue
        MobInformation[12168] = new MobInfo
        {
            Id = 12168,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos Hedetet
        MobInformation[12169] = new MobInfo
        {
            Id = 12169,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos mudman
        MobInformation[12170] = new MobInfo
        {
            Id = 12170,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos banshee
        MobInformation[12171] = new MobInfo
        {
            Id = 12171,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos monk
        MobInformation[12172] = new MobInfo
        {
            Id = 12172,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Easy,
            Patrol = true,
        };
        // Orthos stingray
        MobInformation[12173] = new MobInfo
        {
            Id = 12173,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos Oceanus
        MobInformation[12174] = new MobInfo
        {
            Id = 12174,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos big claw
        MobInformation[12175] = new MobInfo
        {
            Id = 12175,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos yabby
        MobInformation[12176] = new MobInfo
        {
            Id = 12176,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
            Patrol = true,
        };
        // Orthos piranu
        MobInformation[12177] = new MobInfo
        {
            Id = 12177,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos leech
        MobInformation[12178] = new MobInfo
        {
            Id = 12178,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos zaratan
        MobInformation[12179] = new MobInfo
        {
            Id = 12179,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sound,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos ice sprite
        MobInformation[12180] = new MobInfo
        {
            Id = 12180,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Caution,
        };
        // Orthos bombfish
        MobInformation[12181] = new MobInfo
        {
            Id = 12181,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos rockfin
        MobInformation[12182] = new MobInfo
        {
            Id = 12182,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
            Patrol = true,
        };
        // Orthos ymir
        MobInformation[12183] = new MobInfo
        {
            Id = 12183,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos pteroc
        MobInformation[12184] = new MobInfo
        {
            Id = 12184,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos drake
        MobInformation[12185] = new MobInfo
        {
            Id = 12185,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos cobra
        MobInformation[12186] = new MobInfo
        {
            Id = 12186,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
            Patrol = true,
        };
        // Orthos monitor
        MobInformation[12187] = new MobInfo
        {
            Id = 12187,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos anala
        MobInformation[12188] = new MobInfo
        {
            Id = 12188,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos diplocaulus
        MobInformation[12189] = new MobInfo
        {
            Id = 12189,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos basilisk
        MobInformation[12190] = new MobInfo
        {
            Id = 12190,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos matamata
        MobInformation[12191] = new MobInfo
        {
            Id = 12191,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos palleon
        MobInformation[12192] = new MobInfo
        {
            Id = 12192,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos falak
        MobInformation[12193] = new MobInfo
        {
            Id = 12193,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos gowrow
        MobInformation[12194] = new MobInfo
        {
            Id = 12194,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
            Patrol = true,
        };
        // orthoninja
        MobInformation[12195] = new MobInfo
        {
            Id = 12195,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Easy,
            Patrol = true,
        };
        // Haokah perfected
        MobInformation[12196] = new MobInfo
        {
            Id = 12196,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos toco toco
        MobInformation[12197] = new MobInfo
        {
            Id = 12197,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos skatene
        MobInformation[12198] = new MobInfo
        {
            Id = 12198,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sound,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos unicorn
        MobInformation[12199] = new MobInfo
        {
            Id = 12199,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos coeurl
        MobInformation[12200] = new MobInfo
        {
            Id = 12200,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
            Patrol = true,
        };
        // Orthos wolf
        MobInformation[12201] = new MobInfo
        {
            Id = 12201,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos gulo gulo
        MobInformation[12202] = new MobInfo
        {
            Id = 12202,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos sasquatch
        MobInformation[12203] = new MobInfo
        {
            Id = 12203,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos primelephas
        MobInformation[12204] = new MobInfo
        {
            Id = 12204,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // orthokunoichi
        MobInformation[12205] = new MobInfo
        {
            Id = 12205,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Easy,
            Patrol = true,
        };
        // bird of Orthos
        MobInformation[12206] = new MobInfo
        {
            Id = 12206,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos thunderbeast
        MobInformation[12207] = new MobInfo
        {
            Id = 12207,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
            Patrol = true,
        };
        // Orthos flamebeast
        MobInformation[12208] = new MobInfo
        {
            Id = 12208,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Caution,
        };
        // Orthos kargas
        MobInformation[12209] = new MobInfo
        {
            Id = 12209,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos deepeye
        MobInformation[12210] = new MobInfo
        {
            Id = 12210,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos Spartoi
        MobInformation[12211] = new MobInfo
        {
            Id = 12211,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos hecteyes
        MobInformation[12212] = new MobInfo
        {
            Id = 12212,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sound,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos pudding
        MobInformation[12213] = new MobInfo
        {
            Id = 12213,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos persona
        MobInformation[12214] = new MobInfo
        {
            Id = 12214,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos gourmand
        MobInformation[12215] = new MobInfo
        {
            Id = 12215,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Caution,
            Patrol = true,
        };
        // Orthos ahriman
        MobInformation[12216] = new MobInfo
        {
            Id = 12216,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // orthoiron corse
        MobInformation[12217] = new MobInfo
        {
            Id = 12217,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Caution,
            Patrol = true,
        };
        // Orthos catoblepas
        MobInformation[12218] = new MobInfo
        {
            Id = 12218,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Caution,
        };
        // Orthos pegasus
        MobInformation[12219] = new MobInfo
        {
            Id = 12219,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos wraith
        MobInformation[12220] = new MobInfo
        {
            Id = 12220,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos specter
        MobInformation[12221] = new MobInfo
        {
            Id = 12221,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Caution,
            Patrol = true,
        };
        // Orthos abyss
        MobInformation[12222] = new MobInfo
        {
            Id = 12222,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // orthodrone
        MobInformation[12223] = new MobInfo
        {
            Id = 12223,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthosystem γ
        MobInformation[12224] = new MobInfo
        {
            Id = 12224,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Easy,
            Patrol = true,
        };
        // Orthos Mithridates
        MobInformation[12225] = new MobInfo
        {
            Id = 12225,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // orthonaught
        MobInformation[12226] = new MobInfo
        {
            Id = 12226,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos fitter
        MobInformation[12227] = new MobInfo
        {
            Id = 12227,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos sphinx
        MobInformation[12228] = new MobInfo
        {
            Id = 12228,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthosystem α
        MobInformation[12229] = new MobInfo
        {
            Id = 12229,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos zaghnal
        MobInformation[12230] = new MobInfo
        {
            Id = 12230,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos mining drone
        MobInformation[12231] = new MobInfo
        {
            Id = 12231,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sound,
            DangerLevel = DangerLevel.Easy,
        };
        // Orthos motherbit
        MobInformation[12232] = new MobInfo
        {
            Id = 12232,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Easy,
        };
        // servomechanical orthochimera
        MobInformation[12233] = new MobInfo
        {
            Id = 12233,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
            Patrol = true,
        };
        // servomechanical orthotaur
        MobInformation[12234] = new MobInfo
        {
            Id = 12234,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Easy,
            Patrol = true,
        };
        // Orthos Durga
        MobInformation[12235] = new MobInfo
        {
            Id = 12235,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Easy,
        };
        // Gancanagh
        MobInformation[12240] = new MobInfo
        {
            Id = 12240,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Easy,
            BossOrAdd = true,
        };
        // Tiamat clone
        MobInformation[12242] = new MobInfo
        {
            Id = 12242,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Easy,
            BossOrAdd = true,
        };
        // aeturna
        MobInformation[12246] = new MobInfo
        {
            Id = 12246,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Easy,
            BossOrAdd = true,
        };
        // Proto-Kaliya
        MobInformation[12247] = new MobInfo
        {
            Id = 12247,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Easy,
            BossOrAdd = true,
        };
        // cloning node
        MobInformation[12261] = new MobInfo
        {
            Id = 12261,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Easy,
            BossOrAdd = true,
        };
        // Twintania clone
        MobInformation[12263] = new MobInfo
        {
            Id = 12263,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Easy,
            BossOrAdd = true,
        };
        // servomechanical chimera 14X
        MobInformation[12265] = new MobInfo
        {
            Id = 12265,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Proximity,
            DangerLevel = DangerLevel.Easy,
            BossOrAdd = true,
        };
        // servomechanical minotaur 16
        MobInformation[12267] = new MobInfo
        {
            Id = 12267,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
            BossOrAdd = true,
        };
        // Orthos craklaw
        MobInformation[12318] = new MobInfo
        {
            Id = 12318,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Easy,
        };
        // lamia queen
        MobInformation[12322] = new MobInfo
        {
            Id = 12322,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Danger,
            Special = true,
        };
        // Meracydian clone
        MobInformation[12323] = new MobInfo
        {
            Id = 12323,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Danger,
            Special = true,
        };
        // Demi-Cochma
        MobInformation[12324] = new MobInfo
        {
            Id = 12324,
            Dungeon = DeepDungeon.EO,
            AggroType = AggroType.Sight,
            DangerLevel = DangerLevel.Danger,
            Special = true,
        };
    }
}
