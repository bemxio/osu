// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Game.Configuration;
using osu.Game.Rulesets;
using osu.Game.Rulesets.Mods;
using osu.Game.Utils;

namespace osu.Game.Screens.Select
{
    public partial class SongSelectTouchInputDetector : Component
    {
        [Resolved]
        private Bindable<RulesetInfo> ruleset { get; set; } = null!;

        [Resolved]
        private Bindable<IReadOnlyList<Mod>> mods { get; set; } = null!;

        private IBindable<bool> touchActive = null!;

        [BackgroundDependencyLoader]
        private void load(SessionStatics statics)
        {
            touchActive = statics.GetBindable<bool>(Static.TouchInputActive);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            ruleset.BindValueChanged(_ => Scheduler.AddOnce(updateState));
            mods.BindValueChanged(_ => Scheduler.AddOnce(updateState));
            mods.BindDisabledChanged(_ => Scheduler.AddOnce(updateState));
            touchActive.BindValueChanged(_ => Scheduler.AddOnce(updateState));
            updateState();
        }

        private void updateState()
        {
            return;
        }
    }
}
