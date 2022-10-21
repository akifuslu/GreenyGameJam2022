using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ChoiceSystem
{
    public static class ChoiceActionProcessor
    {
        private static readonly Dictionary<ChoiceActionTypes, ChoiceActionBase> CardActionDict =
            new Dictionary<ChoiceActionTypes, ChoiceActionBase>();

        public static bool IsInitialized { get; private set; }

        public static void Initialize()
        {
            CardActionDict.Clear();

            var allActionCards = Assembly.GetAssembly(typeof(ChoiceActionBase)).GetTypes()
                .Where(t => typeof(ChoiceActionBase).IsAssignableFrom(t) && t.IsAbstract == false);

            foreach (var actionCard in allActionCards)
            {
                ChoiceActionBase action = Activator.CreateInstance(actionCard) as ChoiceActionBase;
                if (action != null) CardActionDict.Add(action.ActionType, action);
            }

            IsInitialized = true;
        }

        public static ChoiceActionBase GetAction(ChoiceActionTypes targetAction) =>
            CardActionDict[targetAction];
    }
}