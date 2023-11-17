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
        
        public MugData NpcOrder { get => _npcOrder; set => _npcOrder = value; }
    }    
}
