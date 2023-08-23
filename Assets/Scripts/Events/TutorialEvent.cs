using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SwordfishGame
{
    public class TutorialEvent : EventObj
    {
        public enum TutorialStates
        {
            learnMove,
            learnLook,
            learnAttack,
            learnLean, 
            end
        }

        public TutorialStates state;
        int currentStateNum = 0;
        public List<string> textList = new List<string>();
        public TextMeshProUGUI text;

        private void Start()
        {
            EventStart();
        }

        private void Update()
        {
            EventUpdate();      
        }

        protected override void EventStart()
        {
            // Goals
            state = TutorialStates.learnMove;
        }

        protected override void EventUpdate()
        {
            text.text = textList[currentStateNum];
        }

        protected override void EventEnd()
        {
            // All goals Finished
            state = TutorialStates.end;
        }

        protected void SwitchTutorialState(int newStateNum)
        {
            currentStateNum = newStateNum;
        }

        int debugNum = 0;
        public void DebugTutorial()
        {
            SwitchTutorialState(debugNum);
            debugNum++;
        }
    }
}
