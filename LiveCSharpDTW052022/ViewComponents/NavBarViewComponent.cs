using Microsoft.AspNetCore.Mvc;

public class NavBarViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {

        // Pas besoin de récupérer l'objet UserModel ici, nous utilisons directement User.Identity.IsAuthenticated
        return View("_Administration");
    }
}