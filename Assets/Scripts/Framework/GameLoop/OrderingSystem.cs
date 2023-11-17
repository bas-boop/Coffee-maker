using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Baz_geluk9.CoffeeMaker
{
    public class OrderingSystem : MonoBehaviour
    {
        [SerializeField] private NpcNeeds[] allOrders;
        [SerializeField] private NpcOrder npcOrder;
        [SerializeField, Range(0, 180)] private int waitTime;
        private NpcOrdering _npcOrdering;

        private void Awake() => _npcOrdering = npcOrder.GetComponent<NpcOrdering>();

        private void SetNpcOder()
        {
            int randomNumber = Random.Range(0, allOrders.Length);
            npcOrder.Order = allOrders[randomNumber];
        }

        private void StartGrading()
        {
            _npcOrdering.ShowOrder();
        }

        private IEnumerator StartWaiting()
        {
            yield return new WaitForSeconds(waitTime);
            // todo: duurde te lang met maken >:(
        }
    }
}
