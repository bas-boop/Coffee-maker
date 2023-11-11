using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Baz_geluk9.CoffeeMaker
{
    public class Liquid : MonoBehaviour
    {
        public bool isFilling;
        [SerializeField] private float fillPercentage;
        [field: SerializeField] public Transform HighestPoint { get; private set; }

        public float FillPercentage
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
            scale.y = FillPercentage;
            transform.localScale = scale;
        }
    }
}
