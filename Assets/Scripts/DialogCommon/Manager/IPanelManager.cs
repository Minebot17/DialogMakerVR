using System;

namespace DialogCommon.Manager
{
    public interface IPanelManager
    {
        T GetPanel<T>() where T : class, IPanel;
        T OpenPanel<T>() where T : class, IPanel;
        void ClosePanel<T>() where T : class, IPanel;
    }
}