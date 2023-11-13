using UnityEngine;

namespace Baz_geluk9.CoffeeMaker
{
    public sealed class DrinkDecoration : MonoBehaviour
    {
        [SerializeField] private Vector3 targetPosition;
        public Vector3 TargetPosition => targetPosition;
    }
}
