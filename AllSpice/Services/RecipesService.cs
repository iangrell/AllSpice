namespace AllSpice.Services;

public class RecipesService
{
    private readonly RecipesRepository _repo;

    public RecipesService(RecipesRepository repo)
    {
        _repo = repo;
    }

    internal Recipe CreateRecipe(Recipe recipeData)
    {
        Recipe recipe = _repo.CreateRecipe(recipeData);
        return recipe;
    }

    internal Recipe GetOne(int recipeId)
    {
        Recipe recipe = _repo.GetOne(recipeId);
        if (recipe == null)
        {
            throw new Exception("No recipe found with that ID");
        }
        return recipe;
    }

    internal List<Recipe> GetRecipes()
    {
        List<Recipe> recipes = _repo.Get();
        return recipes;
    }
}
