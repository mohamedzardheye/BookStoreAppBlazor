namespace BookStoreApp.Api.Models
{
    public class CreateRoleDto
    {
     
        public string Name { get; set; }
        public string NormalizedName { get; set; }

        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

    }
}