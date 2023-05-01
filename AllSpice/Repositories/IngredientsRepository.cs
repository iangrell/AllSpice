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
}
