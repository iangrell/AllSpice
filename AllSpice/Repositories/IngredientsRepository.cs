namespace AllSpice.Repositories;

public class IngredientsRepository
{
    private readonly IDbConnection _db;

    public IngredientsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal int CreateIngredient(Ingredient ingredientData)
    {
        string sql = @"
        INSERT INTO ingredients(
            name, quantity, recipeId
        )
        values(
            @Name, @Quantity, @RecipeId
        );
        SELECT LAST_INSERT_ID();";
        int id = _db.ExecuteScalar<int>(sql, ingredientData);
        return id;
    }

    internal Ingredient GetOne(int ingredientId)
    {
        string sql = "SELECT * FROM ingredients WHERE id = @ingredientId;";
        Ingredient ingredient = _db.Query<Ingredient>(sql, new { ingredientId }).FirstOrDefault();
        return ingredient;
    }

    internal List<Ingredient> GetRecipeIngredients(int recipeId)
    {
        string sql = "SELECT * FROM ingredients WHERE ingredients.recipeId = @recipeId;";
        List<Ingredient> ingredients = _db.Query<Ingredient>(sql, new { recipeId }).ToList();
        return ingredients;
    }

    internal void Remove(int ingredientId)
    {
        string sql = "DELETE FROM ingredients WHERE id = @ingredientId LIMIT 1";
        _db.Execute(sql, new { ingredientId });
    }
}
