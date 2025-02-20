// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Localisation;
using osu.Game.Beatmaps;
using osu.Game.Beatmaps.ControlPoints;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Osu.Beatmaps;
using osu.Game.Rulesets.Osu.Objects;
using osu.Game.Rulesets.UI;
using System;
using System.Collections.Generic;

namespace osu.Game.Rulesets.Osu.Mods
{
    public sealed class OsuModSliderStreams : Mod, IApplicableToBeatmap
    {
        public override string Name => "Slider Streams";
        public override string Acronym => "SSS";
        public override LocalisableString Description => "Slider fanboys HATE this SIMPLE trick!";
        public override double ScoreMultiplier => 1;
        public override ModType Type => ModType.Bemmy;

        public void ApplyToBeatmap(IBeatmap beatmap)
        {
            if (beatmap is not OsuBeatmap osuBeatmap)
                return;

            List<OsuHitObject> newHitObjects = new List<OsuHitObject>();

            foreach (OsuHitObject hitObject in osuBeatmap.HitObjects)
            {
                if (hitObject is Slider slider)
                {
                    double circleAmount = osuBeatmap.BeatmapInfo.BPM * (slider.Duration / 60000) * 4;

                    for (double i = 0; i <= circleAmount; i++)
                    {
                        HitCircle hitCircle = new HitCircle
                        {
                            StartTime = slider.StartTime + slider.Duration * (i / circleAmount),
                            Position = slider.StackedPositionAt(i / circleAmount),
                            Samples = slider.Samples,
                            ComboOffset = slider.ComboOffset,
                            IndexInCurrentCombo = slider.IndexInCurrentCombo,
                            ComboIndex = slider.ComboIndex,
                            ComboIndexWithOffsets = slider.ComboIndexWithOffsets,
                        };

                        hitCircle.ApplyDefaults(osuBeatmap.ControlPointInfo, osuBeatmap.Difficulty);
                        newHitObjects.Add(hitCircle);
                    }
                }
                else
                {
                    newHitObjects.Add(hitObject);
                }
            }

            osuBeatmap.HitObjects = newHitObjects;
        }
    }
}