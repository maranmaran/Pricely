using DataAccess.Sql.Models;

namespace ItemService.Persistence.DTOModels
{
    public class IngredientDto : EntityDtoBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}