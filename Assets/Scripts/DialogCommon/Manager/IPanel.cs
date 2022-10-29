namespace DialogCommon.Manager
{
    public interface IPanel
    {
        void Open();
        void Close();
        bool IsOpened();
    }
}