using System;
using System.Collections.Generic;
using UnityEngine;

namespace DialogCommon.Manager
{
    public class PanelContainer : MonoBehaviour, IPanelContainer
    {
        private Dictionary<Type, IPanel> _panelsByType;
        
        public Dictionary<Type, IPanel> PanelsByType => _panelsByType ??= FindAllPanels();

        private Dictionary<Type, IPanel> FindAllPanels()
        {
            var panelsByType = new Dictionary<Type, IPanel>();
            for (int i = 0; i < transform.childCount; i++)
            {
                foreach (var panel in transform.GetChild(i).GetComponents<IPanel>())
                {
                    panelsByType[panel.GetType()] = panel;
                }
            }

            return panelsByType;
        }
    }
}