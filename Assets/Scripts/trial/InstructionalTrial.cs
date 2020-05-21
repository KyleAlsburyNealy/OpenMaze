﻿using data;
using UI.Buttons;
using UnityEngine;
using value;

namespace trial
{
    public class InstructionalTrial : TimeoutableTrial

    {
        public InstructionalTrial(Data data, BlockId blockId, TrialId trialId) : base(data, blockId, trialId)
        {
        }

        public override void PreEntry(TrialProgress t, bool first = true)
        {
            base.PreEntry(t, first);
            t.TimingVerification = DataSingleton.GetData().TimingVerification; // timing diagnostics

            t.isLoaded = true;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            // Default to space key
            var trialEndKeyCode = "space";
            var ignoreUserInputDelay = DataSingleton.GetData().IgnoreUserInputDelay;

            if (!string.IsNullOrEmpty(trialData.TrialEndKey.ToLower()))
                trialEndKeyCode = trialData.TrialEndKey.ToLower();

            if (Input.GetKey(trialEndKeyCode) && _runningTime > ignoreUserInputDelay) Progress();

            //Exit the experiment 
            if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        }
    }
}
