namespace AllSpice.Services;

public class IngredientsService
{
    private readonly IngredientsRepository _repo;

    public IngredientsService(IngredientsRepository repo)
    {
        _repo = repo;
    }

    internal Ingredient CreateIngredient(Ingredient ingredientData)
    {
        int id = _repo.CreateIngredient(ingredientData);
        Ingredient ingredient = this.GetOne(id);
        return ingredient;
    }

    internal Ingredient GetOne(int ingredientId)
    {
        Ingredient ingredient = _repo.GetOne(ingredientId);
        if (ingredient == null)
        {
            throw new Exception("Ingredient does not exist.");
        }
        return ingredient;
    }
}