﻿using MapsetParser.objects;
using System.Collections.Generic;

namespace MapsetChecksCatch.helper
{
    public sealed class CatchHitObject : HitObject
    {
        public CatchHitObject(string[] anArgs, Beatmap beatmap) : base(anArgs, beatmap)
        {
            X = Position.X;
        }

        public float X;
        public float DistanceToHyperDash { get; set; }
        public double PixelsToHyperDash { get; set; }
        public bool IsHyperDash => HyperDashTarget != null;
        public CatchHitObject HyperDashTarget { get; set; }
        public CatchHitObject Origin { get; set; }
        public List<CatchHitObject> Extras { get; set; }

        /**
         * Check if the current object is hypersnapped taking the current objects start point and the end point of the last object.
         *
         * Providing a difficulty level and if the last object was a hyper
         */
        public bool IsHigherSnapped(Beatmap.Difficulty difficulty, bool wasHyper)
        {
            var ms = GetPrevDeltaTime();

            return difficulty switch
            {
                Beatmap.Difficulty.Normal => (ms < (int) AllowedDash.DIFF_N),
                Beatmap.Difficulty.Hard => (ms < (wasHyper
                    ? (int) AllowedHyperDash.DIFF_H * 2
                    : (int) AllowedDash.DIFFS_HI * 2)),
                Beatmap.Difficulty.Insane => (ms < (wasHyper
                    ? (int) AllowedHyperDash.DIFF_I * 2
                    : (int) AllowedDash.DIFFS_HI * 2)),
                _ => false
            };
        }
    }
}