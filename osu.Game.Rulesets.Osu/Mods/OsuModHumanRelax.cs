// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using osu.Game.Graphics;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Osu.Mods
{
    public class OsuModHumanRelax : Mod
    {
        public override string Name => "Human Relax";
        public override string Acronym => "HRX";
        public override LocalisableString Description => "Relax, but for cheaters.";
        public override double ScoreMultiplier => 1;
        public override IconUsage? Icon => OsuIcon.ModRelax;
        public override ModType Type => ModType.Automation;

        public override Type[] IncompatibleMods => new[]
        {
            typeof(ModAutoplay),
            typeof(OsuModRelax),
            typeof(OsuModAutopilot),
            typeof(OsuModMagnetised),
            typeof(OsuModAlternate),
            typeof(OsuModSingleTap)
        };
    }
}
