namespace DialogCommon.Manager
{
    public class PanelManager : IPanelManager
    {
        private readonly IPanelContainer _panelContainer;
        
        public PanelManager(IPanelContainer panelContainer)
        {
            _panelContainer = panelContainer;
        }
        
        public T GetPanel<T>() where T : class, IPanel
        {
            return _panelContainer.PanelsByType[typeof(T)] as T;
        }

        public T OpenPanel<T>() where T : class, IPanel
        {
            T panel = GetPanel<T>();
            if (!panel.IsOpened())
            {
                panel.Open();
            }

            return panel;
        }

        public void ClosePanel<T>() where T : class, IPanel
        {
            T panel = GetPanel<T>();
            if (panel.IsOpened())
            {
                panel.Close();
            }
        }
    }
}