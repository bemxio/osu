// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Localisation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Logging;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Mods
{
    public partial class ModTransRights : Mod
    {
        public override string Name => "TRANS RIGHTS";
        public override string Acronym => "TRS";
        public override LocalisableString Description => "TRANS RIGHTS ARE HUMAN RIGHTS";
        public override double ScoreMultiplier => 1;
        public override ModType Type => ModType.Fun;

        public partial class DrawableTransFlag : FillFlowContainer
        {
            public DrawableTransFlag()
            {
                RelativeSizeAxes = Axes.Both;
                Direction = FillDirection.Vertical;
                Alpha = 0.5f;
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
