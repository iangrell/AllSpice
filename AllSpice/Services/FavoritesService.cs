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

    internal Favorite GetOne(int id)
    {
        Favorite favorite = _repo.GetOne(id);
        if (favorite == null)
        {
            throw new Exception("No favorite found");
        }
        return favorite;
    }

    internal string RemoveFavorite(int favoriteId, string userId)
    {
        Favorite favorite = GetOne(favoriteId);
        if (favorite.AccountId != userId)
        {
            throw new Exception("You do not have access");
        }
        _repo.RemoveFavorite(favoriteId);
        return "This recipe is no longer a favorite";
    }
}
