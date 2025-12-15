// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using osu.Game.Graphics;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Osu.Beatmaps;
using osu.Game.Rulesets.Osu.Objects;
using osu.Game.Rulesets.Osu.UI;
using System.Collections.Generic;

namespace osu.Game.Rulesets.Osu.Mods
{
    public class OsuModSpinnerMadness : Mod, IApplicableToBeatmap
    {
        public override string Name => "SPINNER-MADNESS";
        public override string Acronym => "SPM";
        public override LocalisableString Description => "Enter your message here. Links and general information will automatically be added above this.";
        public override double ScoreMultiplier => 1;
        public override IconUsage? Icon => OsuIcon.ModSpunOut;
        public override ModType Type => ModType.Bemmy;

        public void ApplyToBeatmap(IBeatmap beatmap)
        {
            if (beatmap is not OsuBeatmap osuBeatmap)
                return;

            List<OsuHitObject> newHitObjects = new List<OsuHitObject>();

            foreach (OsuHitObject hitObject in osuBeatmap.HitObjects)
            {
                switch (hitObject)
                {
                    case HitCircle circle:
                        Spinner hitCircleSpinner = new Spinner
                        {
                            Position = OsuPlayfield.BASE_SIZE / 2,
                            StartTime = circle.StartTime,
                            EndTime = circle.StartTime,
                            Samples = circle.Samples,
                            ComboOffset = circle.ComboOffset,
                            IndexInCurrentCombo = circle.IndexInCurrentCombo,
                            ComboIndex = circle.ComboIndex,
                            ComboIndexWithOffsets = circle.ComboIndexWithOffsets,
                        };

                        hitCircleSpinner.ApplyDefaults(osuBeatmap.ControlPointInfo, osuBeatmap.Difficulty);
                        newHitObjects.Add(hitCircleSpinner);

                        break;

                    case Slider slider:
                        Spinner sliderSpinner = new Spinner
                        {
                            Position = OsuPlayfield.BASE_SIZE / 2,
                            StartTime = slider.StartTime,
                            EndTime = slider.EndTime,
                            Samples = slider.Samples,
                            ComboOffset = slider.ComboOffset,
                            IndexInCurrentCombo = slider.IndexInCurrentCombo,
                            ComboIndex = slider.ComboIndex,
                            ComboIndexWithOffsets = slider.ComboIndexWithOffsets,
                        };

                        sliderSpinner.ApplyDefaults(osuBeatmap.ControlPointInfo, osuBeatmap.Difficulty);
                        newHitObjects.Add(sliderSpinner);

                        break;

                    case Spinner spinner:
                        newHitObjects.Add(spinner); break;
                }

                osuBeatmap.HitObjects = newHitObjects;
            }
        }
    }
}
