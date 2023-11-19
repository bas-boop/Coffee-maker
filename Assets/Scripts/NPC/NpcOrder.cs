using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        [SerializeField] private Image timerImage;

        private bool _isLevel = true;
        private bool _isGoodLiquids;
        private float _waitingTime;

        private void Start() => SetNewEverything(true);

        private void Update() {
            if (_waitingTime < 0)
            {
                npcOrdering.ShowGradeText(0);
                SetNewEverything();
            }
            
            _waitingTime -= Time.deltaTime;
            UpdateTimerFill();
        }

        public void GradeOrder(MugData deliveredOrder)
        {
            if (deliveredOrder.Null())
            {
                npcOrdering.ShowGradeText(0);
                SetNewEverything();
                return;
            }
            
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
            
            AddJustGrade(order.npcOrder.MainDecoEquals(deliveredOrder));
            AddJustGrade(order.npcOrder.SecondaryDecoEquals(deliveredOrder));
            AddJustGrade(_isGoodLiquids);
            AddJustGrade(_isLevel);
            
            npcOrdering.ShowGradeText(grade);
            
            SetNewEverything();
        }
        
        private void UpdateTimerFill()
        {
            float fillAmount = _waitingTime / waitTime;
            timerImage.fillAmount = fillAmount;
        }

        private void SetNewEverything(bool start = false)
        {
            SetNpcOder();
            grade = 5;
            _waitingTime = waitTime;
            npcOrdering.NpcOrder = order.npcOrder;
            StartCoroutine(DelayText(start));
        }

        private IEnumerator DelayText(bool start)
        {
            yield return new WaitForSeconds(5);
            if (!start) npcOrdering.ShowOrder();
        }

        private void SetNpcOder()
        {
            List<NpcNeeds> availableOrders = new List<NpcNeeds>(allOrders);
            availableOrders.Remove(order);
            
            int randomNumber = Random.Range(0, availableOrders.Count);
            order = availableOrders[randomNumber];
        }

        private void AddJustGrade(bool targetGrade) => grade = targetGrade ? grade : grade - 1;
    }
}
