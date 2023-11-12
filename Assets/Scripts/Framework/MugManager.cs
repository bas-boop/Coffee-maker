using System.Collections.Generic;
using UnityEngine;

namespace Baz_geluk9.CoffeeMaker
{
    public sealed class MugManager : MonoBehaviour
    {
        [SerializeField] private List<Liquid> liquids = new ();
        [SerializeField] private Liquid currentLiquid;
        [SerializeField] private float totalLiquidHeight;

        private void Update()
        {
            if(liquids.Count == 0) return;

            if (Input.GetKey(KeyCode.Mouse1) && !(totalLiquidHeight >= 1))
                totalLiquidHeight += 1f * Time.deltaTime;
            
            currentLiquid.isFilling = !(totalLiquidHeight >= 1) && Input.GetKey(KeyCode.Mouse1);
        }
        
        public void AddLiquid(Liquid newLiquid)
        {
            if(totalLiquidHeight >= 1 // Return when mug is full
               || currentLiquid != null && newLiquid.gameObject.name + "(Clone)" == currentLiquid.gameObject.name) // return when same liquid is being poured
                return;

            if (currentLiquid && currentLiquid.transform.localScale.y == 0)
            {
                liquids.Remove(currentLiquid);
                Destroy(currentLiquid.gameObject);
            }

            currentLiquid = Instantiate(newLiquid, transform);
            liquids.Add(currentLiquid);

            if (liquids.Count >= 2) AdjustLiquidPosition();
        }

        private void AdjustLiquidPosition() => currentLiquid.transform.position = new Vector2(currentLiquid.transform.position.x, currentLiquid.transform.position.y + totalLiquidHeight);
    }
}
