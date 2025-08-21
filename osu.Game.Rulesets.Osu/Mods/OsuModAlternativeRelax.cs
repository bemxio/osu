// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using osu.Game.Beatmaps;
using osu.Game.Graphics;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Osu.Objects;
using osu.Game.Rulesets.Osu.Replays;
using osu.Game.Rulesets.Osu.UI;
using osu.Game.Rulesets.UI;
using osu.Game.Screens.Play;
using static osu.Game.Input.Handlers.ReplayInputHandler;

namespace osu.Game.Rulesets.Osu.Mods
{
    public class OsuModAlternativeRelax : Mod, IUpdatableByPlayfield, IApplicableToDrawableRuleset<OsuHitObject>, IApplicableToPlayer, IHasNoTimedInputs
    {
        public override string Name => "Alternative Relax";
        public override string Acronym => "ARX";
        public override LocalisableString Description => "A different implementation of the Relax mod.";
        public override double ScoreMultiplier => 0.1;
        public override IconUsage? Icon => OsuIcon.ModRelax;
        public override ModType Type => ModType.Bemmy;

        public override Type[] IncompatibleMods => new[]
        {
            typeof(ModAutoplay),
            typeof(OsuModRelax),
            typeof(OsuModAutopilot),
            typeof(OsuModMagnetised),
            typeof(OsuModAlternate),
            typeof(OsuModSingleTap)
        };

        private DrawableOsuRuleset drawableOsuRuleset = null!;
        private OsuInputManager osuInputManager = null!;

        private List<OsuReplayFrame> replayFrames = null!;
        private int currentFrame = -1;

        private bool hasReplay;
        private bool legacyReplay;

        public void ApplyToDrawableRuleset(DrawableRuleset<OsuHitObject> drawableRuleset)
        {
            drawableOsuRuleset = (DrawableOsuRuleset)drawableRuleset;
            osuInputManager = drawableOsuRuleset.KeyBindingInputManager;

            replayFrames = new ARXOsuAutoGenerator(drawableOsuRuleset.Beatmap, drawableOsuRuleset.Mods).Generate().Frames.Cast<OsuReplayFrame>().ToList();
        }

        public void ApplyToPlayer(Player player)
        {
            if (osuInputManager.ReplayInputHandler != null)
            {
                hasReplay = true;
                legacyReplay = drawableOsuRuleset.ReplayScore.ScoreInfo.IsLegacyScore;

                return;
            }

            osuInputManager.AllowGameplayInputs = false;
        }

        public void Update(Playfield playfield)
        {
            if (hasReplay && !legacyReplay)
                return;

            if (currentFrame == replayFrames.Count - 1)
                return;

            double time = playfield.Clock.CurrentTime;

            if (currentFrame < 0 || Math.Abs(replayFrames[currentFrame + 1].Time - time) <= Math.Abs(replayFrames[currentFrame].Time - time))
            {
                currentFrame++;

                new ReplayState<OsuAction> {
                    PressedActions = replayFrames[currentFrame].Actions
                }.Apply(osuInputManager.CurrentState, osuInputManager);
            }
        }

        private class ARXOsuAutoGenerator : OsuAutoGenerator
        {
            public ARXOsuAutoGenerator(IBeatmap beatmap, IReadOnlyList<Mod> mods)
                : base(beatmap, mods)
            {
            }

            public new const double KEY_UP_DELAY = 500;
        }
    }
}
