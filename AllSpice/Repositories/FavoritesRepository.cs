namespace AllSpice.Repositories;

public class FavoritesRepository
{
    private readonly IDbConnection _db;

    public FavoritesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Favorite CreateFavorite(Favorite favoriteData)
    {
        string sql = @"
        INSERT INTO
        favorites(recipeId, accountId)
        values(@RecipeId, @AccountId);
        SELECT LAST_INSERT_ID()
        ;";
        int id = _db.ExecuteScalar<int>(sql, favoriteData);
        favoriteData.Id = id;
        favoriteData.CreatedAt = DateTime.Now;
        favoriteData.UpdatedAt = DateTime.Now;
        return favoriteData;
    }

    internal List<MyFavoriteRecipe> GetMyFavoriteRecipes(string userId)
    {
        string sql = @"
        SELECT
        favorites.*,
        recipes.*,
        accounts.*
        FROM favorites
        JOIN recipes ON favorites.recipeId = recipes.id
        JOIN accounts ON recipes.creatorId = accounts.id
        WHERE favorites.accountId = @userId
        ;";
        List<MyFavoriteRecipe> recipes = _db.Query<Favorite, MyFavoriteRecipe, Profile, MyFavoriteRecipe>
        (sql, (favorites, recipes, accounts) =>
        {
            recipes.Creator = accounts;
            recipes.FavoriteId = favorites.Id;
            return recipes;
        }, new { userId }).ToList();
        return recipes;
    }
}
