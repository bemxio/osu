// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Localisation;

namespace osu.Game.Rulesets.Mods
{
    public sealed class ModTransRights : Mod
    {
        public override string Name => "TRANS RIGHTS";
        public override string Acronym => "LGBTQIA+";
        public override LocalisableString Description => "TRANS RIGHTS ARE HUMAN RIGHTS";
        public override double ScoreMultiplier => 1;
        public override ModType Type => ModType.Fun;
    }
}
