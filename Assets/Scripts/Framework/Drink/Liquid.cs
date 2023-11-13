using UnityEngine;

namespace Baz_geluk9.CoffeeMaker
{
    public sealed class Liquid : MonoBehaviour
    {
        public bool isFilling;
        [SerializeField] private double fillPercentage;
        [field: SerializeField] public Transform HighestPoint { get; private set; }

        public double FillPercentage
        {
            get => fillPercentage;
            private set
            {
                if (value > 1)
                {
                    fillPercentage = 1;
                    return;
                }
                fillPercentage = value;
            }
        }

        private void Start()
        {
            transform.localScale = new Vector3(1, 0, 1);
        }

        private void Update()
        {
            if(!isFilling || fillPercentage >= 1) return;

            FillPercentage += 1f * Time.deltaTime;

            var scale = transform.localScale;
            scale.y = (float)FillPercentage;
            transform.localScale = scale;
        }
    }
}
