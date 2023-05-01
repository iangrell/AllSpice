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

    public Recipe GetOne(int id)
    {
        string sql = @"
        SELECT 
        recipes.*,
        creator.*
        FROM recipes 
        JOIN accounts creator ON creator.id = recipes.creatorId
        WHERE recipes.id = @id;";
        Recipe recipe = _db.Query<Recipe, Account, Recipe>(sql, (recipe, creator) =>
        {
            recipe.Creator = creator;
            return recipe;
        }, new { id }).FirstOrDefault();
        return recipe;
    }

    internal void EditRecipe(Recipe originalRecipe)
    {
        string sql = @"
        UPDATE recipes
        SET
        title = @Title,
        instructions = @Instructions,
        img = @Img,
        category = @Category
        ;";

        _db.Execute(sql, originalRecipe);
    }

    internal void Remove(int recipeId)
    {
        string sql = "DELETE FROM recipes WHERE id = @recipeId LIMIT 1;";
        _db.Execute(sql, new { recipeId });
    }
}
