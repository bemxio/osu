// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Audio;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using osu.Game.Configuration;
using osu.Game.Graphics;
using osu.Game.Overlays.Settings;

namespace osu.Game.Rulesets.Mods
{
    public abstract class ModDoubleTime : ModRateAdjust
    {
        public override string Name => "Double Time";
        public override string Acronym => "DT";
        public override IconUsage? Icon => OsuIcon.ModDoubleTime;
        public override ModType Type => ModType.DifficultyIncrease;
        public override LocalisableString Description => "Zoooooooooom...";
        public override bool Ranked => SpeedChange.IsDefault;

        private readonly RateAdjustModHelper rateAdjustHelper;

        [SettingSource("Speed increase", "The actual increase to apply", SettingControlType = typeof(MultiplierSettingsSlider))]
        public override BindableNumber<double> SpeedChange { get; } = new BindableDouble(1.5)
        {
            MinValue = 1.01,
            MaxValue = 2.5,
            Precision = 0.01,
        };

        [SettingSource("Adjust pitch", "Should pitch be adjusted with speed")]
        public virtual BindableBool AdjustPitch { get; } = new BindableBool();

        [SettingSource("Extended Limits", "Adjust speed beyond playable limits.")]
        public BindableBool ExtendedLimits { get; } = new BindableBool();

        private void OnExtendedLimitsValueChanged(ValueChangedEvent<bool> e)
        {
            SpeedChange.MaxValue = e.NewValue ? 25 : 2.5;
            SpeedChange.Precision = e.NewValue ? 0.1 : 0.01;
        }

        protected ModDoubleTime()
        {
            ExtendedLimits.BindValueChanged(OnExtendedLimitsValueChanged);

            rateAdjustHelper = new RateAdjustModHelper(SpeedChange);
            rateAdjustHelper.HandleAudioAdjustments(AdjustPitch);
        }

        public override void ApplyToTrack(IAdjustableAudioComponent track)
        {
            rateAdjustHelper.ApplyToTrack(track);
        }

        public override double ScoreMultiplier => rateAdjustHelper.ScoreMultiplier;
    }
}
