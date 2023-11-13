using UnityEngine;

namespace Baz_geluk9.CoffeeMaker
{
    [CreateAssetMenu(fileName = "NewNpcOrder", menuName = "Npc Oder Data")]
    public sealed class NpcNeeds : ScriptableObject
    {
        [SerializeField] public MugData npcOrder;
    }
}
