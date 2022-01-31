namespace Imi.Project.Common.Dtos
{
    public class InfoDto
    {

        private int _pageAmount;

        public int PageAmount
        {
            get => _pageAmount;
            set => _pageAmount = value <= 0 ? 1 : value;
        }
        public int ItemCount { get; set; }

    }
}
