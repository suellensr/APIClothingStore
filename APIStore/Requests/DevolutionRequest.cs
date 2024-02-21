using System.ComponentModel.DataAnnotations;

namespace APIStore.Requests
{
    public class DevolutionRequest
    {
        [Required]
        public List<int> ProductId { get; set; }
    }
}
