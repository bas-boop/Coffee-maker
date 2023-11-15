using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Baz_geluk9.CoffeeMaker
{
    [RequireComponent(typeof(NpcOrder))]
    public sealed class NpcOrdering : MonoBehaviour
    {
        [SerializeField] private TMP_Text oderText;
        private NpcOrder _npcOrder;

        private void Awake() => _npcOrder = GetComponent<NpcOrder>();

        public void ShowOrder()
        {
            string text = String.Empty;
            var order = _npcOrder.Order.npcOrder;
            List<Liquid> liquids = _npcOrder.Order.npcOrder.liquids;
            List<double> heights = _npcOrder.Order.npcOrder.liquidHeights;
            
            text = "I want ";
            for (int i = 0; i < liquids.Count; i++)
            {
                text += liquids[i].name + " with a height of " + heights[i];
                if (i < liquids.Count - 1)
                {
                    text += ", ";
                }
            }

            if (order.mainDecoration)
            {
                text += " and a " + order.mainDecoration.name;
                if(order.secondaryDecoration) 
                    text += " and for last a " + order.secondaryDecoration.name;
            }

            text += ".";

            oderText.text = text;
        }
    }    
}
