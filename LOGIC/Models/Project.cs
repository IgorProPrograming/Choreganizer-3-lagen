namespace LOGIC.Models
{
    public class Project
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string ProjectName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastEditDate { get; set; }

    }
}
