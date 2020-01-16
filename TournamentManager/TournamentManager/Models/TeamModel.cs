namespace TournamentManager.Models
{
    public class TeamModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public TeamModel(string Name)
        {
            this.Name = Name;
        }

        public override string ToString()
        {
            return Name;
        }

        public TeamModel() { }
    }
}