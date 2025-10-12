namespace NecroLens.Model;

public enum Pomander
{
    Safety = 1,
    Sight,
    Strength,
    Steel,
    Affluence,
    Flight,
    Alteration,
    Purity,
    Fortune,
    Witching,
    Serenity,
    Rage,                // PotD Unique
    Lust,                // PotD Unique
    Intuition,
    Raising,
    Resolution,          // PotD Unique
    Frailty,             // HoH Unique
    Concealment,         // HoH Unique
    Petrification,       // HoH Unique
    LethargyProtomander, // EO Unique
    StormsProtomander,   // EO Unique
    DreadProtomander,    // EO Unique
    SafetyProtomander,   // = 23 -> 22 + Safety
    SightProtomander,    // = 24 -> 22 + Sight
    StrengthProtomander, // ...
    SteelProtomander,
    AffluenceProtomander,
    FlightProtomander,
    AlterationProtomander,
    PurityProtomander,
    FortuneProtomander,
    WitchingProtomander,
    SerenityProtomander,  // = 33 -> 22 + Serenity
    IntuitionProtomander, // = 34 -> 20 + Intuition
    RaisingProtomander,   // = 35 -> 20 + Raising
    
    HastePomander, // = 37 PT Unique
    PurificationPomander, // PT Unique
    DevotionPomander, // PT Unique
}
