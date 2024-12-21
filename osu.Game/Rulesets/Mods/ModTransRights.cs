// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Localisation;
using osu.Game.Configuration;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Mods
{
    public partial class ModTransRights : Mod
    {
        public override string Name => "TRANS RIGHTS";
        public override string Acronym => "TRS";
        public override LocalisableString Description => "TRANS RIGHTS ARE HUMAN RIGHTS";
        public override double ScoreMultiplier => 1 + Math.Round(Transparency.Value * 0.15f, 2, MidpointRounding.ToZero);
        public override ModType Type => ModType.Bemmy;
        public override bool Ranked => Transparency.IsDefault;

        [SettingSource("Flag transparency", "The transparency of the transgender flag overlay.")]
        public BindableNumber<float> Transparency { get; } = new BindableFloat(0.5f)
        {
            MinValue = 0,
            MaxValue = 1,
            Precision = 0.01f
        };

        public partial class DrawableTransFlag : FillFlowContainer
        {
            public DrawableTransFlag(float transparency)
            {
                RelativeSizeAxes = Axes.Both;
                Direction = FillDirection.Vertical;
                Alpha = transparency;

                Children = new[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Height = 0.2f,
                        Colour = Color4.LightSkyBlue
                    },
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Height = 0.2f,
                        Colour = Color4.Pink
                    },
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Height = 0.2f,
                        Colour = Color4.White
                    },
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Height = 0.2f,
                        Colour = Color4.Pink
                    },
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Height = 0.2f,
                        Colour = Color4.LightSkyBlue
                    }
                };
            }
        }
    }
}
