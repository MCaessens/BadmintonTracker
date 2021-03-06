namespace Imi.Project.Mobile.Core.Entities
{
    public class Info
    {
        private int _pageAmount;

        public int PageAmount
        {
            get => _pageAmount;
            set
            {
                if (value <= 0) _pageAmount = 1;
                else _pageAmount = value;
            }
        }
        public int ItemCount { get; set; }
    }
}
