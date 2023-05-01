namespace AllSpice.Services;

public class FavoritesService
{
    private readonly FavoritesRepository _repo;

    public FavoritesService(FavoritesRepository repo)
    {
        _repo = repo;
    }

    internal Favorite CreateFavorite(Favorite favoriteData)
    {
        Favorite favorite = _repo.CreateFavorite(favoriteData);
        return favorite;
    }

    internal List<MyFavoriteRecipe> GetMyFavoriteRecipes(string userId)
    {
        List<MyFavoriteRecipe> recipes = _repo.GetMyFavoriteRecipes(userId);
        return recipes;
    }
}
