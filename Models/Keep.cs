namespace Keepr.Models
{
    public class Keep
    {
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public int Views { get; set; }
        public int Shares { get; set; }
        // NOTE keeps is the number of times this has been saved to someone's Vault
        public int Keeps { get; set; }
        public Profile Creator { get; set; }
    }

    public class ViewModelKeep : Keep
    {
        public int vaultKeepId { get; set; }
    }
}