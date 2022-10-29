using System;
using System.Collections.Generic;

namespace DialogCommon.Manager
{
    public interface IPanelContainer
    {
        Dictionary<Type, IPanel> PanelsByType { get; }
    }
}