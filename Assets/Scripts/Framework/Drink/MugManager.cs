using System.Collections.Generic;
using UnityEngine;

namespace Baz_geluk9.CoffeeMaker
{
    public sealed class MugManager : MonoBehaviour
    {
        [SerializeField] private List<Liquid> liquids = new ();
        [SerializeField] private Liquid currentLiquid;
        [SerializeField] private DrinkDecoration mainDecoration;
        [SerializeField] private DrinkDecoration secondaryDecoration;
        [SerializeField] private double totalLiquidHeight;

        private MugData _currentMugData = new MugData();

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
               || currentLiquid != null && newLiquid.gameObject.GetTag(0) == currentLiquid.gameObject.GetTag(0)) // return when same liquid is being poured
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

        public void AddDecoration(DrinkDecoration newDecoration)
        {
            if(totalLiquidHeight < 1) return;

            if (newDecoration.gameObject.HasTag("MainDeco"))
                mainDecoration = Instantiate(newDecoration, newDecoration.TargetPosition, transform.rotation, transform);
            else if (newDecoration.gameObject.HasTag("SecondaryDeco"))
                secondaryDecoration = Instantiate(newDecoration, newDecoration.TargetPosition, transform.rotation, transform);
        }

        public MugData GetMugData()
        {
            _currentMugData = new MugData(new List<Liquid>(), new List<double>());
            
            var lenght = liquids.Count;
            for (int i = 0; i < lenght; i++)
            {
                _currentMugData.liquids.Add(liquids[i]);
                _currentMugData.liquidHeights.Add(liquids[i].transform.localScale.y);
            }
            
            if (mainDecoration) _currentMugData.mainDecoration = mainDecoration;
            if (secondaryDecoration) _currentMugData.secondaryDecoration = secondaryDecoration;
            
            return _currentMugData;
        }
        
        private void AdjustLiquidPosition() => currentLiquid.transform.position =
            new Vector2(currentLiquid.transform.position.x, currentLiquid.transform.position.y + (float)totalLiquidHeight);
    }
}
