namespace BrookerCompany.ViewModels.Projects
{

    public class ProjectIndexViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BuildingType { get; set; }
        public int Capacity { get; set; }
        public string ReleaseDate { get; set; }
        public int FloorArea { get; set; }
        public int FloorsCount { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; } = "https://ethiopianbroker.com/wp-content/uploads/2022/05/Ethi-PNG-01.png"

    }
}
