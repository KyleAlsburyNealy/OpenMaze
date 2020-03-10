﻿using UnityEngine;
using value;

namespace trial
{
    public abstract class TimeoutableTrial : AbstractTrial
    {
        private readonly float _threshHold;

        protected TimeoutableTrial(BlockId blockId, TrialId trialId) : base(blockId, trialId)
        {
            _threshHold = trialData.TrialTime < 1 ? int.MaxValue : trialData.TrialTime;
        }

        //Code for a trial to continue
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (_runningTime > _threshHold)
            {
                Debug.Log(_runningTime);
                Progress();
            }
        }
    }
}