using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SwordfishGame
{
    public class TutorialEvent : EventObj
    {
        enum TutorialStates
        {
            learnMove,
            learnLook,
            learnAttack,
            learnLean, 
            end
        }

        TutorialStates state;
        int currentState = 0;
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
            text.text = textList[currentState];
        }

        protected override void EventEnd()
        {
            // All goals Finished
            state = TutorialStates.end;
        }
    }
}
