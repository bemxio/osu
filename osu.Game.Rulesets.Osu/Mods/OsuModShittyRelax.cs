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
using static osu.Game.Input.Handlers.ReplayInputHandler;

namespace osu.Game.Rulesets.Osu.Mods
{
    public class OsuModShittyRelax : Mod, IApplicableToDrawableRuleset<OsuHitObject>, IUpdatableByPlayfield
    {
        public override string Name => "Shitty Relax";
        public override string Acronym => "SRX";
        public override LocalisableString Description => "A worse implementation of the Relax mod.";
        public override double ScoreMultiplier => 1;
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

        private OsuInputManager inputManager = null!;

        private List<OsuReplayFrame> replayFrames = null!;
        private int currentFrame = -1;

        public void ApplyToDrawableRuleset(DrawableRuleset<OsuHitObject> drawableRuleset)
        {
            inputManager = ((DrawableOsuRuleset)drawableRuleset).KeyBindingInputManager;
            replayFrames = new SRXOsuAutoGenerator(drawableRuleset.Beatmap, drawableRuleset.Mods).Generate().Frames.Cast<OsuReplayFrame>().ToList();
        }
        
        public void Update(Playfield playfield)
        {
            if (currentFrame == replayFrames.Count - 1)
            {
                return;
            }

            double time = playfield.Clock.CurrentTime;

            if (currentFrame < 0 || Math.Abs(replayFrames[currentFrame + 1].Time - time) <= Math.Abs(replayFrames[currentFrame].Time - time))
            {
                currentFrame++;

                new ReplayState<OsuAction> { 
                    PressedActions = replayFrames[currentFrame].Actions
                }.Apply(inputManager.CurrentState, inputManager); 
            }
        }

        private class SRXOsuAutoGenerator : OsuAutoGenerator
        {
            public SRXOsuAutoGenerator(IBeatmap beatmap, IReadOnlyList<Mod> mods)
                : base(beatmap, mods)
            {
            }

            public new const double KEY_UP_DELAY = 100;
        }
    }
}
