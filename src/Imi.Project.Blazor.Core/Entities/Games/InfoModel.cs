namespace Imi.Project.Blazor.Core.Entities.Games
{
    public class InfoModel
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
