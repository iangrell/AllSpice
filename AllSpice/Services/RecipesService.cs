namespace AllSpice.Services;

public class RecipesService
{
    private readonly RecipesRepository _repo;
    private readonly IngredientsService _ingredientsService;

    public RecipesService(RecipesRepository repo, IngredientsService ingredientsService)
    {
        _repo = repo;
        _ingredientsService = ingredientsService;
    }

    internal Recipe CreateRecipe(Recipe recipeData)
    {
        int id = _repo.CreateRecipe(recipeData);
        Recipe recipe = this.GetOne(id);
        return recipe;
    }

    internal Recipe EditRecipe(Recipe recipeData, int recipeId)
    {
        Recipe originalRecipe = this.GetOne(recipeId);

        originalRecipe.Title = recipeData.Title ?? originalRecipe.Title;
        originalRecipe.Instructions = recipeData.Instructions ?? originalRecipe.Instructions;
        originalRecipe.Img = recipeData.Img ?? originalRecipe.Img;
        originalRecipe.Category = recipeData.Category ?? originalRecipe.Category;

        _repo.EditRecipe(originalRecipe);
        originalRecipe.UpdatedAt = DateTime.Now;
        return originalRecipe;
    }

    internal Recipe GetOne(int recipeId)
    {
        Recipe recipe = _repo.GetOne(recipeId);
        if (recipe == null) throw new Exception("No recipe found with that ID");
        return recipe;
    }

    internal List<Ingredient> GetRecipeIngredients(int recipeId)
    {
        Recipe recipe = _repo.GetOne(recipeId);
        List<Ingredient> ingredients = _ingredientsService.GetRecipeIngredients(recipeId);
        return ingredients;
    }

    internal List<Recipe> GetRecipes()
    {
        List<Recipe> recipes = _repo.Get();
        return recipes;
    }

    internal string Remove(int recipeId, string userId)
    {
        Recipe recipe = GetOne(recipeId);

        if (recipe.CreatorId != userId)
        {
            throw new Exception("This ID doesn't match");
        }
        _repo.Remove(recipeId);
        return "Recipe deleted";
    }
}
