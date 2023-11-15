using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Baz_geluk9.CoffeeMaker
{
    public sealed class NpcOrder : MonoBehaviour
    {
        [SerializeField] private int grade = 5;
        [SerializeField] private double liquidFillMargin = 0.1;
        [SerializeField] private NpcNeeds order;

        private bool _isLevel = true;
        private bool _isGoodLiquids;
        
        [SerializeField] private bool executeGradeOrder;

        private void Update() {
            if (executeGradeOrder) {
                GradeOrder(FindObjectOfType<MugManager>().GetMugData());
                executeGradeOrder = false; // Reset the boolean
            }
        }

        public void GradeOrder(MugData deliveredOrder)
        {
            if(order.npcOrder.Equals(deliveredOrder)) return; // perfect grade

            var lenght = order.npcOrder.liquids.Count;
            for (int index = 0; index < lenght; index++)
            {
                var height = order.npcOrder.liquidHeights[index];
                var diff = height - deliveredOrder.liquidHeights[index];
                if (!(Mathf.Abs((float) diff) > liquidFillMargin)) continue;
                
                _isLevel = false;
                break;
            }

            for (int index = 0; index < lenght; index++)
            {
                Liquid currentLiquid = order.npcOrder.liquids[index];
                Liquid nextLiquidInOrder = index + 1 < order.npcOrder.liquids.Count ? order.npcOrder.liquids[index + 1] : null;
                Liquid nextLiquidInDelivered = index + 1 < deliveredOrder.liquids.Count ? deliveredOrder.liquids[index + 1] : null;

                if (!nextLiquidInOrder && nextLiquidInDelivered
                    || !nextLiquidInDelivered) 
                    break;
                
                if (currentLiquid.gameObject.GetTag(0) != deliveredOrder.liquids[index].gameObject.GetTag(0)) continue;
                
                _isGoodLiquids = true;
                break;
            }

            Debug.Log(order.npcOrder.MainDecoEquals(deliveredOrder) + " "+ order.npcOrder.SecondaryDecoEquals(deliveredOrder) + " " + _isGoodLiquids + " " + _isLevel);
            
            AddJustGrade(order.npcOrder.MainDecoEquals(deliveredOrder));
            AddJustGrade(order.npcOrder.SecondaryDecoEquals(deliveredOrder));
            AddJustGrade(_isGoodLiquids);
            AddJustGrade(_isLevel);
        }

        private void AddJustGrade(bool targetGrade) => grade = targetGrade ? grade : grade--;
    }
}
