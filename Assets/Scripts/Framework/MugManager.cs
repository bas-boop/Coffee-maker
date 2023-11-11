using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Baz_geluk9.CoffeeMaker
{
    public class MugManager : MonoBehaviour
    {
        [SerializeField] private List<Liquid> liquids = new ();
        [SerializeField] private Liquid currentLiquid;
        [SerializeField] private float totalLiquidHeight;

        private void Update()
        {
            if(liquids.Count == 0) return;

            if (Input.GetKey(KeyCode.Mouse1) && !(totalLiquidHeight >= 1))
            {
                totalLiquidHeight += 1f * Time.deltaTime;
            }
            
            currentLiquid.isFilling = !(totalLiquidHeight >= 1) && Input.GetKey(KeyCode.Mouse1);
        }
        
        public void AddLiquid(Liquid newLiquid)
        {
            currentLiquid = newLiquid;
            liquids.Add(currentLiquid);

            if (liquids.Count != 0) AdjustLiquidPosition();
        }

        private void AdjustLiquidPosition()
        {
            var pos = currentLiquid.gameObject.transform.position;
            pos.y = totalLiquidHeight;
            currentLiquid.gameObject.transform.position = pos;
        }
    }
}
