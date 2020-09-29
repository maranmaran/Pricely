using DataAccess.Sql.Entities;

namespace ItemService.Persistence.DTOModels
{
    public class IngredientDto : EntityDtoBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}