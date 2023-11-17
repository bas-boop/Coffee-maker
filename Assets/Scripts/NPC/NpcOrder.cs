using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Baz_geluk9.CoffeeMaker
{
    public sealed class NpcOrder : MonoBehaviour
    {
        [SerializeField] private int grade = 5;
        [SerializeField] private double liquidFillMargin = 0.1;
        [SerializeField] private NpcNeeds order;
        [SerializeField] private NpcNeeds[] allOrders;
        [SerializeField, Range(0, 180)] private int waitTime;
        [SerializeField] private NpcOrdering npcOrdering;

        private bool _isLevel = true;
        private bool _isGoodLiquids;
        private float _waitingTime;
        
        // [SerializeField] private bool executeGradeOrder;

        private void Start() => SetNewEverything();

        private void Update() {
            // if (executeGradeOrder) {
            //     GradeOrder(FindObjectOfType<MugManager>().GetMugData());
            //     executeGradeOrder = false; // Reset the boolean
            // }

            if (_waitingTime < 0)
            {
                order = ScriptableObject.CreateInstance<NpcNeeds>();
                GradeOrder(order.npcOrder);
            }
            
            _waitingTime -= Time.deltaTime;
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

            // Debug.Log(order.npcOrder.MainDecoEquals(deliveredOrder) + " "+ order.npcOrder.SecondaryDecoEquals(deliveredOrder) + " " + _isGoodLiquids + " " + _isLevel);
            
            AddJustGrade(order.npcOrder.MainDecoEquals(deliveredOrder));
            AddJustGrade(order.npcOrder.SecondaryDecoEquals(deliveredOrder));
            AddJustGrade(_isGoodLiquids);
            AddJustGrade(_isLevel);
            
            SetNewEverything();
        }

        private void SetNewEverything()
        {
            SetNpcOder();
            grade = 5;
            _waitingTime = waitTime;
            npcOrdering.NpcOrder = order.npcOrder;
            npcOrdering.ShowOrder();
        }
        
        private void SetNpcOder()
        {
            int randomNumber = Random.Range(0, allOrders.Length);
            order = allOrders[randomNumber];
        }

        private void AddJustGrade(bool targetGrade) => grade = targetGrade ? grade : grade--;
    }
}
