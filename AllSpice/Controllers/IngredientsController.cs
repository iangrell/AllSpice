namespace AllSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
    private readonly IngredientsService _ingredientsService;
    private readonly Auth0Provider _auth;

    public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth)
    {
        _ingredientsService = ingredientsService;
        _auth = auth;
    }

    [HttpPost]
    public ActionResult<Ingredient> CreateIngredient([FromBody] Ingredient ingredientData)
    {
        try
        {
            Ingredient ingredient = _ingredientsService.CreateIngredient(ingredientData);
            return Ok(ingredient);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{ingredientId}")]
    public ActionResult<Ingredient> GetOne(int ingredientId)
    {
        try
        {
            Ingredient ingredient = _ingredientsService.GetOne(ingredientId);
            return Ok(ingredient);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
