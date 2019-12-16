namespace TournamentManager.Models
{
    public class TeamModel
    {
        public string Name { get; set; }

        public TeamModel(string Name)
        {
            this.Name = Name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}