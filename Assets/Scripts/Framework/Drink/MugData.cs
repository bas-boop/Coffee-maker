using System.Collections.Generic;
using System.Linq;

namespace Baz_geluk9.CoffeeMaker
{
    [System.Serializable]
    public struct MugData
    {
        public List<Liquid> liquids;
        public List<double> liquidHeights;
        public DrinkDecoration mainDecoration;
        public DrinkDecoration secondaryDecoration;
            
        // Constructor to initialize
        public MugData(
            List<Liquid> initialLiquids,
            List<double> initialHeights)
        {
            liquids = initialLiquids ?? new List<Liquid>();
            liquidHeights = initialHeights ?? new List<double>();
            mainDecoration = null;
            secondaryDecoration = null;
        }

        public bool Null()
        {
            return liquids.Count == 0
                   && liquidHeights.Count == 0
                   && !mainDecoration
                   && !secondaryDecoration;
        }

        public bool Equals(MugData other) {
            return liquids.SequenceEqual(other.liquids)
                   && liquidHeights.SequenceEqual(other.liquidHeights)
                   && mainDecoration.Equals(other.mainDecoration)
                   && secondaryDecoration.Equals(other.secondaryDecoration);
        }

        public bool LiquidsEquals(MugData other) {
            return liquids.SequenceEqual(other.liquids)
                   && liquidHeights.SequenceEqual(other.liquidHeights);
        }
        
        public bool DecoEquals(MugData other)
        {
            bool answer = false;
            bool main = mainDecoration && other.mainDecoration;
            bool secondary = secondaryDecoration && other.secondaryDecoration;

            if (main && secondary)
                answer = mainDecoration.gameObject.GetTag(0) == other.mainDecoration.gameObject.GetTag(0) 
                         && secondaryDecoration.gameObject.GetTag(0) == other.secondaryDecoration.gameObject.GetTag(0);
            
            return answer;
        }

        public bool MainDecoEquals(MugData other) => CompareDeco(mainDecoration, other.mainDecoration);

        public bool SecondaryDecoEquals(MugData other) => CompareDeco(secondaryDecoration, other.secondaryDecoration);

        private bool CompareDeco(
            DrinkDecoration thisDeco,
            DrinkDecoration otherDeco)
        {
            if (thisDeco && !otherDeco) 
                return false;
            
            if (thisDeco && otherDeco)
                return thisDeco.gameObject.GetTag(0) == otherDeco.gameObject.GetTag(0);
            
            return !thisDeco && !otherDeco;
        }
    }
}
