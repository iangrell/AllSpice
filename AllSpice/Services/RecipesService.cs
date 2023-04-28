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
        int id = _repo.CreateRecipe(recipeData);
        Recipe recipe = this.GetOne(id);
        return recipe;
    }

    internal Recipe GetOne(int recipesId)
    {
        Recipe recipe = _repo.GetOne(recipesId);
        if (recipe == null) throw new Exception("No recipe found with that ID");
        return recipe;
    }

    internal List<Recipe> GetRecipes()
    {
        List<Recipe> recipes = _repo.Get();
        return recipes;
    }
}
