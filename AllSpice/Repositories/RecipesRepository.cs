namespace AllSpice.Repositories;

public class RecipesRepository
{
    private readonly IDbConnection _db;

    public RecipesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal int CreateRecipe(Recipe recipeData)
    {
        string sql = @"
        INSERT INTO
    recipes(
        title,
        instructions,
        img,
        category,
        creatorId
    )
    values(
        @Title, 
        @Instructions, 
        @Img, 
        @Category,
        @CreatorId
    );
    SELECT LAST_INSERT_ID();";

        int id = _db.ExecuteScalar<int>(sql, recipeData);
        recipeData.Id = id;

        return id;
    }

    public List<Recipe> Get()
    {
        string sql = @"
        SELECT
    recipes.*,
    creator.*
FROM recipes
    JOIN accounts creator ON creator.id = recipes.creatorId;";
        List<Recipe> recipes = _db.Query<Recipe, Account, Recipe>(sql, (recipe, creator) =>
        {
            recipe.Creator = creator;
            return recipe;
        }).ToList();
        return recipes;
    }

    internal Recipe GetOne(int recipeId)
    {
        string sql = "SELECT * FROM recipes WHERE id = @recipeId;";

        Recipe recipe = _db.Query<Recipe>(sql, new { recipeId }).FirstOrDefault();
        return recipe;
    }
}
