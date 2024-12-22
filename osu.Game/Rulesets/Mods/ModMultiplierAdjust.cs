// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Bindables;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using osu.Game.Configuration;
using osu.Game.Rulesets.Scoring;
using osu.Game.Scoring;

namespace osu.Game.Rulesets.Mods
{
    public sealed class ModMultiplierAdjust : Mod, IApplicableToScoreProcessor
    {
        public override string Name => "Multiplier Adjust";
        public override string Acronym => "MA";
        public override LocalisableString Description => "Override a mod combination's score multiplier.";
        public override double ScoreMultiplier => Multiplier.Value;
        public override IconUsage? Icon => FontAwesome.Solid.Hammer;
        public override ModType Type => ModType.Bemmy;

        [SettingSource("Score multiplier", "The score multiplier that will be applied.")]
        public BindableNumber<double> Multiplier { get; } = new BindableDouble(1)
        {
            MinValue = 0,
            MaxValue = 2,
            Precision = 0.01
        };

        public void ApplyToScoreProcessor(ScoreProcessor scoreProcessor)
        {
            scoreProcessor.Mods.Value = new[] { this };
        }

        public ScoreRank AdjustRank(ScoreRank rank, double accuracy) => rank;
    }
}
