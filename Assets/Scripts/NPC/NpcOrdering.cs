using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Baz_geluk9.CoffeeMaker
{
    [RequireComponent(typeof(NpcOrder))]
    public sealed class NpcOrdering : MonoBehaviour
    {
        [SerializeField] private TMP_Text oderText;
        private MugData _npcOrder;
        private const string StartingText = "I want ";
        private const string LiquidText = " with a height of ";
        private const string MainDecoText = " and a ";
        private const string SecDecoText = " and for last a ";

        private const string FiveStarGrade = "This is the most perfect coffee I have seen.";
        private const string FourStarGrade = "Looking good, thank you.";
        private const string ThreeStarGrade = "Could be better :(";
        private const string TwoStarGrade = "I not taking that, drink it your self!";
        private const string OneStarGrade = "This is not my order!!!";
        private const string ZeroStarGrade = "Why must you hurt me in this way :'(";

        public void ShowOrder()
        {
            string text = String.Empty;
            List<Liquid> liquids = _npcOrder.liquids;
            List<double> heights = _npcOrder.liquidHeights;
            
            text = StartingText;
            for (int i = 0; i < liquids.Count; i++)
            {
                text += liquids[i].name + LiquidText + heights[i];
                if (i < liquids.Count - 1)
                {
                    text += ", ";
                }
            }

            if (_npcOrder.mainDecoration)
            {
                text += MainDecoText + _npcOrder.mainDecoration.name;
                if(_npcOrder.secondaryDecoration) 
                    text += SecDecoText + _npcOrder.secondaryDecoration.name;
            }

            text += ".";

            oderText.text = text;
        }

        public void ShowGradeText(int grade)
        {
            oderText.text = grade switch
            {
                5 => FiveStarGrade,
                4 => FourStarGrade,
                3 => ThreeStarGrade,
                2 => TwoStarGrade,
                1 => OneStarGrade,
                0 => ZeroStarGrade,
                _ => oderText.text
            };
        }
        
        public MugData NpcOrder { get => _npcOrder; set => _npcOrder = value; }
    }    
}
