// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using osu.Game.Graphics;
using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Mods
{
    public partial class ModNISZOgen : Mod, IApplicableToHealthProcessor
    {
        public override string Name => "NISZOgen";
        public override string Acronym => "NGN";
        public override LocalisableString Description => "200ms to nie są 2 sekundy?";
        public override double ScoreMultiplier => 1;
        public override ModType Type => ModType.Bemmy;
        public override IconUsage? Icon => OsuIcon.ModPerfect;
        public override Type[] IncompatibleMods => new[] { typeof(ModNoFail), typeof(ModCinema) };

        public void ApplyToHealthProcessor(HealthProcessor healthProcessor)
        {
            healthProcessor.TriggerFailure();
        }
    }
}
